using FluentValidation;
using static PS.Core.Helpers.ResourceString;

namespace PS.Client.API.Validations
{
    public class UserClientValidation : AbstractValidator<Models.Client>
    {
        public UserClientValidation()
        {
            RuleFor(row => row.Id)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_ID_OBRIGATORIO);

            RuleFor(row => row.Name)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_NOME_OBRIGATORIO);

            RuleFor(row => row.Email)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_EMAIL_OBRIGATORIO);

            RuleFor(row => row.Cpf)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_CPF_OBRIGATORIO);
        }
    }
}
