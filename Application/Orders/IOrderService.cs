using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders
{
    public interface IOrderService
    {
        int CreateOrder(int basketId, int userAddressId, PaymentMethod paymentMethod);
    }


    public class OrderService : IOrderService
    {
        private readonly IDataBaseContext _context;
        private readonly Mapper _mapper;
        private readonly IUriComposerServie _uriComposerServie;

        public OrderService(IDataBaseContext context, Mapper mapper, IUriComposerServie uriComposerServie)
        {
            _context = context;
            _mapper = mapper;
            _uriComposerServie = uriComposerServie;
        }
        public OrderService()
        {

        }
        public int CreateOrder(int basketId, int userAddressId, PaymentMethod paymentMethod)
        {
            var basket = _context.Baskets.Include(x => x.Items).SingleOrDefault(x => x.Id == basketId);
            int[] Ids = basket.Items.Select(x => x.CatalogItemId).ToArray();
            var catalogItems = _context.CatalogItems.Include(x=>x.CatalogItemImages).Where(x => Ids.Contains(basketId));

            var orderItems = basket.Items.Select(basketItem =>
            {
                var catalogItem = catalogItems.First(x => x.Id == basketItem.CatalogItemId);
                var orderItem = new OrderItem(catalogItem.Id,
                    catalogItem.Name,
                    _uriComposerServie.ComposerImageUri(catalogItem?.CatalogItemImages?.FirstOrDefault()?.Src ?? ""),
                    catalogItem.Price,
                    basketItem.Quantity);
                return orderItem;
            }).ToList() ;

            var userAddress = _context.UserAddresses.SingleOrDefault(p => p.Id == userAddressId);
            var address = _mapper.Map<Address>(userAddress); 
            var order = new Order(basket.BuyerId, address, orderItems, paymentMethod);
            _context.Orders.Add(order);
            _context.Baskets.Remove(basket); 
            _context.SaveChanges();
            return order.Id;
        }
    }


}
