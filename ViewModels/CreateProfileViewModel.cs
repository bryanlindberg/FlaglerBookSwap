﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FlaglerBookSwap.ViewModels
{
    public class CreateProfileViewModel
    {
        [Required(ErrorMessage = "Please check off your major")]
        public List<CreateMajorViewModel> Major { get; set; }

        [Required(ErrorMessage = "Please pick your graduation year")]
        public string GradYear { get; set; }

        [StringLength(40, MinimumLength = 10)]
        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }

        public string Gender { get; set; } // Not required

        public string ProfilePic { get; set; } // Not required
    }

    public class CreateMajorViewModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
