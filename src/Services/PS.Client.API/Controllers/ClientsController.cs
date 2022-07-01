using Microsoft.AspNetCore.Mvc;
using PS.Client.API.Applications.Commands;
using PS.Client.API.Models;
using PS.Core.Mediator;
using PS.WebApi.Core.Controllers;
using PS.WebApi.Core.User;

namespace PS.Client.API.Controllers
{
    public class ClientsController : MainController
    {
        private readonly IClienteRepository _clientRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;

        public ClientsController(IClienteRepository clientRepository, IMediatorHandler mediator, IAspNetUser user)
        {
            _clientRepository = clientRepository;
            _mediator = mediator;
            _user = user;
        }

        [HttpGet("cliente/endereco")]
        public async Task<IActionResult> GetAddress()
        {
            var endereco = await _clientRepository.GetAddressById(_user.GetUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }

        [HttpPost("cliente/endereco")]
        public async Task<IActionResult> AddAddress(AddAddressCommand adress)
        {
            adress.ClientId = _user.GetUserId();
            return CustomResponse(await _mediator.SendCommand(adress));
        }
    }
}
