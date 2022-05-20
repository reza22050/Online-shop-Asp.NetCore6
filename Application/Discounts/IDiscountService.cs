using Application.Dtos;
using Application.Interfaces.Contexts;
using Domain.Discounts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Discounts
{
    public interface IDiscountService
    {
        List<CatalogItemDto> GetCatalogItems(string searchKey);
        bool ApplyDiscountInBasket(string CoponCode, int BasketId);
        bool RemoveDiscountFromBasket(int BasketId);
        BaseDto IsDiscountValid(string coponCode,User user);
    }

    public class DiscountService : IDiscountService
    {
        private readonly IDataBaseContext _context;
        private readonly IDiscountHistoryService _discountHistoryService;

        public DiscountService(IDataBaseContext context, IDiscountHistoryService discountHistoryService)
        {
            _context = context;
            this._discountHistoryService = discountHistoryService;
        }

        public bool ApplyDiscountInBasket(string CoponCode, int BasketId)
        {
            var basket = _context.Baskets
               .Include(x => x.Items)
               .Include(x => x.AppliedDiscount)
               .FirstOrDefault(x => x.Id == BasketId);
            
            var discount = _context.Discounts.Where(x=>x.CouponCode.Equals(CoponCode)).FirstOrDefault();
            basket.ApplyDiscountCode(discount);
            _context.SaveChanges();

            return true;
        } 

        public List<CatalogItemDto> GetCatalogItems(string? searchKey)
        {
            if (!string.IsNullOrEmpty(searchKey))
            {
                var data = _context.CatalogItems.Where(x => x.Name.Contains(searchKey)).Select(x => new CatalogItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                return data;
            }
            else
            {
                var data = _context.CatalogItems
                    .OrderByDescending(x=>x.Id)
                    .Take(10)
                    .Select(x => new CatalogItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                return data;
            }
        }

        public BaseDto IsDiscountValid(string coponCode, User user)
        {
            var discount = _context.Discounts.Where(x => x.CouponCode.Equals(coponCode)).FirstOrDefault();

            if (discount == null)
            {
                return new BaseDto(false, Message: new List<string> {"Code is not valid." });
            }

            var now = DateTime.Now;
            if (discount.StartDate.HasValue)
            {
                var startdate = DateTime.SpecifyKind(discount.StartDate.Value, DateTimeKind.Utc);
                if (startdate.CompareTo(now) > 0)
                {
                    return new BaseDto(false, new List<string>() { "You should wait a little more."});
                }
            }

            if(discount.EndDate.HasValue)
            {
                var enddate = DateTime.SpecifyKind(discount.EndDate.Value, DateTimeKind.Utc);
                if (enddate.CompareTo(now) < 0)
                {
                    return new BaseDto(false, new List<string>() {"Your code is not valid longer" });
                }
            }

            var checkLimit = CheckDiscountLimitations(discount, user);

            if (checkLimit.IsSuccess == false)
                return checkLimit;
            return new BaseDto(true, null);

        }

        private BaseDto CheckDiscountLimitations(Discount discount, User user)
        {
            switch (discount.DiscountLimitation)
            {
                case DiscountLimitationType.Unlimited:
                    {
                        return new BaseDto(true, null);
                        break;
                    }
                case DiscountLimitationType.NTimeOnly:
                    {
                        var totalUsage = _discountHistoryService.GetAllDiscountUsageHistory(discount.Id,null, 0, 1).Data.Count();
                        if(totalUsage<discount.LimitationTimes)
                        {
                            return new BaseDto(true, null);
                        }
                        else
                        {
                            return new BaseDto(false, new List<string>() { "Discount capacity is completed" });
                        }
                        break;
                    }
                case DiscountLimitationType.NTimesPerCustomer:
                    {
                        if (user != null)
                        {
                            var totalUsage = _discountHistoryService.GetAllDiscountUsageHistory(discount.Id, user.Id, 0, 1).Data.Count();
                             if(totalUsage<discount.LimitationTimes)
                            {
                                return new BaseDto(true, null);
                            }
                            else
                            {
                                return new BaseDto(false, new List<string>() { "Discount usage count is finished" });
                            }
                        }
                        else
                        {
                            return new BaseDto(true, null);
                        }
                        break;
                    }
                default:
                    return new BaseDto(true, null);
                    break;
            }

        }


        public bool RemoveDiscountFromBasket(int BasketId)
        {
            var basket = _context.Baskets.Find(BasketId);
            basket.RemoveDiscount();
            _context.SaveChanges();
            return true;
        }
    }
    public class CatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
