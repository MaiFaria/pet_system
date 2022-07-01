using Microsoft.AspNetCore.Mvc;
using PS.Catalog.API.Models;
using PS.WebApi.Core.Controllers;

namespace PS.Catalog.API.Controllers
{
    public class CatalogController : MainController
    {
        private readonly IProductRepository _produtoRepository;

        public CatalogController(IProductRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("catalogo/produtos")]
        public async Task<PagedResult<Product>> Index([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return await _produtoRepository.GetAll(ps, page, q);
        }

        [HttpGet("catalogo/produtos/{id}")]
        public async Task<Product> ProductDetail(Guid id)
        {
            return await _produtoRepository.GetById(id);
        }

        [HttpGet("catalogo/produtos/lista/{ids}")]
        public async Task<IEnumerable<Product>> GetProductById(string ids)
        {
            return await _produtoRepository.GetProductById(ids);
        }
    }
}