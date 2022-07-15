using Microsoft.Extensions.Options;
using PS.Core.Communication;
using PS.WebApp.Extensions;
using PS.WebApp.Models;

namespace PS.WebApp.Services
{
    public interface IComprasBffService
    {
        // Carrinho
        Task<CartViewModel> GetCart();
        Task<int> GetQuantityCart();
        Task<ResponseResult> AddItemCart(CartItemViewModel cart);
        Task<ResponseResult> UpdateItemCart(Guid prroductId, CartItemViewModel cart);
        Task<ResponseResult> RemoveItemCart(Guid prroductId);
        Task<ResponseResult> ApplyVoucherCart(string voucher);

        // Pedido
        Task<ResponseResult> FinalizeOrder(OrderTransacaoViewModel OrderTransaction);
        Task<OrderViewModel> GetLastOrder();
        Task<IEnumerable<OrderViewModel>> GetListById();
        OrderTransacaoViewModel MapToOrder(CartViewModel cart, AddressViewModel address);
    }

    public class ComprasBffService : Service, IComprasBffService
    {
        private readonly HttpClient _httpClient;

        public ComprasBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ComprasBffUrl);
        }

        #region Carrinho

        public async Task<CartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho/");

            HandleErrorResponse(response);

            return await DeserializeObjectResponse<CartViewModel>(response);
        }
        public async Task<int> GetQuantityCart()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho-quantidade/");

            HandleErrorResponse(response);

            return await DeserializeObjectResponse<int>(response);
        }
        public async Task<ResponseResult> AddItemCart(CartItemViewModel carrinho)
        {
            var itemContent = GetContent(carrinho);

            var response = await _httpClient.PostAsync("/compras/carrinho/items/", itemContent);

            if (!HandleErrorResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
        public async Task<ResponseResult> UpdateItemCart(Guid produtoId, CartItemViewModel item)
        {
            var itemContent = GetContent(item);

            var response = await _httpClient.PutAsync($"/compras/carrinho/items/{produtoId}", itemContent);

            if (!HandleErrorResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
        public async Task<ResponseResult> RemoveItemCart(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/compras/carrinho/items/{produtoId}");

            if (!HandleErrorResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
        public async Task<ResponseResult> ApplyVoucherCart(string voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync("/compras/carrinho/aplicar-voucher/", itemContent);

            if (!HandleErrorResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        #endregion

        #region Pedido

        public async Task<ResponseResult> FinalizeOrder(OrderTransacaoViewModel pedidoTransacao)
        {
            var pedidoContent = GetContent(pedidoTransacao);

            var response = await _httpClient.PostAsync("/compras/pedido/", pedidoContent);

            if (!HandleErrorResponse(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<OrderViewModel> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("/compras/pedido/ultimo/");

            HandleErrorResponse(response);

            return await DeserializeObjectResponse<OrderViewModel>(response);
        }

        public async Task<IEnumerable<OrderViewModel>> GetListById()
        {
            var response = await _httpClient.GetAsync("/compras/pedido/lista-cliente/");

            HandleErrorResponse(response);

            return await DeserializeObjectResponse<IEnumerable<OrderViewModel>>(response);
        }

        public OrderTransacaoViewModel MapToOrder(CartViewModel cart, AddressViewModel address)
        {
            var pedido = new OrderTransacaoViewModel
            {
                TotalValue = cart.TotalValue,
                Itens = cart.Itens,
                Discount = cart.Discount,
                VoucherUsed = cart.VoucherUsed,
                VoucherCode = cart.Voucher?.Code
            };

            if (address != null)
            {
                pedido.Address = new AddressViewModel
                {
                    PublicPlace = address.PublicPlace,
                    Number = address.Number,
                    District = address.District,
                    Cep = address.Cep,
                    Complement = address.Complement,
                    City = address.City,
                    State = address.State
                };
            }

            return pedido;
        }

        #endregion
    }
}