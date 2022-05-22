using Application.Interfaces.Contexts;
using Domain.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Banners
{
    public interface IBannersService
    {
        void AddBanner(BannerDto banner);
        List<BannerDto> GetBanners(); 
    }

    public class BannersService : IBannersService
    {
        private readonly IDataBaseContext _context;

        public BannersService(IDataBaseContext context)
        {
            this._context = context;
        }
        public void AddBanner(BannerDto banner)
        {
            _context.Banners.Add(new Banner()
            {
                Image = banner.Image,
                IsActive = banner.IsActive,
                Link = banner.Link,
                Name = banner.Name,
                Position = banner.Position,
                Priority = banner.Priority,
            });
            _context.SaveChanges();
        }

        public List<BannerDto> GetBanners()
        {
            var banners = _context.Banners.Select(x=>new BannerDto() { 
                Image = x.Image,
                IsActive = x.IsActive,
                Link=x.Link,
                Name=x.Name,
                Position = x.Position,
                Priority=x.Priority,
            }).ToList();
            return banners;
        }
    }

    public class BannerDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public Position Position { get; set; }
        public int Priority { get; set; }
    }

}
