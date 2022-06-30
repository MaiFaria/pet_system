using FluentValidation;
using PS.Client.API.Models;
using static PS.Core.Helpers.ResourceString;

namespace PS.Client.API.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(row => row.PublicPlace)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_LOGRADOURO_OBRIGATORIO);

            RuleFor(row => row.Number)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_NUMERO_OBRIGATORIO);

            RuleFor(row => row.District)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_BAIRRO_OBRIGATORIO);

            RuleFor(row => row.Cep)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_CEP_OBRIGATORIO);

            RuleFor(row => row.City)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_CIDADE_OBRIGATORIO);

            RuleFor(row => row.State)
               .NotEmpty()
               .WithMessage(Mensagens.MSG_ESTADO_OBRIGATORIO);

            RuleFor(row => row.ClientId)
              .NotEmpty()
              .WithMessage(Mensagens.MSG_GEN_PESSOAID_INEXISTENTE);
        }
    }
}
