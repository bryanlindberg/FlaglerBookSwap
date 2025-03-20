﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Flagler Email is required")]
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
        [MinimumAge(18, ErrorMessage = "You must be at least 18 years old to register")]
        public int BirthYear { get; set; }
    
    }

    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;
        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int birthYear)
            {
                var currentYear = DateTime.Now.Year;
                if(currentYear - birthYear >= _minimumAge)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? $"You must be at least {_minimumAge} years old to register");
                }
            }
            return new ValidationResult("Invalid birth year");
        }
    }       
}
