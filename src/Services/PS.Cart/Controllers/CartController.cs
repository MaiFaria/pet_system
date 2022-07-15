using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS.Cart.Data;
using PS.Cart.Models;
using PS.WebApi.Core.Controllers;
using PS.WebApi.Core.User;

namespace PS.Cart.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _context;

        public CartController(IAspNetUser user, CartContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("carrinho")]
        public async Task<CustomerCart> GetCart()
        {
            return await GetCustomerCart() ?? new CustomerCart();
        }

        [HttpPost("carrinho")]
        public async Task<IActionResult> AddItemCarrinho(CartItem item)
        {
            var carrinho = await GetCustomerCart();

            if (carrinho == null)
                ManipularNovoCarrinho(item);
            else
                HandleExistingCart(carrinho, item);

            if (!ValidOperation()) return CustomResponse();

            await PersistData();
            return CustomResponse();
        }

        [HttpPut("carrinho/{produtoId}")]
        public async Task<IActionResult> UpdateItemCart(Guid produtoId, CartItem item)
        {
            var carrinho = await GetCustomerCart();
            var itemCarrinho = await GetItemCartValidated(produtoId, carrinho, item);
            if (itemCarrinho == null) return CustomResponse();

            carrinho.AtualizarUnidades(itemCarrinho, item.Quantity);

            ValidateCart(carrinho);
            if (!ValidOperation()) return CustomResponse();

            _context.CartItens.Update(itemCarrinho);
            _context.CustomerCart.Update(carrinho);

            await PersistData();
            return CustomResponse();
        }

        [HttpDelete("carrinho/{produtoId}")]
        public async Task<IActionResult> RemoveItemCart(Guid produtoId)
        {
            var carrinho = await GetCustomerCart();

            var itemCarrinho = await GetItemCartValidated(produtoId, carrinho);
            if (itemCarrinho == null) return CustomResponse();

            ValidateCart(carrinho);
            if (!ValidOperation()) return CustomResponse();

            carrinho.RemoverItem(itemCarrinho);

            _context.CartItens.Remove(itemCarrinho);
            _context.CustomerCart.Update(carrinho);

            await PersistData();
            return CustomResponse();
        }

        [HttpPost]
        [Route("carrinho/aplicar-voucher")]
        public async Task<IActionResult> ApplyVoucher(Voucher voucher)
        {
            var carrinho = await GetCustomerCart();

            carrinho.AplicarVoucher(voucher);

            _context.CustomerCart.Update(carrinho);

            await PersistData();
            return CustomResponse();
        }

        private async Task<CustomerCart> GetCustomerCart()
        {
            return await _context.CustomerCart
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.ClienteId == _user.GetUserId());
        }
        private void ManipularNovoCarrinho(CartItem item)
        {
            var carrinho = new CustomerCart(_user.GetUserId());
            carrinho.AdicionarItem(item);

            ValidateCart(carrinho);
            _context.CustomerCart.Add(carrinho);
        }
        private void HandleExistingCart(CustomerCart carrinho, CartItem item)
        {
            var produtoItemExistente = carrinho.CartExistingItem(item);

            carrinho.AdicionarItem(item);
            ValidateCart(carrinho);

            if (produtoItemExistente)
            {
                _context.CartItens.Update(carrinho.GetProductById(item.ProductId));
            }
            else
            {
                _context.CartItens.Add(item);
            }

            _context.CustomerCart.Update(carrinho);
        }
        private async Task<CartItem> GetItemCartValidated(Guid produtoId, CustomerCart carrinho, CartItem item = null)
        {
            if (item != null && produtoId != item.ProductId)
            {
                AddProcessingError("O item não corresponde ao informado");
                return null;
            }

            if (carrinho == null)
            {
                AddProcessingError("Carrinho não encontrado");
                return null;
            }

            var itemCarrinho = await _context.CartItens
                .FirstOrDefaultAsync(i => i.CartId == carrinho.Id && i.ProductId == produtoId);

            if (itemCarrinho == null || !carrinho.CartExistingItem(itemCarrinho))
            {
                AddProcessingError("O item não está no carrinho");
                return null;
            }

            return itemCarrinho;
        }
        private async Task PersistData()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddProcessingError("Não foi possível persistir os dados no banco");
        }
        private bool ValidateCart(CustomerCart carrinho)
        {
            carrinho.ValidationResult.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
            return false;
        }
    }
}
