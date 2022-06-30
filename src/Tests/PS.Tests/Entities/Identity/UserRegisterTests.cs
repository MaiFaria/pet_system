using PS.Tests.Builders.Identity;
using Xunit;
using Xunit.Abstractions;
using Assert = PS.Tests.Helpers.Assert;

namespace PS.Tests.Entities.Identity
{
    public class UserRegisterTests
    {
        private readonly UserRegisterBuilder _builder;
        private readonly ITestOutputHelper _output;

        public UserRegisterTests(ITestOutputHelper output)
        {
            _builder = new UserRegisterBuilder();
            _output = output;
        }

        [Fact(DisplayName = "#0 - Deve criar um model de UserRegister")]
        public void MustCreateUserRegister()
        {
            var builder = _builder.New();
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();
            Helpers.Assert.Equal<PS.Client.API.Models.Client>(builder, model, _output);
        }

        #region 01 - Name
        [Fact(DisplayName = "#01 - Não Deve criar sem um Name")]
        public void MustNotCreateName()
        {
            var builder = _builder.New();
            builder.Name = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#01.1 - Deve criar com name válido")]
        public void MustCreateName()
        {
            var builder = _builder.New();
            builder.Name = _builder.Name;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 02 - CPF
        [Fact(DisplayName = "#02 - Não Deve criar sem um CPF")]
        public void MustNotCreateCPF()
        {
            var builder = _builder.New();
            builder.Cpf = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#02.1 - Deve criar um CPF válido")]
        public void MustCreateCPF()
        {
            var builder = _builder.New();
            builder.Cpf = _builder.Cpf;
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

        [Fact(DisplayName = "#03.1 -  Deve criar um E-mail válido")]
        public void MustCreateEmail()
        {
            var builder = _builder.New();
            builder.Email = _builder.Email;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 04 - Password
        [Fact(DisplayName = "#04 - Não Deve criar sem um Password com tamanho menor que o 6 dígitos")]
        public void MustNotCreateShorterPassword()
        {
            var builder = _builder.New();
            builder.Password = "cinco";
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#04.1 - Deve criar um Password válido")]
        public void MustCreateShorterPassword()
        {
            var builder = _builder.New();
            builder.Password = _builder.Password;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#04.2 - Não Deve criar um Password Confirmation com senhas diferentes")]
        public void MustNotCreateDifferentPassword()
        {
            var builder = _builder.New();
            builder.PasswordConfirmation = "diferente";
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#04.3 - Deve criar um Password Confirmation válido")]
        public void MustCreateDifferentPassword()
        {
            var builder = _builder.New();
            builder.PasswordConfirmation = _builder.PasswordConfirmation;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 05 - Id
        [Fact(DisplayName = "#05 - Não Deve criar sem um Id")]
        public void MustNotCreateShorterId()
        {
            var builder = _builder.New();
            builder.Id = Guid.Empty;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#05.1 - Deve criar um Id")]
        public void MustCreateShorterId()
        {
            var builder = _builder.New();
            builder.Id = _builder.Id;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.True(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion
    }
}
