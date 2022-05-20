using Domain.Attributes;
using Domain.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    [AudiTable]
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get;private set; }
        public DateTime OrderDate { get; private set; } = DateTime.Now;
        public Address Address { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public OrderStatus OrderStatus { get; private set; }


        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();


        public decimal DiscountAmount { get; private set; }
        public Discount AppliedDiscount { get; private set; }
        public int? AppliedDiscountId { get; private set; }


        public Order(string userId, Address address,List<OrderItem> orderItems, PaymentMethod paymentMethod, Discount discount)
        {
            UserId = userId;
            Address = address;
            PaymentMethod = paymentMethod;
            _orderItems = orderItems;
            if(discount != null)
            {


            }
        }

        public Order()
        {

        }

        public decimal TotalPrice()
        {
            decimal totalPrice = _orderItems.Sum(x => x.UnitPrice * x.Units);
            totalPrice -= AppliedDiscount.GetDiscountAmount(totalPrice);
            return totalPrice;
        }


        public decimal TotalPriceWithOutDiscount()
        {
            decimal totalPrice = _orderItems.Sum(x => x.UnitPrice * x.Units);
            return totalPrice;
        }

        public void ApplyDiscountCode(Discount discount)
        {
            this.AppliedDiscount = discount;
            this.AppliedDiscountId = discount.Id;
            this.DiscountAmount = discount.GetDiscountAmount(TotalPrice());
        }

        public void PaymentDone()
        {
            PaymentStatus = PaymentStatus.Paid;
        }

        public void OrderDelivered()
        {
            OrderStatus = OrderStatus.Deliverd;
        }

        public void OrderReturned()
        {
            OrderStatus = OrderStatus.Returned;
        }
        public void OrderCancelled()
        { 
            OrderStatus = OrderStatus.Cancelled;
        }
    }


    public class Address
    {
        public Address(string State, string City, string ZipCode, string PostalAddress, string ReciverName)
        {
            this.State = State;
            this.City = City;
            this.ZipCode = ZipCode;
            this.PostalAddress = PostalAddress;
            this.ReciverName = ReciverName;
        }
        public string State { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string PostalAddress { get; private set; }
        public string ReciverName { get; private set; }
    }

    public enum PaymentMethod
    {
        OnlinePayment = 0,
        PaymentOnTheSpot  = 1,
    }

    public enum PaymentStatus
    {
        WaitingForPayment = 0,
        Paid = 1 
    }
    public enum OrderStatus
    {
        Processing = 0,
        Deliverd = 1,
        Returned = 2,
        Cancelled = 3
    }

    [AudiTable]
    public class OrderItem
    {
        public OrderItem(int CatalogItemId, string ProductName, string PictureUri , decimal UnitPrice, int Units)
        {
            this.CatalogItemId = CatalogItemId;
            this.ProductName = ProductName;
            this.PictureUri = PictureUri;
            this.UnitPrice = UnitPrice;
            this.Units = Units;
        }

        public OrderItem()
        {

        }

        public int Id { get; set; }
        public int CatalogItemId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Units { get; private set; }

    }


}
