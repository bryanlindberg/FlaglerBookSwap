using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FlaglerBookSwap.ViewModels
{
    public class CreateProfileViewModel
    {
        [Required(ErrorMessage = "Please check off your major")]
        public List<CreateMajorViewModel> major { get; set; }

        [Required(ErrorMessage = "Please pick your graduation year")]
        public string expected_grad_year { get; set; }

        [StringLength(40, MinimumLength = 10)]
        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone_number { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password and Confirm Password must match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string? gender { get; set; } // Not required

        public byte[]? profile_picture { get; set; } // Not required        
        public string? flagler_email { get; set; }

    }

    public class CreateMajorViewModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
