using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.ViewModels.User
{
    public class LoginViewModel
    {

        [Required(ErrorMessage ="Enter your emails please")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "EnterEmail")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter your password please")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool IsPersistent { get; set; } = false;
    
        public string? ReturnUrl { get; set; }
    }
}
