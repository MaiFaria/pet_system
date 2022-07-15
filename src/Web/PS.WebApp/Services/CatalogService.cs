using Microsoft.Extensions.Options;
using PS.WebApp.Extensions;
using PS.WebApp.Models;

namespace PS.WebApp.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid id);
    }
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);

            _httpClient = httpClient;
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            HandleErrorResponse(response);

            return await DeserializeObjectResponse<ProductViewModel>(response);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/");

            HandleErrorResponse(response);

            return await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
        }
    }
}