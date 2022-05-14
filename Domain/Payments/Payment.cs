using Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Payments
{
    [AudiTable]
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; private set; }
        public bool IsPay { get; private set; } = false;
        public DateTime? DatePay { get; private set; } = null;
        public string Authority { get; private set; }
        public long RefId { get; private set; }
        public Order.Order Order { get; private set; }
        public int OrderId { get; private set; }
        
        public Payment(decimal amount, int orderId)
        {
            Amount = amount;
            OrderId = orderId;
        }

        public void PaymentIsDone(string authority, long refId)
        {
            IsPay = true;
            DatePay = DateTime.Now;
            Authority = authority;
            RefId = refId;
        }

    }
}
