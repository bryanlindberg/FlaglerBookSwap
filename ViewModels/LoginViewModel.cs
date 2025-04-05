using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Flagler Email is required")]
        [EmailAddress]
        public string flagler_email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
