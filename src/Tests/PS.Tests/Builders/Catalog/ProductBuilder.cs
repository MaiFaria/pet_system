using Bogus;
using PS.Catalog.API.Models;
using PS.Core.DomainObjects;

namespace PS.Tests.Builders.Catalog
{
    public class ProductBuilder
    {
        private readonly Faker? _faker;

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateRegister { get; set; }
        public string Image { get; set; }
        public int? QuantityStock { get; set; }
        public Guid Id { get; set; }

        public ProductBuilder()
        {
            _faker = FakerBuilder.New().Build();
        }

        public ProductBuilder New()
        {
            Name = _faker.Person.FullName;
            Description = _faker.Commerce.ProductDescription();
            Active = _faker.Random.Bool();
            Price = _faker.Random.Decimal();
            DateRegister = DateTime.Now;
            Image = _faker.Image.Cats();
            QuantityStock = _faker.Random.Int();
            Id = _faker.Random.Guid();

            return this;
        }

        public Product Build()
        {
            var model = new Product();

            if(Name != null)
                model.Name = Name;

            if (Description != null)
                model.Description = Description;

            if (Active != null)
                model.Active = Active;

            if (Price != null)
                model.Price = Price;

            if (DateRegister != null)
                model.DateRegister = DateRegister;

            if (Image != null)
                model.Image = Image;

            if (QuantityStock != null)
                model.QuantityStock = QuantityStock;

            if (Id != null)
                model.Id = Id;

            return model;
        }
    }
}
