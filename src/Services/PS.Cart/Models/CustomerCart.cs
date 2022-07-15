using FluentValidation;
using FluentValidation.Results;
using PS.Core.DomainObjects;

namespace PS.Cart.Models
{
    public class CustomerCart : Entity
    {
        internal const int MAX_QUANTIDADE_ITEM = 5;

        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<CartItem> Itens { get; set; } = new List<CartItem>();

        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; set; }

        public Voucher Voucher { get; set; }

        public CustomerCart(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
        }

        public CustomerCart() { }

        public void AplicarVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorCarrinho();
        }

        internal void CalcularValorCarrinho()
        {
            ValorTotal = Itens.Sum(p => p.CalcularValor());
            CalcularValorTotalDesconto();
        }

        private void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.DiscountType == DiscountTypeVoucher.Percentage)
            {
                if (Voucher.Percent.HasValue)
                {
                    desconto = valor * Voucher.Percent.Value / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.DiscountValue.HasValue)
                {
                    desconto = Voucher.DiscountValue.Value;
                    valor -= desconto;
                }
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        internal bool CartExistingItem(CartItem item)
        {
            return Itens.Any(p => p.ProductId == item.ProductId);
        }

        internal CartItem GetProductById(Guid produtoId)
        {
            return Itens.FirstOrDefault(p => p.ProductId == produtoId);
        }

        internal void AdicionarItem(CartItem item)
        {
            item.AssociarCarrinho(Id);

            if (CartExistingItem(item))
            {
                var itemExistente = GetProductById(item.ProductId);
                itemExistente.AdicionarUnidades(item.Quantity);

                item = itemExistente;
                Itens.Remove(itemExistente);
            }

            Itens.Add(item);
            CalcularValorCarrinho();
        }

        internal void AtualizarItem(CartItem item)
        {
            item.AssociarCarrinho(Id);

            var itemExistente = GetProductById(item.ProductId);

            Itens.Remove(itemExistente);
            Itens.Add(item);

            CalcularValorCarrinho();
        }

        internal void AtualizarUnidades(CartItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        internal void RemoverItem(CartItem item)
        {
            Itens.Remove(GetProductById(item.ProductId));
            CalcularValorCarrinho();
        }

        public async Task ValidateForPersistence()
        {
            ValidationResult = await new CustomerCartValidation().ValidateAsync(this);
        }

        public class CustomerCartValidation : AbstractValidator<CustomerCart>
        {
            public CustomerCartValidation()
            {
                RuleFor(c => c.ClienteId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Cliente não reconhecido");

                RuleFor(c => c.Itens.Count)
                    .GreaterThan(0)
                    .WithMessage("O carrinho não possui itens");

                RuleFor(c => c.ValorTotal)
                    .GreaterThan(0)
                    .WithMessage("O valor total do carrinho precisa ser maior que 0");
            }
        }
    }
}


