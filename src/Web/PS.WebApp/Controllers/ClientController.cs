using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.WebApp.Models;
using PS.WebApp.Services;

namespace PS.WebApp.Controllers
{
    [Authorize]
    public class ClientController : MainController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            this._clientService = clientService;
        }

        public async Task<IActionResult> NewAddress(AddressViewModel address)
        {
            var response = await _clientService.AddAddress(address);

            if (ResponseHasErrors(response)) TempData["Erros"] =
                    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

            return RedirectToAction("EnderecoEntrega", "Pedido");
        }
    }
}
