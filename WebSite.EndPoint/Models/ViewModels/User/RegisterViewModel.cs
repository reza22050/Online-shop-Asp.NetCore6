using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.ViewModels.Register
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Enter your name and lastname please")]
        [Display(Name ="Name and family")]
        [MaxLength(100, ErrorMessage = "FullName must be less than 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email must be unique")]
        [EmailAddress]
        [Display(Name = "Enter email")]
        public string Email { get; set; }
   
        
        [Required(ErrorMessage ="Enter your password please")]
        [DataType(DataType.Password)]
        [Display(Name ="Enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter your password again please")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter password again")]
        [Compare(nameof(Password), ErrorMessage ="Password and Repassword must be the same")]
        public string RePassword { get; set; }

        public string? PhoneNumber { get; set; }    
    }
}
