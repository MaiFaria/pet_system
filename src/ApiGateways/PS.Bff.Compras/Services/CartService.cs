using Microsoft.Extensions.Options;
using PS.Bff.Compras.Extensions;
using PS.Bff.Compras.Models;
using PS.Core.Communication;

namespace PS.Bff.Compras.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetCart();
        Task<ResponseResult> AddCartItem(CartItemDTO product);
        Task<ResponseResult> UpdateCartItem(Guid productId, CartItemDTO cart);
        Task<ResponseResult> RemoveCartItem(Guid productId);
        Task<ResponseResult> ApplyVoucherCart(VoucherDTO voucher);
    }

    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<CartDTO> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            HandleErrorResponse(response);

            return await DeserializarObjetoResponse<CartDTO>(response);
        }

        public async Task<ResponseResult> AddCartItem(CartItemDTO product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PostAsync("/cart/", itemContent);

            if (!HandleErrorResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, CartItemDTO cart)
        {
            var itemContent = GetContent(cart);

            var response = await _httpClient.PutAsync($"/cart/{cart.ProductId}", itemContent);

            if (!HandleErrorResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> RemoveCartItem(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");

            if (!HandleErrorResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> ApplyVoucherCart(VoucherDTO voucher)
        {
            var itemContent = GetContent(voucher);

            var response = await _httpClient.PostAsync("/cart/aplicar-voucher/", itemContent);

            if (!HandleErrorResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}