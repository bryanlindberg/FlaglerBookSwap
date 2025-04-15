using FlaglerBookSwap.Data;
using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap.Pages.Account
{
    public class EditProfileModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditProfileModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }

        [BindProperty]
        public EditProfileViewModel EditProfileViewModel { get; set; }
        public List<SelectListItem> GraduationYears { get; set; }


        public async Task<IActionResult>OnGetAsync()
        {          
            //Email = email
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
            {
                return ErrorEventArgs("Email is not provided.");
            }

            // get the user from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.flagler_email == email);
            if (user != null)
            {
                Console.WriteLine($"Found user with ID: {user.UserID}");
            }
            else
            {
                Console.WriteLine("No user found with email: " + email);
                // List all emails in the database for debugging
                var allEmails = await _context.Users.Select(u => u.flagler_email).ToListAsync();
                Console.WriteLine("Available emails: " + string.Join(", ", allEmails));
            }

            EditProfileViewModel = new EditProfileViewModel
            {
                //Idk how to abbrevaite half these majors
                major = new List<EditMajorViewModel>
                {
                    new EditMajorViewModel { Value = "Computer Information Systems", Text = "CIS",Selected = user.major == "Computer Information Systems"}, //computer information systems
                    new EditMajorViewModel { Value = "Business Administration", Text = "BUS ADMIN",Selected = user.major == "Business Administration"}, //business
                    new EditMajorViewModel { Value = "Psychology", Text = "PSY",Selected = user.major == "Psychology" }, //psychology
                    new EditMajorViewModel { Value = "Coastal Enviormental Science", Text = "ENV SCI",Selected = user.major == "Coastal Enviormental Science" }, //Coastal Enviormental Science
                    new EditMajorViewModel { Value = "Elementary Education", Text = "ELE EDU",Selected = user.major == "Elementary Education" }, //Elementary Education
                    new EditMajorViewModel { Value = "Elementary Exceptional Student Education", Text = "ELE EXC EDU",Selected = user.major == "Elementary Exceptional Student Education" }, //Elementary Education
                    new EditMajorViewModel { Value = "Graphic Design", Text = "ECO",Selected = user.major == "Graphic Design" }, //Graphic design
                    new EditMajorViewModel { Value = "Accounting", Text = "ACC",Selected = user.major == "Accounting" }, //Accounting
                    new EditMajorViewModel { Value = "Marketing", Text = "MARK",Selected = user.major == "Marketing" }, //Marketing
                    new EditMajorViewModel { Value = "Journalism", Text = "JOURN",Selected = user.major == "Journalism" }, //Journalism
                    new EditMajorViewModel { Value = "Public relations", Text = "PBR",Selected = user.major == "Public relations" }, //Public relations
                    new EditMajorViewModel { Value = "Fine Arts", Text = "ART",Selected = user.major == "Fine/studio arts" }, //Fine/studio arts
                    new EditMajorViewModel { Value = "History", Text = "HIS",Selected = user.major == "Histor" }, //History
                    new EditMajorViewModel { Value = "Sport Management", Text = "SPT",Selected = user.major == "Sport Management" }, //Sport Management
                    new EditMajorViewModel { Value = "Hospitality", Text = "HSP",Selected = user.major == "Hospitality" }, //Hospitality
                    new EditMajorViewModel { Value = "English", Text = "ENG",Selected = user.major == "English" }, //English
                    new EditMajorViewModel { Value = "English Literature", Text = "ENG LIT",Selected = user.major == "English Literature" }, //English Literature
                    new EditMajorViewModel { Value = "Public Administration", Text = "PUB ADMIN",Selected = user.major == "Public administration" }, //Public administration
                    new EditMajorViewModel { Value = "Global Studies", Text = "GLB STU",Selected = user.major == "Global Studies" }, //Global Studies
                    new EditMajorViewModel { Value = "Political science", Text = "POLY SCI",Selected = user.major == "Political science" }, //Political science
                    new EditMajorViewModel { Value = "Education", Text = "EDU",Selected = user.major == "Education" }, //Education
                    new EditMajorViewModel { Value = "Finance", Text = "FIN",Selected = user.major == "Finance" }, //Finance
                    new EditMajorViewModel { Value = "Theatre Arts", Text = "THT ART",Selected = user.major == "Theatre Arts" }, //Theatre Arts
                    new EditMajorViewModel { Value = "Liberal Arts", Text = "LBL ART",Selected = user.major == "Liberal Arts" }, //Liberal arts
                    new EditMajorViewModel { Value = "Sociology", Text = "SOC",Selected = user.major == "Sociology" }, //Sociology
                    new EditMajorViewModel { Value = "Entrepreneurial", Text = "ENT",Selected = user.major == "Entrepreneurial" }, //Entrepreneurial
                    new EditMajorViewModel { Value = "Media Studies", Text = "MDA STU",Selected = user.major == "Media Studies" }, //Media Studies
                    new EditMajorViewModel { Value = "Deaf Education", Text = "DF EDU",Selected = user.major == "Deaf Education" }, //Education/teaching of individuals with hearing impairments including deafness
                    new EditMajorViewModel { Value = "International Business", Text = "INT BUS",Selected = user.major == "International Business" }, //International Business
                    new EditMajorViewModel { Value = "International Studies", Text = "INT STU",Selected = user.major == "International Studies" }, //International studies
                    new EditMajorViewModel { Value = "Economics", Text = "ECON",Selected = user.major == "Economics" }, //Economics
                    new EditMajorViewModel { Value = "Anthropology", Text = "ANTH" , Selected = user.major == "Anthropology"}, //Anthropology
                    new EditMajorViewModel { Value = "Mathematics", Text = "MAT" , Selected = user.major == "Mathematics"}, //Mathematics
                    new EditMajorViewModel { Value = "Philosophy", Text = "PHI" ,Selected = user.major == "Philosophy"}, //Philosophy 
                    new EditMajorViewModel { Value = "Art History", Text = "ART HIST",Selected = user.major == "Art History" }, //Art History
                    new EditMajorViewModel { Value = "Management Information Systems", Text = "MIS",Selected = user.major == "Management Information Systems" }, //Management Information Systems
                    new EditMajorViewModel { Value = "Public Hitory", Text = "PUB HIST",Selected = user.major == "Public Hitory" }, //Finance
                    new EditMajorViewModel { Value = "Spanish", Text = "SPAN",Selected = user.major == "Spanish" }, //Spanish
                    new EditMajorViewModel { Value = "Biology", Text = "BIO",Selected = user.major == "Biology" }, //Biology
                    new EditMajorViewModel { Value = "Cinematic Arts", Text = "CIN ARTS",Selected = user.major == "Cinematic Arts" }, //Theatre Arts
                    new EditMajorViewModel { Value = "Criminology", Text = "CRM",Selected = user.major == "Criminology" }, //Criminology
                    new EditMajorViewModel { Value = "Data Science", Text = "DTA SCI",Selected = user.major == "Data Science" }, //data science
                    new EditMajorViewModel { Value = "Social Entrpreneurship", Text = "SOC ENT" , Selected = user.major == "Social Entrpreneurship"}, //Biology
                    new EditMajorViewModel { Value = "Secondary Education Math", Text = "SEC MAT" ,Selected = user.major == "Secondary Education Math"}, //Secondary Education Math
                    new EditMajorViewModel { Value = "Secondary Education English", Text = "SEC ENG" ,Selected = user.major == "Secondary Education English"} //Secondary Education English
                },
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

            // Fetch the user from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.flagler_email == Email);
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
            }

            // Save changes to the database
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your profile has been updated successfully.";
            return RedirectToPage("Profile");
        }
    }
}
