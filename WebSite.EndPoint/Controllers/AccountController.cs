using Application.BasketsService;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite.EndPoint.Models.ViewModels.Register;
using WebSite.EndPoint.Models.ViewModels.User;
using WebSite.EndPoint.Utilities.Filters;

namespace WebSite.EndPoint.Controllers
{
    [ServiceFilter(typeof(SaveVisitorFilter))]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBasketService _basketService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IBasketService basketService)
        {
            _userManager = userManager;
            _signInManager  = signInManager;
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userManager.FindByNameAsync(model.Email).Result;

            if (user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }

            _signInManager.SignOutAsync();
            var result = _signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, true).Result;
            if (result.Succeeded)
            {
                TransferBasketForUser(user.Id);
                return Redirect(model.ReturnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                // 
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Register(string returnUrl = "/")
        {
            return View(new LoginViewModel() { 
                ReturnUrl = returnUrl
            });
        }

      
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);     
            }
            User user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {
                var mainUser = _userManager.FindByNameAsync(user.Email).Result;
                TransferBasketForUser(mainUser.Id);
                _signInManager.SignInAsync(user, true).Wait();
                return RedirectToAction(nameof(Profile));
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
            

            return View(model);
        }

        public IActionResult Profile()
        {
            return View();
        }


        private void TransferBasketForUser(string userId)
        {
            string cookieName = "BasketId";
            if (Request.Cookies.ContainsKey(cookieName))
            {
                var anonymousId = Request.Cookies[cookieName];
                _basketService.TransferBasket(anonymousId, userId);
                Response.Cookies.Delete(cookieName);
            }
        }
    }
}
