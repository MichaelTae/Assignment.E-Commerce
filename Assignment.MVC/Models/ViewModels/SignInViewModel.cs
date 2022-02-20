using System.ComponentModel.DataAnnotations;

namespace Assignment.MVC.Models.ViewModels
{
    public class SignInViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email has to be a valid email address.")]
        [Required(ErrorMessage = "You have to enter an email address.")]
        [StringLength(100, ErrorMessage = "Email has to be a valid email address.", MinimumLength = 6)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You have to enter a Password.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password has to be atleast 8 characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
