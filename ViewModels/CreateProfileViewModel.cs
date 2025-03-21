using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FlaglerBookSwap.ViewModels
{
    public class CreateProfileViewModel
    {
        [Required(ErrorMessage = "Please check off your major")]
        public List<MajorViewModel> Major { get; set; }

        [Required(ErrorMessage = "Please pick your graduation year")]
        public string GradYear { get; set; }

        [Required(ErrorMessage="Please enter your phone number")]
        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string ProfilePic { get; set; }
    }

    public class MajorViewModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
