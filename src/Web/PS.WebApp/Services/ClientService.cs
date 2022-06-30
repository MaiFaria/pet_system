using Microsoft.Extensions.Options;
using PS.WebApp.Extensions;
using PS.WebApp.Models;

namespace PS.WebApp.Services
{
    public interface IClientService
    {
        Task<AddressViewModel> GetAddress();
        Task<Core.Communication.ResponseResult> AddAddress(AddressViewModel address);
    }

    public class ClientService : Service, IClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ClientUrl);
        }

        public async Task<Core.Communication.ResponseResult> AddAddress(AddressViewModel address)
        {
            var addressContent = GetContent(address);

            var response = await _httpClient.PostAsync("/cliente/endereco/", addressContent);

            if (!HandleErrorResponse(response)) return await DeserializeObjectResponse<Core.Communication.ResponseResult>(response);

            return ReturnOk();

        }

        public async Task<AddressViewModel> GetAddress()
        {
            var response = await _httpClient.GetAsync("/cliente/endereco/");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            HandleErrorResponse(response);

            return await DeserializeObjectResponse<AddressViewModel>(response);
        }
    }
}
