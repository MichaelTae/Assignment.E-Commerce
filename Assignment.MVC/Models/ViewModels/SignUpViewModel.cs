using System.ComponentModel.DataAnnotations;

namespace Assignment.MVC.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Display(Name = "First name.")]
        [Required(ErrorMessage = "You have to enter a firstname.")]
        [StringLength(100, ErrorMessage = "Your firstname has to be atleast 2 characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Last name.")]
        [Required(ErrorMessage = "You have to enter a last name.")]
        [StringLength(100, ErrorMessage = "Your last name has to be atleast 2 characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Your email has to be valid.")]
        [Required(ErrorMessage = "You have to enter an email.")]
        [StringLength(100, ErrorMessage = "You have to enter a valid email address.", MinimumLength = 6)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You have to enter a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Your password has to be atleast 8 characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "You have to confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Street")]
        [Required(ErrorMessage = "You have to enter a street.")]
        [StringLength(100, ErrorMessage = "Your street name has to be atleast 2 characters long.", MinimumLength = 2)]
        public string Street { get; set; }

        [Display(Name = "Postal code")]
        [Required(ErrorMessage = "You have to enter a postal code.")]
        [StringLength(5, ErrorMessage = "Your postal code has to be atleast 5 characters long.", MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "You have to enter a City.")]
        [StringLength(100, ErrorMessage = "Your City has to be atleast 2 characters long.", MinimumLength = 2)]
        public string City { get; set; }
    }
}
