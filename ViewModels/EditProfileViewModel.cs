using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.ViewModels
{
    public class EditProfileViewModel
    {
   
        [Required(ErrorMessage = "Please check off your major")]
        public List<EditMajorViewModel> Major { get; set; }

        [Required(ErrorMessage = "Please pick your graduation year")]
        public string GradYear { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string ProfilePic { get; set; }
    }

    public class EditMajorViewModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
