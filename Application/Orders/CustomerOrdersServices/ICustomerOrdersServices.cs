using Application.Interfaces.Contexts;
using Domain.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.CustomerOrdersServices
{
    public interface ICustomerOrdersServices
    {
        List<MyOrderDto> GetMyOrder(string userId);
    }

    public class CustomerOrdersServices : ICustomerOrdersServices
    {
        private readonly IDataBaseContext _context;

        public CustomerOrdersServices(IDataBaseContext context)
        {
            this._context = context;
        }
        public List<MyOrderDto> GetMyOrder(string userId)
        {
            var orders = _context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .Select(x => new MyOrderDto()
                {
                    Id = x.Id,
                    Date = _context.Entry(x).Property("InsertTime").CurrentValue.ToString(),
                    OrderStatus = x.OrderStatus,
                    PaymentStatus = x.PaymentStatus,
                    Price = x.TotalPrice()

                }).ToList();
            return orders;
        }
    }

    public class MyOrderDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public decimal Price { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }


    }

}
