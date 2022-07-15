using Bogus;
using PS.Cart.Models;
using PS.Core.DomainObjects;

namespace PS.Tests.Builders.Cart
{
    public class CartItemBuilder
    {
        private readonly Faker _faker;

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid CartId { get; set; }
        public CustomerCart CustomerCart { get; set; }

        public CartItemBuilder()
        {
            _faker = FakerBuilder.New().Build();
        }

        public CartItemBuilder New()
        {
            Id = _faker.Random.Guid();
            ProductId = _faker.Random.Guid();
            Name = _faker.Commerce.Product();
            Quantity = _faker.Random.Int(1, 5);
            Price = _faker.Random.Decimal();
            Image = _faker.Image.Animals();
            CartId = _faker.Random.Guid();
            CustomerCart = null;

            return this;
        }

        public CartItem Build()
        {
            var model = new CartItem();

            if(Id != Guid.Empty)
                model.Id = Id;

            if (ProductId != Guid.Empty)
                model.ProductId = ProductId;

            if (Name != null)
                model.Name = Name;

            if (Quantity != null)
                model.Quantity = Quantity;

            if (Price != null)
                model.Price = Price;

            if (Image != null)
                model.Image = Image;

            if (CartId != Guid.Empty)
                model.CartId = CartId;

            model.CustomerCart = CustomerCart;

            return model;
        }
    }
}
