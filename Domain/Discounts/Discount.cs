using Domain.Attributes;
using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Discounts
{
    [AudiTable]
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool UsePercentage { get; set; }
        public int DiscountPercentage { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool RequiresCouponCode { get; set; }
        public bool CouponCode { get; set; }

        [NotMapped]
        public DiscountType DiscountType {
            get => (DiscountType)this.DiscountTypeId;
            set => this.DiscountTypeId = (int)value;
        }

        public int DiscountTypeId { get; set; }

        public ICollection<CatalogItem> CatalogItems { get; set; }
        //

        public int DiscountLimitationId { get; set; }

        [NotMapped]
        public DiscountLimitationType DiscountLimitation
        {
            get => (DiscountLimitationType)this.DiscountLimitationId;
            set => this.DiscountLimitationId = (int)value;
        }

    }

    public enum DiscountType
    {
        AssignedProduct = 1,
        AssignedToCategories = 2,
        AssignedToUser = 3,
        AssignedToBrand = 4,
    }

    public enum DiscountLimitationType
    {
        Unlimited = 0,
        NTimeOnly = 1,
        NTimesPerCustomer = 2,
    }
}
