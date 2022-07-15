using Microsoft.Extensions.Options;
using PS.Core.Communication;
using PS.WebApp.Extensions;
using PS.WebApp.Models;

namespace PS.WebApp.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);

        Task<UserResponseLogin> Register(UserRegister userRegister);
    }

    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient,
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl);
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

            if (!HandleErrorResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registerContent);

            if (!HandleErrorResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<UserResponseLogin>(response);
        }
    }
}
