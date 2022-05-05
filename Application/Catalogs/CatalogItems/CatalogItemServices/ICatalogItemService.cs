using Application.Dtos;
using Application.Interfaces.Contexts;
using AutoMapper;
using Common;
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
        PaginatedItemsDto<CatalogItemListDto> GetCatalogList(int page, int pageSize);
    }

    public class CatalogItemService : ICatalogItemService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public CatalogItemService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<CatalogBrandDto> GetBrand()
        {
            var brands = _context.CatalogBrands.OrderBy(x=>x.Brand).Take(500).ToList();
            var data = _mapper.Map<List<CatalogBrandDto>>(brands);
            return data;
        }

        public PaginatedItemsDto<CatalogItemListDto> GetCatalogList(int page, int pageSize)
        {
            int rowCount = 0;
            var catalogItem = _context.CatalogItems
                .Include(x => x.CatalogType)
                .Include(x => x.CatalogBrand)
                .ToPaged(page, pageSize, out rowCount)
                .OrderByDescending(x => x.Id)
                .Select(x => new CatalogItemListDto()
                {
                    Id = x.Id,
                    Brand = x.CatalogBrand.Brand,
                    Type = x.CatalogType.Type,
                    AvailableStock = x.AvailableStock,
                    MaxStockThreshold = x.MaxStockThreshold,
                    Name = x.Name,
                    RestockThreshold = x.RestockThreshold,
                    Price = x.Price
                }).ToList();
            return new PaginatedItemsDto<CatalogItemListDto>(count: rowCount, data: catalogItem, pageIndex :page, pageSize : pageSize);
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

    public class CatalogItemListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }


    }

}
