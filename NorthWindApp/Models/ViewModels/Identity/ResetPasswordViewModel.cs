using System.ComponentModel.DataAnnotations;

namespace NorthWindApp.Models.ViewModels.Identity
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password must contain as minimum 6 symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords does not equal")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
