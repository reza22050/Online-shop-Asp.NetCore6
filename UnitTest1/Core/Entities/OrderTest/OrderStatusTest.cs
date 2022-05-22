using Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest1.Builders;
using Xunit;

namespace UnitTest1.Core.Entities.OrderTest
{
    public class OrderStatusTest
    {
        [Fact]
        public void when_order_is_delivered_orderstaus_changes_to_delivered()
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrderWithDefaultValues();
            order.OrderDelivered();

            Assert.Equal(OrderStatus.Deliverd, order.OrderStatus);

        }

    }
}
