﻿using Domain.Attributes;
using Domain.Catalogs;
using Domain.Discounts;
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


        public decimal DiscountAmount { get; private set; }

        public Discount AppliedDiscount { get; private set; }
        public int? AppliedDiscountId { get; private set; }


        public Basket(string buyerId)
        {
            BuyerId = buyerId;
        }

        public void AddItem(int catalogItemId, int quantity, decimal unitPrice)
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


        public decimal TotalPrice()
        {
            decimal totalPrice = _Items.Sum(x=>x.UnitPrice * x.Quantity);
            totalPrice -= AppliedDiscount.GetDiscountAmount(totalPrice);
            return totalPrice;
        }

        public decimal TotalPriceWithOutDiscount()
        {
            decimal totalPrice = _Items.Sum(x => x.UnitPrice * x.Quantity);
            return totalPrice;
        }


        public void ApplyDiscountCode(Discount discount)
        {
            this.AppliedDiscount = discount;
            this.AppliedDiscountId = discount.Id;
            this.DiscountAmount = discount.GetDiscountAmount(TotalPriceWithOutDiscount());
        }

        public void RemoveDiscount()
        {
            AppliedDiscount = null;
            AppliedDiscountId = null;
            DiscountAmount = 0;
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
            SetQuantity(quantity);
            UnitPrice = unitPrice;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    } 


}
