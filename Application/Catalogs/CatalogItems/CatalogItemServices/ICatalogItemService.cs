using Application.Catalogs.CatalogItems.UriComposer;
using Application.Dtos;
using Application.Interfaces.Contexts;
using AutoMapper;
using Common;
using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.CatalogItems.CatalogItemServices
{
    public interface ICatalogItemService
    {
        List<CatalogBrandDto> GetBrand();
        List<ListCatalogTypeDto> GetCatalogType();


        void AddToMyFavourite(string UserId, int CatalogItemId);
        PaginatedItemsDto<FavouriteCatalogItemDto> GetMyFavourite(string UserId, int page = 1, int pageSize = 20);
    }

    public class CatalogItemService : ICatalogItemService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUriComposerServie _uriComposerServie;

        public CatalogItemService(IDataBaseContext context, IMapper mapper, IUriComposerServie uriComposerServie)
        {
            _context = context;
            _mapper = mapper;
            this._uriComposerServie = uriComposerServie;
        }

        public void AddToMyFavourite(string UserId, int CatalogItemId)
        {
            var catalogItem = _context.CatalogItems.Find(CatalogItemId);
            CatalogItemFavourite catalogItemFavourite = new CatalogItemFavourite()
            {
                CatalogItem = catalogItem,
                UserId = UserId,
            };
            _context.CatalogItemFavourites.Add(catalogItemFavourite);
            _context.SaveChanges();
        }

        public List<CatalogBrandDto> GetBrand()
        {
            var brands = _context.CatalogBrands.OrderBy(x=>x.Brand).Take(500).ToList();
            var data = _mapper.Map<List<CatalogBrandDto>>(brands);
            return data;
        }

        public List<ListCatalogTypeDto> GetCatalogType()
        {
            var types = _context.CatalogTypes
                .Include(x => x.ParentCatalogType)
                .Include(x => x.ParentCatalogType)
                .ThenInclude(x => x.ParentCatalogType.ParentCatalogType)
                .Include(x => x.SubType)
                .Where(x => x.ParentCatalogTypeId != null)
                .Where(x => x.SubType.Count() == 0)
                .Select(x => new { x.Id, x.Type, x.ParentCatalogType, x.SubType }).ToList()
                .Select(x => new ListCatalogTypeDto()
                {
                    Id = x.Id,
                    Type = $"{x?.Type ?? ""} - {x?.ParentCatalogType?.Type ?? ""} - {x?.ParentCatalogType?.ParentCatalogType?.Type ?? ""}"
                }).ToList();

            return types;
            
        }

        public PaginatedItemsDto<FavouriteCatalogItemDto> GetMyFavourite(string UserId, int page = 1, int pageSize = 20)
        {
            var model = _context.CatalogItems.
                Include(x => x.CatalogItemImages)
                .Include(x => x.Discounts)
                .Include(x => x.CatalogItemFavourites)
                .Where(x => x.CatalogItemFavourites.Any(x => x.UserId == UserId))
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            int rowCount = 0;
            var data = model.PagedResult(page, pageSize, out rowCount)
                .ToList()
                .Select(x => new FavouriteCatalogItemDto()
                {
                    Id=x.Id,
                    Name= x.Name,
                    Price=x.Price,
                    Rate = 4,
                    AvailableStock=x.AvailableStock,
                    Image = _uriComposerServie.ComposerImageUri(x.CatalogItemImages.FirstOrDefault().Src),
                }).ToList();
            return new PaginatedItemsDto<FavouriteCatalogItemDto>(page, pageSize, rowCount, data);
        }
    }

    public class CatalogBrandDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }

    public class ListCatalogTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class FavouriteCatalogItemDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }  
        public int Rate { get; set; }
        public int AvailableStock { get; set; }
        public string Name { get; set; }    
        public string Image { get; set; }

    }


}
