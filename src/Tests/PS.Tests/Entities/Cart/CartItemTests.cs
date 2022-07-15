using PS.Tests.Builders.Cart;
using Xunit;
using Xunit.Abstractions;

namespace PS.Tests.Entities.Cart
{
    public class CartItemTests
    {
        private readonly CartItemBuilder _builder;
        private readonly ITestOutputHelper _output;

        public CartItemTests(ITestOutputHelper output)
        {
            _builder = new CartItemBuilder();
            _output = output;
        }

        #region 01 - ProductId
        [Fact(DisplayName = "#01 - Não deve criar sem ProductId")]
        public void MustNotCreateProductId()
        {
            var builder = _builder.New();
            builder.ProductId = Guid.Empty;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#01.1 - Deve criar um ProductId")]
        public void MustCreateProductId()
        {
            var builder = _builder.New();
            builder.ProductId = builder.ProductId;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 02 - Name
        [Fact(DisplayName = "#02 - Não deve criar sem Name")]
        public void MustNotCreateName()
        {
            var builder = _builder.New();
            builder.Name = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#02.1 - Deve criar um Name")]
        public void MustCreateName()
        {
            var builder = _builder.New();
            builder.Name = builder.Name;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 03 - Quantity
        [Fact(DisplayName = "#03 - Não deve criar sem Quantity")]
        public void MustNotCreateQuantity()
        {
            var builder = _builder.New();
            builder.Quantity = 0;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#03.1 - Deve criar uma Quantity")]
        public void MustCreateQuantity()
        {
            var builder = _builder.New();
            builder.Quantity = builder.Quantity;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#03.2 - Deve criar uma Quantity ultrapassando o valor máximo")]
        public void MustCreateQuantityMax()
        {
            var builder = _builder.New();
            builder.Quantity = 10;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 04 - Price
        [Fact(DisplayName = "#04 - Não deve criar sem Price")]
        public void MustNotCreatePrice()
        {
            var builder = _builder.New();
            builder.Price = 0;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#04.1 - Deve criar um Price")]
        public void MustCreatePrice()
        {
            var builder = _builder.New();
            builder.Price = builder.Price;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion
    }
}
