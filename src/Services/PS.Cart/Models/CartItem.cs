using FluentValidation;
using System.Text.Json.Serialization;
using PS.Core.DomainObjects;

namespace PS.Cart.Models
{
    public class CartItem : Entity
    {
        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public Guid CartId { get; set; }

        [JsonIgnore]
        public CustomerCart CustomerCart { get; set; }

        internal void AssociarCarrinho(Guid carrinhoId)
        {
            CartId = carrinhoId;
        }

        internal decimal CalcularValor()
        {
            return Quantity * Price;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantity += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantity = unidades;
        }

        public async Task ValidateForPersistence()
        {
            ValidationResult = await new ItemCarrinhoValidation().ValidateAsync(this);
        }

        public class ItemCarrinhoValidation : AbstractValidator<CartItem>
        {
            public ItemCarrinhoValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado");

                RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade miníma para o {item.Name} é 1");

                RuleFor(c => c.Quantity)
                    .LessThanOrEqualTo(CustomerCart.MAX_QUANTIDADE_ITEM)
                    .WithMessage(item => $"A quantidade máxima do {item.Name} é {CustomerCart.MAX_QUANTIDADE_ITEM}");

                RuleFor(c => c.Price)
                    .GreaterThan(0)
                    .WithMessage(item => $"O valor do {item.Name} precisa ser maior que 0");
            }
        }
    }
}