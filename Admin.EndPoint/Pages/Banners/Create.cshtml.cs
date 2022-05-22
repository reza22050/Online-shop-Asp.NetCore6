using Application.Banners;
using Infrastructure.CacheHelpers;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

namespace Admin.EndPoint.Pages.Banners
{
    public class CreateModel : PageModel
    {
        private readonly IBannersService _banners;
        private readonly IImageUploadService _imageUploadService;
        private readonly IDistributedCache _cache;

        public CreateModel(IBannersService banners, IImageUploadService imageUploadService, IDistributedCache cache)
        {
            this._banners = banners;
            this._imageUploadService = imageUploadService;
            this._cache = cache;
        }

        [BindProperty]
        public BannerDto Banner { get; set; }

        [BindProperty]
        public IFormFile BannerImage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var result = _imageUploadService.Upload(new List<IFormFile> { BannerImage });

            if (result.Count > 0)
            {
                Banner.Image = result.FirstOrDefault();
                _banners.AddBanner(Banner);

                _cache.Remove(CacheHelper.GenerateHomePageCacheKey());
            }

            return RedirectToPage("Index");
        }
         
    }
}
