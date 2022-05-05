using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly GetCatalogItemPLPService _getCatalogItemPLPService;

        public ProductController(GetCatalogItemPLPService getCatalogItemPLPService)
        {
            _getCatalogItemPLPService = getCatalogItemPLPService;
        }
        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var data = _getCatalogItemPLPService.Execute(page, pageSize);

            return View(data);
        }
    }
}
