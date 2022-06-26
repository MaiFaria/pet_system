using FluentValidation;
using static PS.Core.Helpers.ResourceString;
using static PS.Identity.API.Models.UserViewModels;

namespace PS.Identity.API.Validations
{
    public class UserRegisterValidation : AbstractValidator<UserRegister>
    {
        public UserRegisterValidation()
        {
            RuleFor(row => row.Name)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_NOME_OBRIGATORIO);

            RuleFor(row => row.Cpf)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_CPF_OBRIGATORIO);

            RuleFor(row => row.Email)
                .NotEmpty()
                .WithMessage(Mensagens.MSG_EMAIL_OBRIGATORIO);

            RuleFor(row => row.Password)
                .NotEmpty()
                .Length(6, 10)
                .WithMessage(string.Format(Mensagens.MSG_GEN_TAMANHO_CAMPO, Campos.PASSWORD, 6, 10));

            RuleFor(row => row)
                .Custom((row, contexto) =>
                {
                    if (row.PasswordConfirmation != row.Password)
                        contexto.AddFailure(Mensagens.MSG_SENHA_NAO_CONFERE);
                });
        }
    }
}
