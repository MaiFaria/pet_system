using PS.Tests.Builders.Client;
using Xunit;
using Xunit.Abstractions;
using Assert = PS.Tests.Helpers.Assert;

namespace PS.Tests.Entities.Client
{
    public class ClientTests
    {
        private readonly ClientBuilder _builder;
        private readonly ITestOutputHelper _output;

        public ClientTests(ITestOutputHelper output)
        {
            _builder = new ClientBuilder();
            _output = output;
        }

        [Fact(DisplayName = "#0 - Deve criar um model de Client")]
        public void MustCreateAddressModel()
        {
            var builder = _builder.New();
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();
            Helpers.Assert.Equal<PS.Client.API.Models.Client>(builder, model, _output);
        }

        #region 01 - Id
        [Fact(DisplayName = "#01 - Não Deve criar sem um Id")]
        public void MustNotCreateId()
        {
            var builder = _builder.New();
            builder.Id = Guid.Empty;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#01.1 - Deve criar com Id")]
        public void MustCreateId()
        {
            var builder = _builder.New();
            builder.Id = _builder.Id;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 02 - Name
        [Fact(DisplayName = "#02 - Não Deve criar sem um Nome")]
        public void MustNotCreateName()
        {
            var builder = _builder.New();
            builder.Name = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#02.1 - Deve criar com nome válido")]
        public void MustCreateName()
        {
            var builder = _builder.New();
            builder.Name = _builder.Name;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 03 - E-mail
        [Fact(DisplayName = "#03 - Não Deve criar sem um E-mail")]
        public void MustNotCreateEmail()
        {
            var builder = _builder.New();
            builder.Email = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#03.1 - Deve criar com nome E-mail")]
        public void MustCreateEmail()
        {
            var builder = _builder.New();
            builder.Email = _builder.Email;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 04 - CPF
        [Fact(DisplayName = "#04 - Não Deve criar sem um CPF")]
        public void MustNotCreateCpf()
        {
            var builder = _builder.New();
            builder.Cpf = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#04.1 - Deve criar com nome CPF")]
        public void MustCreateCpf()
        {
            var builder = _builder.New();
            builder.Cpf = _builder.Cpf;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 05 -Exclused
        [Fact(DisplayName = "#05 - Deve criar com Exclusão de cadastro false")]
        public void MustNotCreateExclused()
        {
            var builder = _builder.New();
            builder.Exclused = false;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#5.1 - Deve criar com Exclusão de cadastro true")]
        public void MustCreateExclused()
        {
            var builder = _builder.New();
            builder.Exclused = true;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion
    }
}
