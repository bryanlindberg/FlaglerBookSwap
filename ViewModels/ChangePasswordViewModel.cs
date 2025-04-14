using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.ViewModels
{
    public class ChangePasswordViewModel
    {
        //we can use this model for change info of profile 
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")] 
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password and Confirm Password must match")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Passowrd is required")] //we don't have to add this 
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
