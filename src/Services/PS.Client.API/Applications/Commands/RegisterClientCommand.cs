using FluentValidation;
using PS.Core.Messages;

namespace PS.Client.API.Applications.Commands
{
    public class RegisterClientCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public RegisterClientCommand(Guid id, string nome, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = nome;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegisterClientValidation : AbstractValidator<RegisterClientCommand>
        {
            public RegisterClientValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do cliente não foi informado");

                RuleFor(c => c.Cpf)
                    .Must(HaveValidCpf)
                    .WithMessage("O CPF informado não é válido.");

                RuleFor(c => c.Email)
                    .Must(HaveValidEmail)
                    .WithMessage("O e-mail informado não é válido.");
            }

            protected static bool HaveValidCpf(string cpf)
            {
                return Core.DomainObjects.Cpf.Validate(cpf);
            }

            protected static bool HaveValidEmail(string email)
            {
                return Core.DomainObjects.Email.Validate(email);
            }
        }
    }
}
