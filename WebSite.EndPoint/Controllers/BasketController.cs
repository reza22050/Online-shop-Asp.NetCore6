using Application.BasketsService;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace WebSite.EndPoint.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<User> _signInManager;
        private string? UserId = null;
        public BasketController(IBasketService basketService, SignInManager<User> signInManager)
        {
            _basketService = basketService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = GetOrSetBasket();
            return View(data);
        }

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
                return _basketService.GetOrCreateBasketForUser(User.Identity.Name);
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

        [HttpPost]
        public IActionResult SetQuantity(int basketItemId, int quantity)
        {
            return Json(_basketService.SetQuantities(basketItemId, quantity));
        }


    }
}
