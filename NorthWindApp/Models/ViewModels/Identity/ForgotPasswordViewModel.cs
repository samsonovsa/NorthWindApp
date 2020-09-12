using System.ComponentModel.DataAnnotations;

namespace NorthWindApp.Models.ViewModels.Identity
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
