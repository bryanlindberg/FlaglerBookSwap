using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.ViewModels
{
    public class EditProfileViewModel
    {
   
        [Required(ErrorMessage = "Please check off your major")]
        public List<EditMajorViewModel>? major { get; set; }

        [Required(ErrorMessage = "Please pick your graduation year")]
        public string? expected_grad_year { get; set; }

        [StringLength(40, MinimumLength = 10)]
        [Required(ErrorMessage = "Please enter your phone number")]
        public string? Phone_number { get; set; }
        public string? Email { get; set; }
        public string? gender { get; set; } 
        public byte[]? profile_picture { get; set; }
    }

    public class EditMajorViewModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
