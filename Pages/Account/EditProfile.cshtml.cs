using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlaglerBookSwap.Pages.Account
{
    public class EditProfileModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditProfileModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EditProfileViewModel EditProfileViewModel { get; set; }
        public List<SelectListItem> GraduationYears { get; set; }




        public async Task<IActionResult>OnGetAsync(short userId)
        {
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user is not logged in, handle accordingly (e.g., redirect to login page)
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login/"); // Redirect to login if not logged in
            }

            userId = short.Parse(userIdString);

            // Fetch the user from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {           
                return NotFound("User not found.");
            }


            EditProfileViewModel = new EditProfileViewModel
            {
                

                    major = EditMajor.GetMajors(user),

                
                expected_grad_year = user.expected_grad_year,
                Phone_number = user.phone_number,
                gender = user.gender,
                profile_picture = user.profile_picture
            };
            GraduationYears = new List<string> { "2025", "2026", "2027", "2028", "2029", "2030", "2031" }
               .Select(x => new SelectListItem { Text = x, Value = x, Selected = x == user.expected_grad_year })
               .ToList();

            return Page();
        }


        private IActionResult ErrorEventArgs(string v)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user is not logged in, handle accordingly (e.g., redirect to login page)
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login"); // Redirect to login if not logged in
            }

            short userId = short.Parse(userIdString);
            // Fetch the user from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update the user with new inputs
            user.major = EditProfileViewModel.major.FirstOrDefault(m => m.Selected)?.Value;
            user.expected_grad_year = EditProfileViewModel.expected_grad_year;
            user.phone_number = EditProfileViewModel.Phone_number;
            user.gender = EditProfileViewModel.gender;

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
                else
                {
                    ModelState.AddModelError("File", "Invalid file type. Please upload an image.");
                    return Page();
                }
            }

            // Save changes to the database
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your profile has been updated successfully.";
            return Redirect("/Account/Profile/" + userId);
        }
    }
}
