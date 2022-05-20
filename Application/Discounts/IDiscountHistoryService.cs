using Application.Dtos;
using Application.Interfaces.Contexts;
using Common;
using Domain.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Discounts
{
    public interface IDiscountHistoryService
    {
        void InsertDiscountUsageHistory(int DiscountId, int OrderId);
        DiscountUsageHistory GetDiscountUsageHistoryById(int discountUsageHistoryId);
        PaginatedItemsDto<DiscountUsageHistory> GetAllDiscountUsageHistory(int? discountId, string? userId, int pageIndex, int pageSize);
    }

    public class DiscountHistoryService : IDiscountHistoryService
    {
        private readonly IDataBaseContext _context;

        public DiscountHistoryService(IDataBaseContext context)
        {
            this._context = context;
        }
        public PaginatedItemsDto<DiscountUsageHistory> GetAllDiscountUsageHistory(int? discountId , string? userId, int pageIndex, int pageSize)
        {
            var query = _context.DiscountUsageHistories.AsQueryable();
            if(discountId.HasValue && discountId.Value > 0)
                query.Where(x=>x.DiscountId == discountId.Value);

            if(!string.IsNullOrEmpty(userId))
                query = query.Where(x=>x.Order != null && x.Order.UserId == userId);

            query = query.OrderByDescending(x => x.CreateOn);
            var pagedItems = query.PagedResult(pageIndex, pageSize, out int rowCount);
            return new PaginatedItemsDto<DiscountUsageHistory>(pageIndex, pageSize, rowCount, query);


        }

        public DiscountUsageHistory GetDiscountUsageHistoryById(int discountUsageHistoryId)
        {
            if (discountUsageHistoryId == 0)
            {
                return null;
            }

            var discountUsage = _context.DiscountUsageHistories.Find(discountUsageHistoryId);
            return discountUsage;
        }

        public void InsertDiscountUsageHistory(int DiscountId, int OrderId)
        {
            var order = _context.Orders.Find(OrderId);
            var discount = _context.Discounts.Find(DiscountId);

            DiscountUsageHistory discountUsageHistory = new DiscountUsageHistory()
            {
                CreateOn = DateTime.Now,
                Discount = discount,
                Order = order,
            }; 
            _context.DiscountUsageHistories.Add(discountUsageHistory);
            _context.SaveChanges();



        }
    }

}
