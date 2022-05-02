using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.CatalogType
{
    public class EditModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;
        private readonly IMapper _mapper;

        public EditModel(ICatalogTypeService catalogTypeService, IMapper mapper)
        {
            _catalogTypeService = catalogTypeService;
            _mapper = mapper;
        }

        [BindProperty]
        public CatalogTypeViewModel CatalogType { get; set; } = new CatalogTypeViewModel();
        public List<string> Message { get; set; } = new List<string>() { };

        public void OnGet(int Id)
        {
            var model = _catalogTypeService.FindById(Id);
            if(model.IsSuccess)
                CatalogType = _mapper.Map<CatalogTypeViewModel>(model.Data);
            Message = model.Message;
        }

        public IActionResult OnPost()
        {
            var model = _mapper.Map<CatalogTypeDto>(CatalogType);
            var result = _catalogTypeService.Edit(model); 
            Message = result.Message;
            CatalogType = _mapper.Map<CatalogTypeViewModel>(result.Data);
            return Page();
        }
    }
}
