using System.Net;
using Microsoft.Extensions.Options;
using PS.Bff.Compras.Extensions;
using PS.Bff.Compras.Models;

namespace PS.Bff.Compras.Services
{
    public interface IClientService
    {
        Task<AddressDTO> GetAddress();
    }

    public class ClientService : Service, IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ClientUrl);
        }

        public async Task<AddressDTO> GetAddress()
        {
            var response = await _httpClient.GetAsync("/cliente/endereco/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandleErrorResponse(response);

            return await DeserializarObjetoResponse<AddressDTO>(response);
        }
    }
}