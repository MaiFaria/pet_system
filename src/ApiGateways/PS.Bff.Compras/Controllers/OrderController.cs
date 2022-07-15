using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.Bff.Compras.Models;
using PS.Bff.Compras.Services;
using PS.WebApi.Core.Controllers;
using System.Globalization;

namespace PS.Bff.Compras.Controllers
{
    [Authorize]
    public class OrderController : MainController
    {
        private readonly ICatalogService _catalogService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;

        public OrderController(
            ICatalogService catalogService,
            ICartService cartService,
            IOrderService orderService,
            IClientService clientService)
        {
            _catalogService = catalogService;
            _cartService = cartService;
            _orderService = orderService;
            _clientService = clientService;
        }

        [HttpPost]
        [Route("compras/pedido")]
        public async Task<IActionResult> Adicionarorder(OrderDTO order)
        {
            var cart = await _cartService.GetCart();
            var produtos = await _catalogService.GetItens(cart.Itens.Select(p => p.ProductId));
            var endereco = await _clientService.GetAddress();

            if (!await ValidateProductCart(cart, produtos)) return CustomResponse();

            InsertOrderDate(cart, endereco, order);

            return CustomResponse(await _orderService.FinishOrder(order));
        }

        [HttpGet("compras/produto/ultimo")]
        public async Task<IActionResult> LastOrder()
        {
            var order = await _orderService.GetLastOrder();
            if (order is null)
            {
                AddProcessingError("order não encontrado!");
                return CustomResponse();
            }

            return CustomResponse(order);
        }

        [HttpGet("compras/produto/lista-cliente")]
        public async Task<IActionResult> ListByClient()
        {
            var orders = await _orderService.GetListByClientId();

            return orders == null ? NotFound() : CustomResponse(orders);
        }

        private async Task<bool> ValidateProductCart(CartDTO cart, IEnumerable<ProductItemDTO> produtos)
        {
            if (cart.Itens.Count != produtos.Count())
            {
                var itensIndisponiveis = cart.Itens.Select(c => c.ProductId).Except(produtos.Select(p => p.Id)).ToList();

                foreach (var itemId in itensIndisponiveis)
                {
                    var itemCart = cart.Itens.FirstOrDefault(c => c.ProductId == itemId);
                    AddProcessingError($"O item {itemCart.Name} não está mais disponível no catálogo, o remova do cart para prosseguir com a compra");
                }

                return false;
            }

            foreach (var itemCart in cart.Itens)
            {
                var produtoCatalogo = produtos.FirstOrDefault(p => p.Id == itemCart.ProductId);

                if (produtoCatalogo.Value != itemCart.Value)
                {
                    var msgErro = $"O produto {itemCart.Name} mudou de valor (de: " +
                                  $"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", itemCart.Value)} para: " +
                                  $"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", produtoCatalogo.Value)}) desde que foi adicionado ao cart.";

                    AddProcessingError(msgErro);

                    var responseRemover = await _cartService.RemoveCartItem(itemCart.ProductId);
                    if (ResponseHasError(responseRemover))
                    {
                        AddProcessingError($"Não foi possível remover automaticamente o produto {itemCart.Name} do seu cart, _" +
                                                   "remova e adicione novamente caso ainda deseje comprar este item");
                        return false;
                    }

                    itemCart.Value = produtoCatalogo.Value;
                    var responseAdicionar = await _cartService.AddCartItem(itemCart);

                    if (ResponseHasError(responseAdicionar))
                    {
                        AddProcessingError($"Não foi possível atualizar automaticamente o produto {itemCart.Name} do seu cart, _" +
                                                   "adicione novamente caso ainda deseje comprar este item");
                        return false;
                    }

                    ClearProcessingErrors();
                    AddProcessingError(msgErro + " Atualizamos o valor em seu cart, realize a conferência do order e se preferir remova o produto");

                    return false;
                }
            }

            return true;
        }

        private void InsertOrderDate(CartDTO cart, AddressDTO address, OrderDTO order)
        {
            order.VoucherCode = cart.Voucher?.Code;
            order.VoucherUsed = cart.VoucherUsed;
            order.TotalValue = cart.TotalValue;
            order.Discount = cart.Discount;
            order.OrderItens = cart.Itens;

            order.Address = address;
        }
    }
}
