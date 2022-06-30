using FluentValidation;
using PS.Core.Messages;

namespace PS.Client.API.Applications.Commands
{
    public class AddAddressCommand : Command
    {
        public Guid ClientId { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public AddAddressCommand()
        {
        }

        public AddAddressCommand(Guid clienteId, string publicPlace, string number, string complement,
            string district, string cep, string city, string state)
        {
            AggregateId = clienteId;
            ClientId = clienteId;
            PublicPlace = publicPlace;
            Number = number;
            Complement = complement;
            District = district;
            Cep = cep;
            City = city;
            State = state;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddressValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AddressValidation : AbstractValidator<AddAddressCommand>
        {
            public AddressValidation()
            {
                RuleFor(c => c.PublicPlace)
                    .NotEmpty()
                    .WithMessage("Informe o Logradouro");

                RuleFor(c => c.Number)
                    .NotEmpty()
                    .WithMessage("Informe o Número");

                RuleFor(c => c.Cep)
                    .NotEmpty()
                    .WithMessage("Informe o CEP");

                RuleFor(c => c.District)
                    .NotEmpty()
                    .WithMessage("Informe o Bairro");

                RuleFor(c => c.City)
                    .NotEmpty()
                    .WithMessage("Informe o Cidade");

                RuleFor(c => c.State)
                    .NotEmpty()
                    .WithMessage("Informe o Estado");
            }
        }
    }
}