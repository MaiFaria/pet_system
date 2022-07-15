using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.Bff.Compras.Models;
using PS.Bff.Compras.Services;
using PS.WebApi.Core.Controllers;

namespace PS.Bff.Compras.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;

        public CartController(
            ICartService cartService,
            ICatalogService catalogService,
            IOrderService orderService)
        {
            _cartService = cartService;
            _catalogService = catalogService;
            _orderService = orderService;
        }

        [HttpGet]
        [Route("compras/carrinho")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _cartService.GetCart());
        }

        [HttpGet]
        [Route("compras/carrinho-quantity")]
        public async Task<int> GetQuantityCart()
        {
            var quantity = await _cartService.GetCart();
            return quantity?.Itens.Sum(i => i.Quantity) ?? 0;
        }

        [HttpPost]
        [Route("compras/carrinho/items")]
        public async Task<IActionResult> AddItemCart(CartItemDTO productItem)
        {
            var product = await _catalogService.GetById(productItem.ProductId);

            await ValidateItemCart(product, productItem.Quantity, true);
            if (!ValidOperation()) return CustomResponse();

            productItem.Name = product.Name;
            productItem.Value = product.Value;
            productItem.Image = product.Image;

            var resposta = await _cartService.AddCartItem(productItem);

            return CustomResponse(resposta);
        }

        [HttpPut]
        [Route("compras/carrinho/items/{productId}")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, CartItemDTO productItem)
        {
            var product = await _catalogService.GetById(productId);

            await ValidateItemCart(product, productItem.Quantity);
            if (!ValidOperation()) return CustomResponse();

            var resposta = await _cartService.UpdateCartItem(productId, productItem);

            return CustomResponse(resposta);
        }

        [HttpDelete]
        [Route("compras/carrinho/items/{productId}")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {
            var product = await _catalogService.GetById(productId);

            if (product == null)
            {
                AddProcessingError("Produto inexistente!");
                return CustomResponse();
            }

            var resposta = await _cartService.RemoveCartItem(productId);

            return CustomResponse(resposta);
        }

        [HttpPost]
        [Route("compras/carrinho/aplicar-voucher")]
        public async Task<IActionResult> ApplyVoucher([FromBody] string voucherCode)
        {
            var voucher = await _orderService.GetVoucherByCode(voucherCode);
            if (voucher is null)
            {
                AddProcessingError("Voucher inválido ou não encontrado!");
                return CustomResponse();
            }

            var resposta = await _cartService.ApplyVoucherCart(voucher);

            return CustomResponse(resposta);
        }

        private async Task ValidateItemCart(ProductItemDTO product, int quantity, bool adicionarProduct = false)
        {
            if (product == null) AddProcessingError("Produto inexistente!");
            if (quantity < 1) AddProcessingError($"Escolha ao menos uma unidade do produto {product.Name}");

            var carrinho = await _cartService.GetCart();
            var itemCarrinho = carrinho.Itens.FirstOrDefault(p => p.ProductId == product.Id);

            if (itemCarrinho != null && adicionarProduct && itemCarrinho.Quantity + quantity > product.QuantityStock)
            {
                AddProcessingError($"O produto {product.Name} possui {product.QuantityStock} unidades em estoque, você selecionou {quantity}");
                return;
            }

            if (quantity > product.QuantityStock) AddProcessingError($"O produto {product.Name} possui {product.QuantityStock} unidades em estoque, você selecionou {quantity}");
        }
    }
}
