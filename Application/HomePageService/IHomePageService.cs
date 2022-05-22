using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using Domain.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HomePageService
{
    public interface IHomePageService
    {
        HomePageDto GetData();
    }

    public class HomePageService : IHomePageService
    {
        private readonly IDataBaseContext _context;
        private readonly IUriComposerServie _uriComposerService;
        private readonly IGetCatalogItemPLPService _getCatalogItemPLPService;

        public HomePageService(IDataBaseContext context, IUriComposerServie uriComposerService, IGetCatalogItemPLPService getCatalogItemPLPService)
        {
            this._context = context;
            this._uriComposerService = uriComposerService;
            this._getCatalogItemPLPService = getCatalogItemPLPService;
        }
        public HomePageDto GetData()
        {
            var banners = _context.Banners.Where(x => x.IsActive == true)
               .OrderBy(x => x.Priority)
               .ThenByDescending(x => x.Id)
               .Select(x => new BannerDto()
               {
                   Id = x.Id,
                   Image =_uriComposerService.ComposerImageUri(x.Image),
                   Link = x.Link,
                   Position = x.Position,
               }).ToList();

            var BestSelling = _getCatalogItemPLPService.Execute(new CatalogPLPRequestDto()
            {
                AvailableStock = true,
                page= 1,
                pageSize = 20,
                SoryType = SoryType.BestSelling
            }).Data.ToList();

            var MostPopular = _getCatalogItemPLPService.Execute(new CatalogPLPRequestDto()
            {
                AvailableStock = true,
                page = 1,
                pageSize = 20,
                SoryType = SoryType.MostPopular
            }).Data.ToList();

            return new HomePageDto()
            {
                Banners = banners,
                BestSellers = BestSelling,
                MostPopular = MostPopular
            };

        }
    }

    public class HomePageDto
    {
        public List<BannerDto> Banners { get; set; }
        
        public  List<CatalogPLPDto> MostPopular { get; set; }
        public  List<CatalogPLPDto> BestSellers { get; set; }
    }


    public class BannerDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public Position Position { get; set; }
    }
}
