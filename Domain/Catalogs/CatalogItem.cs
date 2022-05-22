using Domain.Attributes;
using Domain.Discounts;
using Domain.Order;

namespace Domain.Catalogs
{
    [AudiTable]
    public class CatalogItem
    {
        private decimal _price = 0;
        private decimal? _oldPrice = null; 

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public decimal Price { 
            get { return GetPrice(); }
            //set { _price = value; }
            set { Price = _price; }
        }
    
        public decimal? OldPrice { 
            get{ return _oldPrice; } 
            set { OldPrice = _oldPrice; } 
        }
        public int? PercentDiscount { get; set; }

        public int CatalogTypeId { get; set; }
        public CatalogType CatalogType { get; set; }

        public int CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }

        public int AvailableStock { get; set; }

        public int RestockThreshold { get; set; }

        public int MaxStockThreshold { get; set; }

        public int VisitCount { get; set; }
        public string Slug { get; set; }
        public ICollection<CatalogItemFeature> CatalogItemFeatures { get; set; }
        public ICollection<CatalogItemImage> CatalogItemImages { get; set; }
        public ICollection<CatalogItemFavourite> CatalogItemFavourites { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Discount> Discounts { get; set; }

        private decimal GetPrice()
        {
            var dis = GetPreferredDicount(Discounts, _price);
            if (dis != null)
            {
                var discountAmount = dis.GetDiscountAmount(_price);
                var newPrice = _price - discountAmount;
                _oldPrice = _price;
                PercentDiscount = (int)(discountAmount * 100 / _price);
                return newPrice;
            }
            return _price;
        }

        private Discount GetPreferredDicount(ICollection<Discount> discounts, decimal price)
        {
            Discount preferredDiscount = null;
            decimal? maximumDiscountValue = null;
            if (discounts != null)
            {
                foreach (Discount discount in discounts)
                {
                    var currentDiscountValue = discount.GetDiscountAmount(price);
                    if (currentDiscountValue != decimal.Zero)
                    {
                        if (!maximumDiscountValue.HasValue || currentDiscountValue > maximumDiscountValue.Value)
                        {
                            maximumDiscountValue = currentDiscountValue;
                            preferredDiscount = discount;
                        }
                    }
                }
            }
            return preferredDiscount;
        }

    }

}
