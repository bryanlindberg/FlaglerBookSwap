using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FlaglerBookSwap.Models;
using Microsoft.IdentityModel.Logging;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FlaglerBookSwap.Data;
using Microsoft.EntityFrameworkCore;



namespace FlaglerBookSwap.Pages.Account
{
    public class CreateProfileModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CreateProfileModel> _logger;

        public CreateProfileModel(AppDbContext context, ILogger<CreateProfileModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public CreateProfileViewModel CreateProfileViewModel { get; set; }
        public List<SelectListItem> GraduationYears { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }


        public async Task<IActionResult> OnGetAsync(string email)
        {
            Email = email;

            if (!string.IsNullOrEmpty(email))
            {
                // Verify the email exists in the database
                var userExists = await _context.Users.AnyAsync(u => u.flagler_email == email);
                if (!userExists)
                {
                    return NotFound("User not found with this email.");
                }
            }

            CreateProfileViewModel = new CreateProfileViewModel
            {
                major = CreateMajor.GetMajors(),
                selected_major = new List<string>()

            };
            GraduationYears = new List<string> { "2025", "2026", "2027", "2028", "2029", "2030", "2031" }
               .Select(x => new SelectListItem { Text = x, Value = x })
               .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var user = await _context.Users.FirstOrDefaultAsync(u =>
                (Email == null && u.flagler_email == null) ||
                (Email != null && u.flagler_email == Email));
                if (user == null)
                {
                    return NotFound("User not found.");
                    
                }

                //this is for me for future projects Since i'm not using the identity framework
                //then i have to instatiate the user manager below everytime a user needs to save data to the account
                user.major = CreateProfileViewModel.selected_major.ElementAtOrDefault(0); // Primary major
                user.second_major = CreateProfileViewModel.selected_major.ElementAtOrDefault(1); // Second major
                user.third_major = CreateProfileViewModel.selected_major.ElementAtOrDefault(2);// Third major
                user.fourth_major = CreateProfileViewModel.selected_major.ElementAtOrDefault(3); // Fourth major
                user.expected_grad_year = CreateProfileViewModel.expected_grad_year;
                user.phone_number = CreateProfileViewModel.Phone_number;
                user.gender = CreateProfileViewModel.gender;
                user.Password = CreateProfileViewModel.Password;

                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            user.profile_picture = memoryStream.ToArray(); 
                        }
                    }
                }
                else
                {
                    user.profile_picture = null; 
                }

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User profile created.");

                TempData["SuccessMessage"] = "Your profile has been created successfully.";

                return RedirectToPage("/Index");
                           
                
            }
            return Page();
        }
    }
}
