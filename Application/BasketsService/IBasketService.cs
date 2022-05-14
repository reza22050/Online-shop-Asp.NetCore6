using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using Domain.Baskets;
using Microsoft.EntityFrameworkCore;

namespace Application.BasketsService
{
    public interface IBasketService
    {
        BasketDto GetOrCreateBasketForUser(string BuyerId);
        void AddItemToBasket(int basketId, int catalogItemId, int quantity = 1);
        bool RemoveItemFromBasket(int ItemId);
        bool SetQuantities(int itemId, int quantity);
        BasketDto GetBasketForUser(string UserId);
        void TransferBasket(string anonymousId, string UserId);
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

        public void AddItemToBasket(int basketId, int catalogItemId, int quantity = 1)
        {
            var basket = _context.Baskets.FirstOrDefault(x=>x.Id == basketId);
            if (basket == null)
                throw new Exception("");
            var price = _context.CatalogItems.Find(catalogItemId).Price;
            basket.AddItem(catalogItemId, quantity, price);
            _context.SaveChanges();
        }

        public BasketDto GetBasketForUser(string UserId)
        {
            var basket = _context.Baskets
                .Include(x => x.Items)
                .ThenInclude(x => x.CatalogItem)
                .ThenInclude(x => x.CatalogItemImages)
                .SingleOrDefault(x => x.BuyerId == UserId);

            if (basket == null)
            {
                return null;
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

        public bool RemoveItemFromBasket(int ItemId)
        {
            var item = _context.BasketItems.SingleOrDefault(x=>x.Id == ItemId);
            _context.BasketItems.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public bool SetQuantities(int itemId, int quantity)
        {
            var item = _context.BasketItems.SingleOrDefault(x => x.Id == itemId);
            item.SetQuantity(quantity);
            _context.SaveChanges();
            return true;
        }

        public void TransferBasket(string anonymousId, string UserId)
        {
            var anonymousBasket = _context.Baskets.SingleOrDefault(x => x.BuyerId == anonymousId);
            if (anonymousBasket == null) return;
            var userBasket = _context.Baskets.SingleOrDefault(x=>x.BuyerId == UserId);
            if (userBasket == null)
            {
                userBasket = new Basket(UserId);
                _context.Baskets.Add(userBasket);
            }

            foreach (var item in anonymousBasket.Items)
            {
                userBasket.AddItem(item.CatalogItemId, item.Quantity, item.UnitPrice); 
            }

            _context.Baskets.Remove(anonymousBasket);
            _context.SaveChanges();

        }

        private BasketDto CreateBasketForUser(string buyerId)
        {
            Basket basket = new Basket(buyerId);
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
        public decimal Total()
        {
            if (Items.Count > 0)
            {
                return Items.Sum(x=>x.UnitPrice * x.Quantity); 
            }
            return 0;
        }
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
