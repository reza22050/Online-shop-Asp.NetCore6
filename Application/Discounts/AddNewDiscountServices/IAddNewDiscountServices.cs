using Application.Interfaces.Contexts;
using Domain.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Discounts.AddNewDiscountServices
{
    public interface IAddNewDiscountServices
    {
        void Execute(AddNewDiscountDto discount);
    }

    public class AddNewDiscountServices : IAddNewDiscountServices
    {
        private readonly IDataBaseContext _context;

        public AddNewDiscountServices(IDataBaseContext context)
        {
            this._context = context;
        }
        public void Execute(AddNewDiscountDto discount)
        {
            var newDiscount = new Discount()
            {
                Name = discount.Name,
                CouponCode = discount.CouponCode,
                DiscountAmount = discount.DiscountAmount,
                DiscountLimitationId = discount.DiscountLimitationId,
                DiscountPercentage = discount.DiscountPercentage,
                DiscountTypeId = discount.DiscountTypeId,
                EndDate = discount.EndDate,
                RequiresCouponCode = discount.RequiresCouponCode,
                StartDate = discount.StartDate,
                UsePercentage = discount.UsePercentage,
            };

            if (discount.appliedToCatalogItem != null)
            {
                var catalogItems = _context.CatalogItems.Where(x=>discount.appliedToCatalogItem.Contains(x.Id)).ToList();
                newDiscount.CatalogItems = catalogItems;
            }
            _context.Discounts.Add(newDiscount);
            _context.SaveChanges();

        }
    }

    public class AddNewDiscountDto
    {
        public string Name { get; set; }
        public bool UsePercentage { get; set; }
        public int DiscountPercentage { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool RequiresCouponCode { get; set; }
        public string CouponCode { get; set; }
        public int DiscountTypeId { get; set; }
        public int DiscountLimitationId { get; set; }
        public int LimitationTimes { get; set; } = 0;
        public ICollection<int> appliedToCatalogItem { get; set; }
    }

}
