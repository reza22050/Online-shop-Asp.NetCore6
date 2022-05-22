using Application.Orders.CustomerOrdersServices;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ICustomerOrdersServices _customerOrdersServices;
        private readonly UserManager<User> _userManager;

        public OrdersController(ICustomerOrdersServices customerOrdersServices, UserManager<User> userManager)
        {
            this._customerOrdersServices = customerOrdersServices;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var orders = _customerOrdersServices.GetMyOrder(user.Id);
            return View(orders);
        }
    }
}
