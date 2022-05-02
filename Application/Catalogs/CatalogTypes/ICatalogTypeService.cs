using Application.Dtos;
using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace Application.Catalogs.CatalogTypes
{

    public interface ICatalogTypeService
    {
        BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogType);
        BaseDto Remove(int Id);
        BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalogType);
        BaseDto<CatalogTypeDto> FindById(int Id);
        PaginatedItemsDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize);
    }

    public class CatalogTypeService : ICatalogTypeService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public CatalogTypeService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogType)
        {
            var model = _mapper.Map<CatalogType>(catalogType);
            _context.CatalogTypes.Add(model);
            _context.SaveChanges();
            return new BaseDto<CatalogTypeDto>(true, new List<string>() { $"Type of {model.Type} added to database successfully" },
                _mapper.Map<CatalogTypeDto>(model));


        }

        public BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalogType)
        {
            var model = _context.CatalogTypes.SingleOrDefault(x => x.Id == catalogType.Id);
            _mapper.Map(catalogType, model);
            _context.SaveChanges();
            return new BaseDto<CatalogTypeDto>(true, new List<string>() { $"Type of {model.Type} ediited successfully" }, _mapper.Map<CatalogTypeDto>(model));
        }

        public BaseDto<CatalogTypeDto> FindById(int Id)
        {
            var data = _context.CatalogTypes.Find(Id);
            var result = _mapper.Map<CatalogTypeDto>(data);
            return new BaseDto<CatalogTypeDto>(true, new List<string>(), result);
        }

        public PaginatedItemsDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize)
        {

            int totalCount = 0;
            var model = _context.CatalogTypes.Where(x=>x.ParentCatalogTypeId == parentId).PagedResult(page, pageSize, out totalCount);
            var result = _mapper.ProjectTo<CatalogTypeListDto>(model).ToList();
            return new PaginatedItemsDto<CatalogTypeListDto>(page, pageSize, totalCount, result);
        }

        public BaseDto Remove(int Id)
        {
            var catalogType = _context.CatalogTypes.Find(Id);
            _context.CatalogTypes.Remove(catalogType);
            _context.SaveChanges();
            return new BaseDto(true, new List<string>() { "Remove successfully done!" });

        }
    }

    public class CatalogTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentCatalogTypeId { get; set; }
    }

    public class CatalogTypeListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public int SubTypeCount { get; set; }   
    }
}
