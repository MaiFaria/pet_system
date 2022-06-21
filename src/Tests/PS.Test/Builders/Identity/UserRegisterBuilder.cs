using Bogus;
using PS.Tests.Helpers;
using static PS.Identity.Models.UserViewModels;
using ValidationResult = FluentValidation.Results.ValidationResult;

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
        public ValidationResult ValidationResult { get; set; }
        public bool IsValid => ValidationResult.IsValid;


        public UserRegisterBuilder()
        {
            _faker = FakerBuilder.New().Build();
        }

        public UserRegisterBuilder New()
        {
            this.Name = _faker.GenerateName();
            this.Cpf = _faker.GenerateCPF();
            this.Email = _faker.GenerateEmail();
            this.Password = _faker.GeneratePassword();
            this.PasswordConfirmation = Password;
            this.ValidationResult = new ValidationResult();

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

            if (this.ValidationResult != null)
                model.ValidationResult = this.ValidationResult;

            return model;
        }
    }
}
