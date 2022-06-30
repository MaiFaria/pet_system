using PS.Catalog.API.Validation;
using PS.Core.DomainObjects;

namespace PS.Catalog.API.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateRegister { get; set; }
        public string Image { get; set; }
        public int? QuantityStock { get; set; }

        public void WithdrawStock(int quantity)
        {
            if (QuantityStock >= quantity)
                QuantityStock -= quantity;
        }

        public bool IsAvaiable(int quantidade)
        {
            return Active && QuantityStock >= quantidade;
        }

        public async Task ValidateForPersistence()
        {
            ValidationResult = await new ProductValidation().ValidateAsync(this);
        }
    }
}