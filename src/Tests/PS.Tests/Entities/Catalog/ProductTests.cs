using PS.Catalog.API.Models;
using PS.Tests.Builders.Catalog;
using Xunit;
using Xunit.Abstractions;

namespace PS.Tests.Entities.Catalog
{
    public class ProductTests
    {
        private readonly ProductBuilder _builder;
        private ITestOutputHelper _output;

        public ProductTests(ITestOutputHelper output)
        {
            _builder = new ProductBuilder();
            _output = output;
        }

        [Fact(DisplayName = "#0 - Deve criar um model de Product")]
        public void MustCreateProductModel()
        {
            var builder = _builder.New();
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();
            Helpers.Assert.Equal<Product>(builder, model, _output);
        }

        #region 01 - Name
        [Fact(DisplayName = "#01 - Não deve criar sem Name")]
        public void MustNotCreateName()
        {
            var builder = _builder.New();
            builder.Name = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#01.1 - Deve criar um Name")]
        public void MustCreateName()
        {
            var builder = _builder.New();
            builder.Name = builder.Name;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 02 - Description
        [Fact(DisplayName = "#02 - Não deve criar sem Description")]
        public void MustNotCreateDescription()
        {
            var builder = _builder.New();
            builder.Description = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#02.1 - Deve criar um Description")]
        public void MustCreateDescription()
        {
            var builder = _builder.New();
            builder.Description = builder.Description;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 03 - Active
        [Fact(DisplayName = "#03 - Deve criar um Active FALSE")]
        public void MustNotCreateActive()
        {
            var builder = _builder.New();
            builder.Active = false;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#03.1 - Deve criar um Active TRUE")]
        public void MustCreateActive()
        {
            var builder = _builder.New();
            builder.Active = true;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 04 - Active
        [Fact(DisplayName = "#04 - Não deve criar sem Price")]
        public void MustNotCreatePrice()
        {
            var builder = _builder.New();
            builder.Price = null;
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

        #region 05 - DateRegister
        [Fact(DisplayName = "#05 - Não deve criar sem DateRegister")]
        public void MustNotCreateDateRegister()
        {
            var builder = _builder.New();
            builder.DateRegister = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#05.1 - Deve criar um DateRegister")]
        public void MustCreateDateRegister()
        {
            var builder = _builder.New();
            builder.DateRegister = builder.DateRegister;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 06 - Image
        [Fact(DisplayName = "#06 - Não deve criar sem Image")]
        public void MustNotCreateImage()
        {
            var builder = _builder.New();
            builder.Image = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#06.1 - Deve criar um Image")]
        public void MustCreateImage()
        {
            var builder = _builder.New();
            builder.Image = builder.Image;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 07 - QuantityStock
        [Fact(DisplayName = "#07 - Não deve criar sem QuantityStock")]
        public void MustNotCreateQuantityStock()
        {
            var builder = _builder.New();
            builder.QuantityStock = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#07.1 - Deve criar um QuantityStock")]
        public void MustCreateQuantityStock()
        {
            var builder = _builder.New();
            builder.QuantityStock = builder.QuantityStock;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion
    }
}
