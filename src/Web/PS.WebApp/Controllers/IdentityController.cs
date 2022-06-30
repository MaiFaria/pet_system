using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = PS.WebApp.Services.IAuthenticationService;
using PS.WebApp.Models;

namespace PS.WebApp.Controllers
{
    public class IdentityController : MainController
    {
        private readonly IAuthenticationService _authentication;

        public IdentityController(IAuthenticationService authentication)
        {
            _authentication = authentication;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var response = await _authentication.Register(userRegister);

            if (ResponseHasErrors(response.ResponseResult)) return View(userRegister);

            await _authentication.LogIn(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(userLogin);

            var resposta = await _authentication.Login(userLogin);

            if (ResponseHasErrors(resposta.ResponseResult)) return View(userLogin);

            await _authentication.LogIn(resposta);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
