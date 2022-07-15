using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.WebApp.Models;
using PS.WebApp.Services;

namespace PS.WebApp.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IComprasBffService _comprasBffService;

        public CartController(IComprasBffService comprasBffService)
        {
            _comprasBffService = comprasBffService;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _comprasBffService.GetCart());
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> AddItemToCart(CartItemViewModel itemCarrinho)
        {
            var resposta = await _comprasBffService.AddItemCart(itemCarrinho);

            if (ResponseHasErrors(resposta)) return View("Index", await _comprasBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> UpdateItemCart(Guid produtoId, int quantidade)
        {
            var item = new CartItemViewModel { ProductId = produtoId, Quantity = quantidade };
            var resposta = await _comprasBffService.UpdateItemCart(produtoId, item);

            if (ResponseHasErrors(resposta)) return View("Index", await _comprasBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> RemoveItemCart(Guid produtoId)
        {
            var resposta = await _comprasBffService.RemoveItemCart(produtoId);

            if (ResponseHasErrors(resposta)) return View("Index", await _comprasBffService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/aplicar-voucher")]
        public async Task<IActionResult> ApllyVoucher(string voucherCodigo)
        {
            var resposta = await _comprasBffService.ApplyVoucherCart(voucherCodigo);

            if (ResponseHasErrors(resposta)) return View("Index", await _comprasBffService.GetCart());

            return RedirectToAction("Index");
        }
    }
}