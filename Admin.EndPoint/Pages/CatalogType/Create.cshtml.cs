using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.CatalogType
{
    public class CreateModel : PageModel
    {
        private readonly ICatalogTypeService _categoryTypeService;
        private readonly IMapper _mapper;

        public CreateModel(ICatalogTypeService categoryTypeService, IMapper mapper)
        {
            _categoryTypeService = categoryTypeService;
            _mapper = mapper;
        }

        [BindProperty]
        public CatalogTypeViewModel CatalogType { get; set; } = new CatalogTypeViewModel { };
        public List<string> Messages { get; set; } = new List<string>();

        public void OnGet(int? parentId)
        {
            CatalogType.ParentCatalogTypeId = parentId; 
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var model = _mapper.Map<CatalogTypeDto>(CatalogType);
            var result = _categoryTypeService.Add(model);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index", new { parentid = CatalogType.ParentCatalogTypeId });
            }
            Messages = result.Message;
            return Page();
        }

    }
}
