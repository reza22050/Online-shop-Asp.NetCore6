using Application.BasketsService;
using Application.Orders;
using Application.Payments;
using Application.Users;
using Domain.Order;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite.EndPoint.Models.ViewModels.Basket;
using WebSite.EndPoint.Utilities;

namespace WebSite.EndPoint.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserAddressService _userAddressService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private string? UserId = null;
        public BasketController(IBasketService basketService, SignInManager<User> signInManager, IUserAddressService userAddressService, 
            IOrderService orderService, IPaymentService payment
            )
        {
            _basketService = basketService;
            _signInManager = signInManager;
            _userAddressService = userAddressService;
            _orderService = orderService;
            _paymentService = payment;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var data = GetOrSetBasket();
            return View(data);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(int catalogItemId, int quantity = 1)
        {
            var bakset = GetOrSetBasket();
            _basketService.AddItemToBasket(bakset.Id, catalogItemId, quantity);
            return RedirectToAction(nameof(Index));

        }

        private BasketDto GetOrSetBasket()
        {
            if (_signInManager.IsSignedIn(User))
            {
                UserId =   ClaimUtility.GetUserId(User);
                return _basketService.GetOrCreateBasketForUser(UserId);
            }
            else
            {
                SetCookiesForBasket();
                return _basketService.GetOrCreateBasketForUser(UserId);
            }
        }

        private void SetCookiesForBasket()
        {
            string basketCookieName = "BasketId";
            if (Request.Cookies.ContainsKey(basketCookieName))
            {
                UserId = Request.Cookies[basketCookieName];
            }

            if (UserId != null) return;

            UserId = Guid.NewGuid().ToString();

            var cookieOptions = new CookieOptions { IsEssential = true };
            cookieOptions.Expires = DateTime.Now.AddYears(2);
            Response.Cookies.Append(basketCookieName, UserId, cookieOptions);
        }

        [HttpPost]
        public IActionResult RemoveItemFromBasket(int ItemId)
        {
            _basketService.RemoveItemFromBasket(ItemId);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SetQuantity(int basketItemId, int quantity)
        {
            return Json(_basketService.SetQuantities(basketItemId, quantity));
        }

        public IActionResult ShippingPayment()
        {
            ShippingPaymentViewModel model = new ShippingPaymentViewModel();
            string userId = ClaimUtility.GetUserId(User);
            model.Basket = _basketService.GetBasketForUser(userId);
            model.UserAddresses = _userAddressService.GetAddress(userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult ShippingPayment(int Address, PaymentMethod paymentMethod)
        {
            string userId = ClaimUtility.GetUserId(User);
            var basket = _basketService.GetBasketForUser(userId);
            int orderId = _orderService.CreateOrder(basket.Id,Address,paymentMethod);
            if (paymentMethod == PaymentMethod.OnlinePayment)
            {
                // submit payment
                var payment = _paymentService.PayForOrder(orderId);
                return RedirectToAction("Index","Pay", new {PaymentId = payment.PaymentId});
            }
            else {
                return RedirectToAction("Index", "Orders", new { area = "customers" });
            }

        }


        public IActionResult Checkout()
        {
            return View();
        }
    }
}
