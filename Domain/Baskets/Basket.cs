using Domain.Attributes;
using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Baskets
{
    [AudiTable]
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; private set; }

        private readonly List<BasketItem> _Items = new List<BasketItem>();
        public ICollection<BasketItem> Items => _Items.AsReadOnly();

        public Basket(string buyerId)
        {
            BuyerId = BuyerId;
        }

        public void AddItem(int catalogItemId, int quantity, int unitPrice)
        {
            if (!Items.Any(x => x.CatalogItemId == catalogItemId))
            {
                _Items.Add(new BasketItem(catalogItemId, quantity, unitPrice));
                return;

            }
            else {
                var existingItem = _Items.FirstOrDefault(x => x.CatalogItemId == catalogItemId);
                existingItem.AddQuantity(quantity);
            }
        }

    }

    [AudiTable]
    public class BasketItem
    {
        public int Id { get; set; }
        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }
        public int CatalogItemId { get; private set; }
        public CatalogItem CatalogItem { get; private set; }
        public int BasketId { get; private set; }
        public BasketItem(int catalogItemId,int quantity, decimal unitPrice)
        {
            CatalogItemId = catalogItemId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += Quantity;
        }

    } 


}
