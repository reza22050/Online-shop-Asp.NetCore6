using Application.Catalogs.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Models.ViewComponents
{
    public class GetMenuCategories:ViewComponent
    {
        private readonly IGetMenuItemService _getMenuItemService;

        public GetMenuCategories(IGetMenuItemService getMenuItemService)
        {
            _getMenuItemService = getMenuItemService;
        }

        public IViewComponentResult Invoke()
        {
            var data = _getMenuItemService.Execute();
            return View(viewName: "GetMenuCategories", model: data);
        }

    }
}
