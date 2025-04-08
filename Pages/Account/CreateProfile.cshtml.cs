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

        public async Task<IActionResult> OnGetAsync()
        {               
            CreateProfileViewModel = new CreateProfileViewModel
            {
                Major = new List<CreateMajorViewModel>
                    {
                        new CreateMajorViewModel { Value = "Computer Information Systems", Text = "CIS" },
                        new CreateMajorViewModel { Value = "Business Administration", Text = "BUS ADMIN" },
                        new CreateMajorViewModel { Value = "Psychology", Text = "PSY" },
                        new CreateMajorViewModel { Value = "Coastal Environmental Science", Text = "ENV SCI" },
                        new CreateMajorViewModel { Value = "Elementary Education", Text = "ELE EDU" },
                        new CreateMajorViewModel { Value = "Elementary Exceptional Student Education", Text = "ELE EXC EDU" },
                        new CreateMajorViewModel { Value = "Graphic Design", Text = "ECO" },
                        new CreateMajorViewModel { Value = "Accounting", Text = "ACC" },
                        new CreateMajorViewModel { Value = "Marketing", Text = "MARK" },
                        new CreateMajorViewModel { Value = "Journalism", Text = "JOURN" },
                        new CreateMajorViewModel { Value = "Public Relations", Text = "PBR" },
                        new CreateMajorViewModel { Value = "Fine Arts", Text = "ART" },
                        new CreateMajorViewModel { Value = "History", Text = "HIS" },
                        new CreateMajorViewModel { Value = "Sport Management", Text = "SPT" },
                        new CreateMajorViewModel { Value = "Hospitality", Text = "HSP" },
                        new CreateMajorViewModel { Value = "English", Text = "ENG" },
                        new CreateMajorViewModel { Value = "English Literature", Text = "ENG LIT" },
                        new CreateMajorViewModel { Value = "Public Administration", Text = "PUB ADMIN" },
                        new CreateMajorViewModel { Value = "Global Studies", Text = "GLB STU" },
                        new CreateMajorViewModel { Value = "Political Science", Text = "POLY SCI" },
                        new CreateMajorViewModel { Value = "Education", Text = "EDU" },
                        new CreateMajorViewModel { Value = "Finance", Text = "FIN" },
                        new CreateMajorViewModel { Value = "Theatre Arts", Text = "THT ART" },
                        new CreateMajorViewModel { Value = "Liberal Arts", Text = "LBL ART" },
                        new CreateMajorViewModel { Value = "Sociology", Text = "SOC" },
                        new CreateMajorViewModel { Value = "Entrepreneurial", Text = "ENT" },
                        new CreateMajorViewModel { Value = "Media Studies", Text = "MDA STU" },
                        new CreateMajorViewModel { Value = "Deaf Education", Text = "DF EDU" },
                        new CreateMajorViewModel { Value = "International Business", Text = "INT BUS" },
                        new CreateMajorViewModel { Value = "International Studies", Text = "INT STU" },
                        new CreateMajorViewModel { Value = "Economics", Text = "ECON" },
                        new CreateMajorViewModel { Value = "Anthropology", Text = "ANTH" },
                        new CreateMajorViewModel { Value = "Mathematics", Text = "MAT" },
                        new CreateMajorViewModel { Value = "Philosophy", Text = "PHI" },
                        new CreateMajorViewModel { Value = "Art History", Text = "ART HIST" },
                        new CreateMajorViewModel { Value = "Management Information Systems", Text = "MIS" },
                        new CreateMajorViewModel { Value = "Public History", Text = "PUB HIST" },
                        new CreateMajorViewModel { Value = "Spanish", Text = "SPAN" },
                        new CreateMajorViewModel { Value = "Biology", Text = "BIO" },
                        new CreateMajorViewModel { Value = "Cinematic Arts", Text = "CIN ARTS" },
                        new CreateMajorViewModel { Value = "Criminology", Text = "CRM" },
                        new CreateMajorViewModel { Value = "Data Science", Text = "DTA SCI" },
                        new CreateMajorViewModel { Value = "Social Entrepreneurship", Text = "SOC ENT" },
                        new CreateMajorViewModel { Value = "Secondary Education Math", Text = "SEC MAT" },
                        new CreateMajorViewModel { Value = "Secondary Education English", Text = "SEC ENG" }
                    }
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
                var user = await _context.Users.FirstOrDefaultAsync(u => u.flagler_email == User.Identity.Name);
                if (user == null)
                {
                    return NotFound("User not found.");
                    
                }

                //*this is for me for future projects
                //Since i'm not using the identity framework then i have to instatiate the user manager below everytime
                //a user needs to save data to the account
                user.major = CreateProfileViewModel.Major.FirstOrDefault(m => m.Selected)?.Value;
                user.expected_grad_year = CreateProfileViewModel.GradYear;
                user.phone_number = CreateProfileViewModel.PhoneNumber;
                user.gender = CreateProfileViewModel.gender;


                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine("wwwroot/uploads", file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        CreateProfileViewModel.profilepicturepath = filePath;

                        user.profile_picture = filePath;

                    }
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
