﻿using Application.BasketsService;
using Application.Users;

namespace WebSite.EndPoint.Models.ViewModels.Basket
{
    public class ShippingPaymentViewModel
    {
        public BasketDto Basket { get; set; }
        public List<UserAddressDto> UserAddresses { get; set; }



    }
}
