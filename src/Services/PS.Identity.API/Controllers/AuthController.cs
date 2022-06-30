using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PS.Core.Messages.Integration;
using PS.Identity.API.Services;
using PS.MessageBus;
using PS.WebApi.Core.Controllers;
using static PS.Identity.API.Models.UserViewModels;

namespace PS.Identity.API.Controllers
{
    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IMessageBus _bus;

        public AuthController(
           AuthenticationService authenticationService,
           IMessageBus bus)
        {
            _authenticationService = authenticationService;
            _bus = bus;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegister.Email.ToString(),
                Email = userRegister.Email.ToString(),
                EmailConfirmed = true
            };

            var result = await _authenticationService.UserManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                var clientResult = await RegisterClient(userRegister);

                if (!clientResult.ValidationResult.IsValid)
                {
                    await _authenticationService.UserManager.DeleteAsync(user);
                    return CustomResponse(clientResult.ValidationResult);
                }

                return CustomResponse(await _authenticationService.GenerateJwt(userRegister.Email.ToString()));
            }

            foreach (var error in result.Errors)
            {
                AddProcessingError(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authenticationService.SignInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password,
                false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authenticationService.GenerateJwt(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AddProcessingError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AddProcessingError("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        private async Task<ResponseMessage> RegisterClient(UserRegister userRegister)
        {
            var user = await _authenticationService.UserManager.FindByEmailAsync(userRegister.Email.ToString());

            var registeredUser = new RegisteredUserIntegrationEvent(
                Guid.Parse(user.Id), userRegister.Name, userRegister.Email.ToString(), userRegister.Cpf.ToString());

            try
            {
                return await _bus.RequestAsync<RegisteredUserIntegrationEvent, ResponseMessage>(registeredUser);
            }
            catch
            {
                await _authenticationService.UserManager.DeleteAsync(user);
                throw;
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AddProcessingError("Refresh Token inválido");
                return CustomResponse();
            }

            var token = await _authenticationService.GetRefreshToken(Guid.Parse(refreshToken));

            if (token is null)
            {
                AddProcessingError("Refresh Token expirado");
                return CustomResponse();
            }

            return CustomResponse(await _authenticationService.GenerateJwt(token.Username));
        }
    }
}
