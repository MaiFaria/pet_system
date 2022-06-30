using PS.Core.DomainObjects;
using PS.Identity.API.Validations;
using System.ComponentModel.DataAnnotations;

namespace PS.Identity.API.Models
{
    public class UserViewModels
    {
        public class UserRegister : Entity
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            public string? Name { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            public Cpf? Cpf { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
            public Email? Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
            public string? Password { get; set; }

            [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
            public string? PasswordConfirmation { get; set; }

            public async Task ValidateForPersistence()
            {
                ValidationResult = await new UserRegisterValidation().ValidateAsync(this);
            }
        }

        public class UserLogin
        {
            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "O campo {0} é obrigatório")]
            [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
            public string? Password { get; set; }
        }

        public class UserResponseLogin
        {
            public string? AccessToken { get; set; }
            public Guid RefreshToken { get; set; }
            public double? ExpiresIn { get; set; }
            public UserToken? UserToken { get; set; }
        }

        public class UserToken
        {
            public string? Id { get; set; }
            public string? Email { get; set; }
            public IEnumerable<UserClaim>? Claims { get; set; }
        }

        public class UserClaim
        {
            public string? Value { get; set; }
            public string? Type { get; set; }
        }
    }
}
