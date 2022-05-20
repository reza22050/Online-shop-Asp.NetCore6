using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.CatalogItems.GetCatalogItemPDP
{
    public interface IGetCatalogItemPDPService
    {
        GetCatalogItemPDPDto Execute(int Id);
    }

    public class GetCatalogItemPDPService : IGetCatalogItemPDPService
    {
        private readonly IDataBaseContext _context;
        private readonly IUriComposerServie _uriComposerServie;

        public GetCatalogItemPDPService(IDataBaseContext context, IUriComposerServie uriComposerServie)
        {
            _context = context;
            _uriComposerServie = uriComposerServie;
        }
        public GetCatalogItemPDPDto Execute(int Id)
        {
            var catalogItem = _context.CatalogItems
                .Include(x=>x.CatalogItemFeatures)
                .Include(x=>x.CatalogItemImages)
                .Include(x=>x.CatalogType)
                .Include(x=>x.CatalogBrand)
                .Include(x=>x.Discounts)
                .SingleOrDefault(x=>x.Id == Id);

            var feature = catalogItem.CatalogItemFeatures.Select(x => new PDPFeatureDto()
            {
                Group = x.Group,
                Key = x.Key,
                Value = x.Value, 
            }).ToList().GroupBy(x => x.Group);

            var similarCatalogItems = _context.CatalogItems
                .Include(x => x.CatalogItemImages)
                .Where(x => x.CatalogTypeId == catalogItem.CatalogTypeId && x.Id!=Id)
                .Take(10)
                .Select(x => new SimilarCatalogItemDto()
                {
                    Id = x.Id,
                    Images= _uriComposerServie.ComposerImageUri(x.CatalogItemImages.FirstOrDefault().Src),
                    Price = x.Price,
                    Name = x.Name,
                    Rate = 4, 
                    Type = x.CatalogType.Type

                }).ToList();

            return new GetCatalogItemPDPDto()
            {
                Features = feature,
                similarCatalogs = similarCatalogItems,
                Brand = catalogItem.CatalogBrand.Brand,
                Description = catalogItem.Description,
                Name = catalogItem.Name,
                Price = catalogItem.Price,
                Id = catalogItem.Id,
                Type = catalogItem.CatalogType.Type,
                Images = catalogItem.CatalogItemImages.Select(x=> _uriComposerServie.ComposerImageUri(x.Src)).ToList(),
                OldPrice = catalogItem.OldPrice,
                PercentDiscount = catalogItem.PercentDiscount,


            };
        }
    }

    public class GetCatalogItemPDPDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int? PercentDiscount { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }
        public IEnumerable<IGrouping<string, PDPFeatureDto>> Features { get; set; }
        public List<SimilarCatalogItemDto> similarCatalogs { get; set; }

    }

    public class PDPFeatureDto
    {
        public string Group { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }


    public class SimilarCatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Images { get; set; }
        public byte Rate { get; set; }
        public string Type { get; set; }

    }

}
