using Microsoft.AspNetCore.Mvc;
using PS.WebApp.Services;

namespace PS.WebApp.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogService _catalogoService;

        public CatalogController(ICatalogService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var produtos = await _catalogoService.GetAll(ps, page, q);
            ViewBag.Pesquisa = q;
            produtos.ReferenceAction = "Index";

            return View(produtos);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var produto = await _catalogoService.GetById(id);

            return View(produto);
        }
    }
}