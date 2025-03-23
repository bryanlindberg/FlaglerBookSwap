using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlaglerBookSwap.Pages.Account
{
    public class CreateProfileModel : PageModel
    {
        [BindProperty]
        public CreateProfileViewModel CreateProfileViewModel { get; set; }
        public List<SelectListItem> GraduationYears { get; set; }
        public void OnGet()
        {
            CreateProfileViewModel = new CreateProfileViewModel
            {
                //Idk how to abbrevaite half these majors
                Major = new List<CreateMajorViewModel>
                {
                    new CreateMajorViewModel { Value = "Computer Information Systems", Text = "CIS" }, //computer information systems
                    new CreateMajorViewModel { Value = "Business Administration", Text = "BUS ADMIN" }, //business
                    new CreateMajorViewModel { Value = "Psychology", Text = "PSY" }, //psychology
                    new CreateMajorViewModel { Value = "Coastal Enviormental Science", Text = "ENV SCI" }, //Coastal Enviormental Science
                    new CreateMajorViewModel { Value = "Elementary Education", Text = "ELE EDU" }, //Elementary Education
                    new CreateMajorViewModel { Value = "Elementary Exceptional Student Education", Text = "ELE EXC EDU" }, //Elementary Education
                    new CreateMajorViewModel { Value = "Graphic Design", Text = "ECO" }, //Graphic design
                    new CreateMajorViewModel { Value = "Accounting", Text = "ACC" }, //Accounting
                    new CreateMajorViewModel { Value = "Marketing", Text = "MARK" }, //Marketing
                    new CreateMajorViewModel { Value = "Journalism", Text = "JOURN" }, //Journalism
                    new CreateMajorViewModel { Value = "Public relations", Text = "PBR" }, //Public relations
                    new CreateMajorViewModel { Value = "Fine Arts", Text = "ART" }, //Fine/studio arts
                    new CreateMajorViewModel { Value = "History", Text = "HIS" }, //History
                    new CreateMajorViewModel { Value = "Sport Management", Text = "SPT" }, //Sport Management
                    new CreateMajorViewModel { Value = "Hospitality", Text = "HSP" }, //Hospitality
                    new CreateMajorViewModel { Value = "English", Text = "ENG" }, //English
                    new CreateMajorViewModel { Value = "English Literature", Text = "ENG LIT" }, //English Literature
                    new CreateMajorViewModel { Value = "Public Administration", Text = "PUB ADMIN" }, //Public administration
                    new CreateMajorViewModel { Value = "Global Studies", Text = "GLB STU" }, //Global Studies
                    new CreateMajorViewModel { Value = "Political science", Text = "POLY SCI" }, //Political science
                    new CreateMajorViewModel { Value = "Education", Text = "EDU" }, //Education
                    new CreateMajorViewModel { Value = "Finance", Text = "FIN" }, //Finance
                    new CreateMajorViewModel { Value = "Theatre Arts", Text = "THT ART" }, //Theatre Arts
                    new CreateMajorViewModel { Value = "Liberal Arts", Text = "LBL ART" }, //Liberal arts
                    new CreateMajorViewModel { Value = "Sociology", Text = "SOC" }, //Sociology
                    new CreateMajorViewModel { Value = "Entrepreneurial", Text = "ENT" }, //Entrepreneurial
                    new CreateMajorViewModel { Value = "Media Studies", Text = "MDA STU" }, //Media Studies
                    new CreateMajorViewModel { Value = "Deaf Education", Text = "DF EDU" }, //Education/teaching of individuals with hearing impairments including deafness
                    new CreateMajorViewModel { Value = "International Business", Text = "INT BUS" }, //International Business
                    new CreateMajorViewModel { Value = "International Studies", Text = "INT STU" }, //International studies
                    new CreateMajorViewModel { Value = "Economics", Text = "ECON" }, //Economics
                    new CreateMajorViewModel { Value = "Anthropology", Text = "ANTH" }, //Anthropology
                    new CreateMajorViewModel { Value = "Mathematics", Text = "MAT" }, //Mathematics
                    new CreateMajorViewModel { Value = "Philosophy", Text = "PHI" }, //Philosophy 
                    new CreateMajorViewModel { Value = "Art History", Text = "ART HIST" }, //Art History
                    new CreateMajorViewModel { Value = "Management Information Systems", Text = "MIS" }, //Management Information Systems
                    new CreateMajorViewModel { Value = "Public Hitory", Text = "PUB HIST" }, //Finance
                    new CreateMajorViewModel { Value = "Spanish", Text = "SPAN" }, //Spanish
                    new CreateMajorViewModel { Value = "Biology", Text = "BIO" }, //Biology
                    new CreateMajorViewModel { Value = "Cinematic Arts", Text = "CIN ARTS" }, //Theatre Arts
                    new CreateMajorViewModel { Value = "Criminology", Text = "CRM" }, //Criminology
                    new CreateMajorViewModel { Value = "Data Science", Text = "DTA SCI" }, //data science
                    new CreateMajorViewModel { Value = "Social Entrpreneurship", Text = "SOC ENT" }, //Biology
                    new CreateMajorViewModel { Value = "Secondary Education Math", Text = "SEC MAT" }, //Secondary Education Math
                    new CreateMajorViewModel { Value = "Secondary Education English", Text = "SEC ENG" } //Secondary Education English
                }
            };
            GraduationYears = new List<string> { "2025", "2026", "2027", "2028", "2029", "2030", "2031" }
               .Select(x => new SelectListItem { Text = x, Value = x })
               .ToList();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Save the profile data
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
