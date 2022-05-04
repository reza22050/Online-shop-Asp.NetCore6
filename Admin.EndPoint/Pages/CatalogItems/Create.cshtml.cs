using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Dtos;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.EndPoint.Pages.CatalogItems
{
    public class CreateModel : PageModel
    {
        private readonly IAddNewCatalogItemService _addNewCatalogItemService;
        private readonly ICatalogItemService _catalogItemService;
        private readonly IImageUploadService _imageUploadService;

        public CreateModel(IAddNewCatalogItemService addNewCatalogItemService, ICatalogItemService catalogItemService, IImageUploadService imageUploadService)
        {
            _addNewCatalogItemService = addNewCatalogItemService;
            _catalogItemService = catalogItemService;
            _imageUploadService = imageUploadService;
        }

        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }
        
        [BindProperty]
        public IList<IFormFile> Files { get; set; }

        [BindProperty]
        public AddNewCatalogItemDto Data { get; set; }
        public void OnGet()
        {
            Categories = new SelectList(_catalogItemService.GetCatalogType(), "Id", "Type");
            Brands = new SelectList(_catalogItemService.GetBrand(), "Id", "Brand");

        }

        public JsonResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(x => x.Errors).ToList();
                return new JsonResult(new BaseDto<int>(false, allErrors.Select(x => x.ErrorMessage).ToList(), 0));
            }

            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                Files.Add(file);
            }

            List<AddNewCatalogItemImage_Dto> images = new List<AddNewCatalogItemImage_Dto>();
            if(Files.Count>0)
            {
                //upload
                var result = _imageUploadService.Upload((List<IFormFile>)Files);
                foreach (var item in result)
                {
                    images.Add(new AddNewCatalogItemImage_Dto { Src = item });
                }
            }
            
            Data.Images = images;

            var resultSertvice = _addNewCatalogItemService.Execute(Data);
            return new JsonResult(resultSertvice);
        }


    }
}
