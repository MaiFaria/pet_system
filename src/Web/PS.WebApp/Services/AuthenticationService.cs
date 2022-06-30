using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using PS.Core.Communication;
using PS.WebApi.Core.User;
using PS.WebApp.Extensions;
using PS.WebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PS.WebApp.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);

        Task<UserResponseLogin> Register(UserRegister userRegister);

        Task LogIn(UserResponseLogin response);
        Task Logout();

        bool ExpiredToken();

        Task<bool> RefreshValidToken();
    }

    public class AuthenticationService : Service, IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IAspNetUser _user;
        private readonly Microsoft.AspNetCore.Authentication.IAuthenticationService _authenticationService;

        public AuthenticationService(HttpClient httpClient,
                                   IOptions<AppSettings> settings,
                                   IAspNetUser user,
                                   Microsoft.AspNetCore.Authentication.IAuthenticationService authenticationService)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl);
            _httpClient = httpClient;
            _user = user;
            _authenticationService = authenticationService;
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

        public async Task<UserResponseLogin> UseRefreshToken(string refreshToken)
        {
            var refreshTokenContent = GetContent(refreshToken);

            var response = await _httpClient.PostAsync("/api/identidade/refresh-token", refreshTokenContent);

            if (!HandleErrorResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializeObjectResponse<UserResponseLogin>(response);
        }

        public async Task LogIn(UserResponseLogin response)
        {
            var token = GetFormattedToken(response.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AccessToken));
            claims.Add(new Claim("RefreshToken", response.RefreshToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true
            };

            await _authenticationService.SignInAsync(
                _user.GetHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task Logout()
        {
            await _authenticationService.SignOutAsync(
                _user.GetHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                null);
        }

        public static JwtSecurityToken? GetFormattedToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }

        public bool ExpiredToken()
        {
            var jwt = _user.GetUserToken();
            if (jwt is null) return false;

            var token = GetFormattedToken(jwt);
            return token.ValidTo.ToLocalTime() < DateTime.Now;
        }

        public async Task<bool> RefreshValidToken()
        {
            var response = await UseRefreshToken(_user.GetUserRefreshToken());

            if (response.AccessToken != null && response.ResponseResult == null)
            {
                await LogIn(response);
                return true;
            }

            return false;
        }
    }
}
