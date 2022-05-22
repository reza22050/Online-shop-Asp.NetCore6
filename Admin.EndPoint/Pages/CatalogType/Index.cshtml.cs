using Application.Catalogs.CatalogTypes;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.CatalogType
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;
        public IndexModel(ICatalogTypeService catalogTypeService)
        {
            _catalogTypeService = catalogTypeService; 
        }

        public PaginatedItemsDto<CatalogTypeListDto> CataloType { get; set; }

        public void OnGet(int? parentId, int pageIndex = 1, int pageSize = 100)
        {
            CataloType = _catalogTypeService.GetList(parentId, pageIndex, pageSize);
        }
    }
}
