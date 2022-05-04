using Application.Interfaces.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.GetMenuItem
{
    public interface IGetMenuItemService
    {
        List<MenuItemDo> Execute();
    }

    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public GetMenuItemService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<MenuItemDo> Execute()
        {
            var catalogType = _context.CatalogTypes.Include(x=>x.ParentCatalogType).ToList();
            var data = _mapper.Map<List<MenuItemDo>>(catalogType);
            return data;
        }
    }

    public class MenuItemDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public List<MenuItemDo> SubMenu { get; set; }
    }
}
