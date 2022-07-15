using System.Net;
using Microsoft.Extensions.Options;
using PS.Bff.Compras.Extensions;
using PS.Bff.Compras.Models;
using PS.Core.Communication;

namespace PS.Bff.Compras.Services
{
    public interface IOrderService
    {
        Task<ResponseResult> FinishOrder(OrderDTO pedido);
        Task<OrderDTO> GetLastOrder();
        Task<IEnumerable<OrderDTO>> GetListByClientId();

        Task<VoucherDTO> GetVoucherByCode(string codigo);
    }

    public class OrderService : Service, IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.OrderUrl);
        }

        public async Task<ResponseResult> FinishOrder(OrderDTO pedido)
        {
            var pedidoContent = GetContent(pedido);

            var response = await _httpClient.PostAsync("/pedido/", pedidoContent);

            if (!HandleErrorResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<OrderDTO> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("/pedido/ultimo/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandleErrorResponse(response);

            return await DeserializarObjetoResponse<OrderDTO>(response);
        }

        public async Task<IEnumerable<OrderDTO>> GetListByClientId()
        {
            var response = await _httpClient.GetAsync("/pedido/lista-cliente/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandleErrorResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<OrderDTO>>(response);
        }

        public async Task<VoucherDTO> GetVoucherByCode(string codigo)
        {
            var response = await _httpClient.GetAsync($"/voucher/{codigo}/");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            HandleErrorResponse(response);

            return await DeserializarObjetoResponse<VoucherDTO>(response);
        }
    }
}