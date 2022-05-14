using Application.Interfaces.Contexts;
using Domain.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payments
{
    public interface IPaymentService
    {
        PaymentOfOrderDto PayForOrder(int OrderId);
        PaymentDto GetPayment(Guid Id);
        bool VerifyPayment(Guid Id, string Authority, long RefId);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IDataBaseContext _context;
        private readonly IIdentityDatabaseContext _identityDatabase;

        public PaymentService(IDataBaseContext context, IIdentityDatabaseContext identityDatabase)
        {
            _context = context;
            _identityDatabase = identityDatabase;
        }

        public PaymentDto GetPayment(Guid Id)
        {
            var payment = _context.Payments
                .Include(x=>x.Order)
                .ThenInclude(x=>x.OrderItems)
                .SingleOrDefault(x=>x.Id == Id);

            var user = _identityDatabase.Users.SingleOrDefault(x=>x.Id == payment.Order.UserId);

            string description = $"payment by id {payment.OrderId} " + Environment.NewLine;
            description += "Products" +  Environment.NewLine;

            foreach (var item in payment.Order.OrderItems.Select(x=>x.ProductName))
            {
                description += $" -{item}";
            }

            PaymentDto paymentDto = new PaymentDto()
            {
                Amount = payment.Order.TotalPrice(),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserId = user.Id,
                Id = payment.Id,
                Description = description,
            };
            return paymentDto;

        }

        public PaymentOfOrderDto PayForOrder(int OrderId)
        {
            var order = _context.Orders.Include(x=>x.OrderItems)
                .SingleOrDefault(x=>x.Id == OrderId);

            if (order == null)
                throw new Exception("");

            var payment = _context.Payments.SingleOrDefault(x => x.OrderId == order.Id);

            if (payment == null)
            {
                payment = new Domain.Payments.Payment(order.TotalPrice(), order.Id);
                _context.Payments.Add(payment);
                _context.SaveChanges();
            }

            return new PaymentOfOrderDto()
            {
                Amount = payment.Amount,
                PaymentId = payment.Id,
                PaymentMethod = order.PaymentMethod
            };
        }

        public bool VerifyPayment(Guid Id, string Authority, long RefId)
        {
            var payment = _context.Payments.Include(x => x.Order)
                .SingleOrDefault(x => x.Id == Id);
            if (payment == null)
                throw new Exception("payment not found");

            payment.Order.PaymentDone();
            payment.PaymentIsDone(Authority, RefId);
            _context.SaveChanges();
            return true;
        }
    }

    public class PaymentOfOrderDto
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }

    }


}
