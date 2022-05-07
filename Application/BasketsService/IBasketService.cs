using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using Domain.Baskets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BasketsService
{
    public interface IBasketService
    {
        BasketDto GetOrCreateBasketForUser(string BuyerId);

    }

    public class BasketService : IBasketService
    {
        private readonly IDataBaseContext _context;
        private readonly IUriComposerServie _uriComposerService;

        public BasketService(IDataBaseContext context, IUriComposerServie uriComposerService)
        {
            _context = context;
            _uriComposerService = uriComposerService;
        }
        public BasketDto GetOrCreateBasketForUser(string BuyerId)
        {
            var basket = _context.Baskets
                .Include(x => x.Items)
                .ThenInclude(x => x.CatalogItem)
                .ThenInclude(x => x.CatalogItemImages)
                .SingleOrDefault(x => x.BuyerId == BuyerId);

            if (basket == null)
            {
                return CreateBasketForUser(BuyerId);
            }

            return new BasketDto()
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(x => new BasketItemDto()
                {
                    CatalogItemId = x.CatalogItemId,
                    Id = x.Id,
                    CatalogName = x.CatalogItem.Name,
                    Quantity = x.Quantity,
                    ImageUrl = _uriComposerService.ComposerImageUri(x?.CatalogItem?.CatalogItemImages?.FirstOrDefault()?.Src ?? ""),
                    UnitPrice = x.UnitPrice
                }).ToList()
            };
        }


        private BasketDto CreateBasketForUser(string BuyerId)
        {
            Basket basket = new Basket(BuyerId);
            _context.Baskets.Add(basket);
            _context.SaveChanges();

            return new BasketDto()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
            };

        }

    }

    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }

    public class BasketItemDto
    {
        public int Id { get; set; }
        public int CatalogItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public string CatalogName { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}
