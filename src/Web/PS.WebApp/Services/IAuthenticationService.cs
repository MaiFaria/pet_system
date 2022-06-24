using PS.WebApp.Models;

namespace PS.WebApp.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponseLogin> Login(UserLogin usuarioLogin);

        Task<UserResponseLogin> Register(UserRegister usuarioRegistro);
    }
}
