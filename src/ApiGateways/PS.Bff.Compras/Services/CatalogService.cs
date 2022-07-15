using Microsoft.Extensions.Options;
using PS.Bff.Compras.Extensions;
using PS.Bff.Compras.Models;

namespace PS.Bff.Compras.Services
{
    public interface ICatalogService
    {
        Task<ProductItemDTO> GetById(Guid id);
        Task<IEnumerable<ProductItemDTO>> GetItens(IEnumerable<Guid> ids);
    }

    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
        }

        public async Task<ProductItemDTO> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            HandleErrorResponse(response);

            return await DeserializarObjetoResponse<ProductItemDTO>(response);
        }

        public async Task<IEnumerable<ProductItemDTO>> GetItens(IEnumerable<Guid> ids)
        {
            var idsRequest = string.Join(",", ids);

            var response = await _httpClient.GetAsync($"/catalogo/produtos/lista/{idsRequest}/");

            HandleErrorResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<ProductItemDTO>>(response);
        }
    }
}