using Application.Catalogs.CatalogItems.CatalogItemServices;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Areas.Customers.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class MyFavouriteController : Controller
    {
        private readonly ICatalogItemService _catalogItemservice;
        private readonly UserManager<User> _userManager;

        public MyFavouriteController(ICatalogItemService catalogItemservice, UserManager<User> userManager)
        {
            this._catalogItemservice = catalogItemservice;
            this._userManager = userManager;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var data = _catalogItemservice.GetMyFavourite(user.Id, page, pageSize);
            return View(data);
        } 

        public IActionResult AddToMyFavourite(int CatalogItemId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            _catalogItemservice.AddToMyFavourite(user.Id, CatalogItemId);
            return RedirectToAction(nameof(Index));
        }

    }
}
