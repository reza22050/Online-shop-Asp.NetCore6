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
        PaginatedItemsDto<CatalogPLPDto> Execute(int page, int pageSize);
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

        public PaginatedItemsDto<CatalogPLPDto> Execute(int page, int pageSize)
        {
            int rowCount = 0;
            var data = _context.CatalogItems
                .Include(x=>x.Discounts)
                .Include(x => x.CatalogItemImages)
                .Include(x => x.CatalogType)
                .OrderByDescending(x => x.Id)
                .PagedResult(page, pageSize, out rowCount)
                .ToList()
                .Select(x => new CatalogPLPDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Type = x.CatalogType.Type,
                    Rate = 3,
                    Image = _uriComposerServie.ComposerImageUri(x.CatalogItemImages.FirstOrDefault().Src)
                }).ToList();

            return new PaginatedItemsDto<CatalogPLPDto>(page, pageSize, rowCount, data);
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
    }


}
