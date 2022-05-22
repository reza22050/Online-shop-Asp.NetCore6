using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGetCatalogItemPLPService _getCatalogItemPLPService;
        private readonly IGetCatalogItemPDPService _getCatalogItemPDPService;

        public ProductController(IGetCatalogItemPLPService getCatalogItemPLPService, IGetCatalogItemPDPService getCatalogItemPDPService)
        {
            _getCatalogItemPLPService = getCatalogItemPLPService;
            this._getCatalogItemPDPService = getCatalogItemPDPService;
        }
        public IActionResult Index(CatalogPLPRequestDto catalogPLPRequestDto)
        {
            var data = _getCatalogItemPLPService.Execute(catalogPLPRequestDto);

            return View(data);
        }

        public IActionResult Details(string  Slug)
        {
            var data = _getCatalogItemPDPService.Execute(Slug);

            return View(data);
        }


    }
}
