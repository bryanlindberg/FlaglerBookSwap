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
                Major = new List<MajorViewModel>
                {
                    new MajorViewModel { Value = "Computer Information Systems", Text = "CIS" }, //computer information systems
                    new MajorViewModel { Value = "Business Administration", Text = "BUS ADMIN" }, //business
                    new MajorViewModel { Value = "Psychology", Text = "PSY" }, //psychology
                    new MajorViewModel { Value = "Coastal Enviormental Science", Text = "ENV SCI" }, //Coastal Enviormental Science
                    new MajorViewModel { Value = "Elementary Education", Text = "ELE EDU" }, //Elementary Education
                    new MajorViewModel { Value = "Elementary Exceptional Student Education", Text = "ELE EXC EDU" }, //Elementary Education
                    new MajorViewModel { Value = "Graphic Design", Text = "ECO" }, //Graphic design
                    new MajorViewModel { Value = "Accounting", Text = "ACC" }, //Accounting
                    new MajorViewModel { Value = "Marketing", Text = "MARK" }, //Marketing
                    new MajorViewModel { Value = "Journalism", Text = "JOURN" }, //Journalism
                    new MajorViewModel { Value = "Public relations", Text = "PBR" }, //Public relations
                    new MajorViewModel { Value = "Fine Arts", Text = "ART" }, //Fine/studio arts
                    new MajorViewModel { Value = "History", Text = "HIS" }, //History
                    new MajorViewModel { Value = "Sport Management", Text = "SPT" }, //Sport Management
                    new MajorViewModel { Value = "Hospitality", Text = "HSP" }, //Hospitality
                    new MajorViewModel { Value = "English", Text = "ENG" }, //English
                    new MajorViewModel { Value = "English Literature", Text = "ENG LIT" }, //English Literature
                    new MajorViewModel { Value = "Public Administration", Text = "PUB ADMIN" }, //Public administration
                    new MajorViewModel { Value = "Global Studies", Text = "GLB STU" }, //Global Studies
                    new MajorViewModel { Value = "Political science", Text = "POLY SCI" }, //Political science
                    new MajorViewModel { Value = "Education", Text = "EDU" }, //Education
                    new MajorViewModel { Value = "Finance", Text = "FIN" }, //Finance
                    new MajorViewModel { Value = "Theatre Arts", Text = "THT ART" }, //Theatre Arts
                    new MajorViewModel { Value = "Liberal Arts", Text = "LBL ART" }, //Liberal arts
                    new MajorViewModel { Value = "Sociology", Text = "SOC" }, //Sociology
                    new MajorViewModel { Value = "Entrepreneurial", Text = "ENT" }, //Entrepreneurial
                    new MajorViewModel { Value = "Media Studies", Text = "MDA STU" }, //Media Studies
                    new MajorViewModel { Value = "Deaf Education", Text = "DF EDU" }, //Education/teaching of individuals with hearing impairments including deafness
                    new MajorViewModel { Value = "International Business", Text = "INT BUS" }, //International Business
                    new MajorViewModel { Value = "International Studies", Text = "INT STU" }, //International studies
                    new MajorViewModel { Value = "Economics", Text = "ECON" }, //Economics
                    new MajorViewModel { Value = "Anthropology", Text = "ANTH" }, //Anthropology
                    new MajorViewModel { Value = "Mathematics", Text = "MAT" }, //Mathematics
                    new MajorViewModel { Value = "Philosophy", Text = "PHI" }, //Philosophy 
                    new MajorViewModel { Value = "Art History", Text = "ART HIST" }, //Art History
                    new MajorViewModel { Value = "Management Information Systems", Text = "MIS" }, //Management Information Systems
                    new MajorViewModel { Value = "Public Hitory", Text = "PUB HIST" }, //Finance
                    new MajorViewModel { Value = "Spanish", Text = "SPAN" }, //Spanish
                    new MajorViewModel { Value = "Biology", Text = "BIO" }, //Biology
                    new MajorViewModel { Value = "Cinematic Arts", Text = "CIN ARTS" }, //Theatre Arts
                    new MajorViewModel { Value = "Criminology", Text = "CRM" }, //Criminology
                    new MajorViewModel { Value = "Data Science", Text = "DTA SCI" }, //data science
                    new MajorViewModel { Value = "Social Entrpreneurship", Text = "SOC ENT" }, //Biology
                    new MajorViewModel { Value = "Secondary Education Math", Text = "SEC MAT" }, //Secondary Education Math
                    new MajorViewModel { Value = "Secondary Education English", Text = "SEC ENG" } //Secondary Education English
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
