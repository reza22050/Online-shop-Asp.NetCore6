using Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSite.EndPoint.Utilities;

namespace WebSite.EndPoint.Areas.Customers.Controllers
{
    [Authorize]
    [Area("Customers")]
    public class AddressController : Controller
    {
        private readonly IUserAddressService _userAddressService;

        public AddressController(IUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }
        public IActionResult Index()
        {
            var addresses = _userAddressService.GetAddress(ClaimUtility.GetUserId(User));
            return View(addresses);
        }


        public IActionResult AddNewAddress()
        {
            return View(new AddUserAddressDto());
        }
        
        [HttpPost]
        public IActionResult AddNewAddress(AddUserAddressDto address)
        {

            string userId = ClaimUtility.GetUserId(User);
            address.UserId = userId;
            _userAddressService.AddNewAddress(address);

            return RedirectToAction(nameof(Index)); 
        }



    }
}
