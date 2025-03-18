using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(40,MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")] //the video has "The  {0}must be at least {2} and at max {1} characters long." 
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password and Confirm Password must match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Passowrd is required")] //we don't have to add this 
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your birthyear")]
        public int BirthYear { get; set; }
    }
}
