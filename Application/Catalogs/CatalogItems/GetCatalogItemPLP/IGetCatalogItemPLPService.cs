using Application.Catalogs.CatalogItems.UriComposer;
using Application.Dtos;
using Application.Interfaces.Contexts;
using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.CatalogItems.GetCatalogItemPLP
{
    public interface IGetCatalogItemPLPService
    {
        PaginatedItemsDto<CatalogPLPDto> Execute(CatalogPLPRequestDto request);
    }

    public class GetCatalogItemPLPService : IGetCatalogItemPLPService
    {
        private readonly IDataBaseContext _context;
        private readonly IUriComposerServie _uriComposerServie;

        public GetCatalogItemPLPService(IDataBaseContext context, IUriComposerServie uriComposerServie)
        {
            _context = context;
            _uriComposerServie = uriComposerServie;
        }

        public PaginatedItemsDto<CatalogPLPDto> Execute(CatalogPLPRequestDto request)
        {
            int rowCount = 0;
            var query = _context.CatalogItems
                .Include(x => x.Discounts)
                .Include(x => x.CatalogItemImages)
                .Include(x => x.CatalogType)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (request.brandId != null)
            {
                query = query.Where(p => request.brandId.Any(b => b == p.CatalogBrandId));
            }

            if (request.CatalogTypeId != null)
            {
                query = query.Where(p => p.CatalogTypeId == request.CatalogTypeId);
            }

            if(!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(x=>x.Name.Contains(request.SearchKey) || x.Description.Contains(request.SearchKey));
            }    

            if(request.AvailableStock == true)
            {
                query.Where(x => x.AvailableStock > 0);
            }


            if(request.SoryType == SoryType.BestSelling)
            {
                query = query.Include(x => x.OrderItems)
                    .OrderByDescending(x => x.OrderItems.Count());

            }

            if (request.SoryType == SoryType.MostPopular)
            {
                query = query.Include(x => x.CatalogItemFavourites)
                    .OrderByDescending(x => x.CatalogItemFavourites.Count());
            }

            if (request.SoryType == SoryType.MostVisited)
            {
                query = query.OrderByDescending(x => x.VisitCount);
            }

            if (request.SoryType == SoryType.Newest)
            {
                query = query.OrderByDescending(x => x.Id);
            }

            if (request.SoryType == SoryType.Cheapest)
            {
                query = query
                    .Include(x=>x.Discounts)
                    .OrderBy(x => x.Price);
            }

            if (request.SoryType == SoryType.MostExpensive)
            {
                query = query
                    .Include(x => x.Discounts)
                    .OrderByDescending(x => x.Price);
            }

            var data = query.PagedResult(request.page, request.pageSize, out rowCount)
                .ToList()
                .Select(x => new CatalogPLPDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Type = x.CatalogType.Type,
                    Rate = 3,
                    Image = _uriComposerServie.ComposerImageUri(x.CatalogItemImages.FirstOrDefault().Src),
                    AvailableStock = x.AvailableStock
                }).ToList();

            return new PaginatedItemsDto<CatalogPLPDto>(request.page, request.pageSize, rowCount, data);
        }
    }

    public class CatalogPLPDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public byte Rate { get; set; }
        public int AvailableStock { get; set; }
    }

    public class CatalogPLPRequestDto
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;

        public int? CatalogTypeId { get; set; }
        public int[] brandId { get; set; }
        public bool AvailableStock { get; set; }

        public string SearchKey { get; set; }

        public SoryType SoryType { get; set; }
    }


    public enum SoryType
    {
        None = 0,
        MostVisited = 1,
        BestSelling = 2,
        MostPopular = 3,
        Newest = 4,
        Cheapest = 5,
        MostExpensive = 6

    }

}
