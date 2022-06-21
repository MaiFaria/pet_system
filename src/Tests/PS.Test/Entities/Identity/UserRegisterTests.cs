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

        #region 01 - Nome
        [Fact(DisplayName = "#01 - Não Deve criar sem um Nome")]
        public void MustCreateName()
        {
            var builder = _builder.New();
            builder.Name = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 02 - CPF
        [Fact(DisplayName = "#02 - Não Deve criar sem um CPF")]
        public void MustCreateCPF()
        {
            var builder = _builder.New();
            builder.Cpf = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 03 - E-mail
        [Fact(DisplayName = "#03 - Não Deve criar sem um E-mail")]
        public void MustCreateEmail()
        {
            var builder = _builder.New();
            builder.Email = null;
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion

        #region 04 - Password
        [Fact(DisplayName = "#04 - Não Deve criar sem um Password com tamanho menor que o 6 dígitos")]
        public void MustCreateShorterPassword()
        {
            var builder = _builder.New();
            builder.Password = "cinco";
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }

        [Fact(DisplayName = "#04.01 - Não Deve criar um Password com senhas diferentes")]
        public void MustCreateDifferentPassword()
        {
            var builder = _builder.New();
            builder.PasswordConfirmation = "diferente";
            var model = _builder.Build();
            model.ValidateForPersistence().Wait();

            Assert.False(model.IsValid, string.Join(Environment.NewLine, model.ValidationResult.Errors), _output, model);
        }
        #endregion
    }
}
