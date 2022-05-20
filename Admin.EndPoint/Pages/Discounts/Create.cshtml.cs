using Admin.EndPoint.Binders;
using Application.Discounts.AddNewDiscountServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.Discounts
{
    public class CreateModel : PageModel
    {
        private readonly IAddNewDiscountServices addNewDiscountServices;

        public CreateModel(IAddNewDiscountServices _addNewDiscountServices)
        {
            this.addNewDiscountServices = _addNewDiscountServices;
        }

        [ModelBinder(BinderType = typeof(DiscountEntityBinder))]
        [BindProperty]
        public AddNewDiscountDto model { get; set; }
        
        public void OnGet()
        {
        }

        public void OnPost()
        {
            addNewDiscountServices.Execute(model);
        }


    }
}
