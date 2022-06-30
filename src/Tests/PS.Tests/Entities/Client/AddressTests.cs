using PS.Client.API.Models;
using PS.Tests.Builders.Client;
using Xunit;
using Xunit.Abstractions;

namespace PS.Tests.Entities.Client
{
    public class AddressTests
    {
        private readonly AddressBuilder _builder;
        private readonly ITestOutputHelper _output;

        public AddressTests(ITestOutputHelper output)
        {
            _builder = new AddressBuilder();
            _output = output;
        }


        [Fact(DisplayName = "#0 - Deve criar um model de Address")]
        public void MustCreateAddressModel()
        {
            var builder = _builder.New();
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();
            Helpers.Assert.Equal<Address>(builder, model, _output);
        }

        #region 01 - FullAddress
        [Fact(DisplayName = "#01 - Não deve criar sem Public Place")]
        public void MustNotCreatePublicPlace()
        {
            var builder = _builder.New();
            builder.PublicPlace = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#01.1 - Deve criar um Public Place")]
        public void MustCreatePublicPlace()
        {
            var builder = _builder.New();
            builder.PublicPlace = builder.PublicPlace;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 02 - Number
        [Fact(DisplayName = "#02 - Não deve criar sem Number")]
        public void MustNotCreateNumber()
        {
            var builder = _builder.New();
            builder.Number = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#02.1 - Deve criar um Number")]
        public void MustCreateNumber()
        {
            var builder = _builder.New();
            builder.Number = builder.Number;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 03 - Complement
        [Fact(DisplayName = "#03 - Deve criar um Complement")]
        public void MustCreateComplement()
        {
            var builder = _builder.New();
            builder.Complement = builder.Complement;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 04 - District
        [Fact(DisplayName = "#04 - Não deve criar sem District")]
        public void MustNotCreateDistrict()
        {
            var builder = _builder.New();
            builder.PublicPlace = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#04.1 - Deve criar um District")]
        public void MustCreateDistrict()
        {
            var builder = _builder.New();
            builder.District = builder.District;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 05 - CEP
        [Fact(DisplayName = "#05 - Não deve criar sem CEP")]
        public void MustNotCreateCEP()
        {
            var builder = _builder.New();
            builder.Cep = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#05.1 - Deve criar um CEP")]
        public void MustCreateCEP()
        {
            var builder = _builder.New();
            builder.Cep = builder.Cep;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 06 - City
        [Fact(DisplayName = "#06 - Não deve criar sem City")]
        public void MustNotCreateCity()
        {
            var builder = _builder.New();
            builder.City = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#06.1 - Deve criar um City")]
        public void MustCreateCity()
        {
            var builder = _builder.New();
            builder.City = builder.City;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 07 - State
        [Fact(DisplayName = "#07 - Não deve criar sem State")]
        public void MustNotCreateState()
        {
            var builder = _builder.New();
            builder.State = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#07.1 - Deve criar um State")]
        public void MustCreateState()
        {
            var builder = _builder.New();
            builder.State = builder.State;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 08 - ClientId
        [Fact(DisplayName = "#08 - Não deve criar sem ClientId")]
        public void MustNotCreateClientId()
        {
            var builder = _builder.New();
            builder.ClientId = Guid.Empty;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#08.1 - Deve criar um ClientId")]
        public void MustCreateClientId()
        {
            var builder = _builder.New();
            builder.ClientId = builder.ClientId;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Helpers.Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion
    }
}
