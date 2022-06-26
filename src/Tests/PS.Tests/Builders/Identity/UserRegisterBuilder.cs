using Bogus;
using PS.Core.DomainObjects;
using PS.Tests.Helpers;
using static PS.Identity.API.Models.UserViewModels;

namespace PS.Tests.Builders.Identity
{
    public class UserRegisterBuilder
    {
        private readonly Faker? _faker;

        public string? Name { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirmation { get; set; }

        public UserRegisterBuilder()
        {
            _faker = FakerBuilder.New().Build();
        }

        public UserRegisterBuilder New()
        {
            this.Name = _faker.GenerateName();
            this.Cpf = _faker.GenerateCPFString();
            this.Email = _faker.GenerateEmailString();
            this.Password = _faker.GeneratePassword();
            this.PasswordConfirmation = Password;

            return this;
        }

        public UserRegister Build()
        {
            var model = new UserRegister();

            if (this.Name != null)
                model.Name = this.Name;

            if (this.Cpf != null)
                model.Cpf = this.Cpf;

            if (this.Email != null)
                model.Email = this.Email;

            if (this.Password != null)
                model.Password = this.Password;

            if (this.PasswordConfirmation != null)
                model.PasswordConfirmation = this.PasswordConfirmation;

            return model;
        }
    }
}
