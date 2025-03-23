using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlaglerBookSwap.Pages.Account
{
    public class EditProfileModel : PageModel
    {
        [BindProperty]
        public EditProfileViewModel EditProfileViewModel { get; set; }
        public List<SelectListItem> GraduationYears { get; set; }
        public void OnGet()
        {
            EditProfileViewModel = new EditProfileViewModel
            {
                //Idk how to abbrevaite half these majors
                Major = new List<EditMajorViewModel>
                {
                    new EditMajorViewModel { Value = "Computer Information Systems", Text = "CIS" }, //computer information systems
                    new EditMajorViewModel { Value = "Business Administration", Text = "BUS ADMIN" }, //business
                    new EditMajorViewModel { Value = "Psychology", Text = "PSY" }, //psychology
                    new EditMajorViewModel { Value = "Coastal Enviormental Science", Text = "ENV SCI" }, //Coastal Enviormental Science
                    new EditMajorViewModel { Value = "Elementary Education", Text = "ELE EDU" }, //Elementary Education
                    new EditMajorViewModel { Value = "Elementary Exceptional Student Education", Text = "ELE EXC EDU" }, //Elementary Education
                    new EditMajorViewModel { Value = "Graphic Design", Text = "ECO" }, //Graphic design
                    new EditMajorViewModel { Value = "Accounting", Text = "ACC" }, //Accounting
                    new EditMajorViewModel { Value = "Marketing", Text = "MARK" }, //Marketing
                    new EditMajorViewModel { Value = "Journalism", Text = "JOURN" }, //Journalism
                    new EditMajorViewModel { Value = "Public relations", Text = "PBR" }, //Public relations
                    new EditMajorViewModel { Value = "Fine Arts", Text = "ART" }, //Fine/studio arts
                    new EditMajorViewModel { Value = "History", Text = "HIS" }, //History
                    new EditMajorViewModel { Value = "Sport Management", Text = "SPT" }, //Sport Management
                    new EditMajorViewModel { Value = "Hospitality", Text = "HSP" }, //Hospitality
                    new EditMajorViewModel { Value = "English", Text = "ENG" }, //English
                    new EditMajorViewModel { Value = "English Literature", Text = "ENG LIT" }, //English Literature
                    new EditMajorViewModel { Value = "Public Administration", Text = "PUB ADMIN" }, //Public administration
                    new EditMajorViewModel { Value = "Global Studies", Text = "GLB STU" }, //Global Studies
                    new EditMajorViewModel { Value = "Political science", Text = "POLY SCI" }, //Political science
                    new EditMajorViewModel { Value = "Education", Text = "EDU" }, //Education
                    new EditMajorViewModel { Value = "Finance", Text = "FIN" }, //Finance
                    new EditMajorViewModel { Value = "Theatre Arts", Text = "THT ART" }, //Theatre Arts
                    new EditMajorViewModel { Value = "Liberal Arts", Text = "LBL ART" }, //Liberal arts
                    new EditMajorViewModel { Value = "Sociology", Text = "SOC" }, //Sociology
                    new EditMajorViewModel { Value = "Entrepreneurial", Text = "ENT" }, //Entrepreneurial
                    new EditMajorViewModel { Value = "Media Studies", Text = "MDA STU" }, //Media Studies
                    new EditMajorViewModel { Value = "Deaf Education", Text = "DF EDU" }, //Education/teaching of individuals with hearing impairments including deafness
                    new EditMajorViewModel { Value = "International Business", Text = "INT BUS" }, //International Business
                    new EditMajorViewModel { Value = "International Studies", Text = "INT STU" }, //International studies
                    new EditMajorViewModel { Value = "Economics", Text = "ECON" }, //Economics
                    new EditMajorViewModel { Value = "Anthropology", Text = "ANTH" }, //Anthropology
                    new EditMajorViewModel { Value = "Mathematics", Text = "MAT" }, //Mathematics
                    new EditMajorViewModel { Value = "Philosophy", Text = "PHI" }, //Philosophy 
                    new EditMajorViewModel { Value = "Art History", Text = "ART HIST" }, //Art History
                    new EditMajorViewModel { Value = "Management Information Systems", Text = "MIS" }, //Management Information Systems
                    new EditMajorViewModel { Value = "Public Hitory", Text = "PUB HIST" }, //Finance
                    new EditMajorViewModel { Value = "Spanish", Text = "SPAN" }, //Spanish
                    new EditMajorViewModel { Value = "Biology", Text = "BIO" }, //Biology
                    new EditMajorViewModel { Value = "Cinematic Arts", Text = "CIN ARTS" }, //Theatre Arts
                    new EditMajorViewModel { Value = "Criminology", Text = "CRM" }, //Criminology
                    new EditMajorViewModel { Value = "Data Science", Text = "DTA SCI" }, //data science
                    new EditMajorViewModel { Value = "Social Entrpreneurship", Text = "SOC ENT" }, //Biology
                    new EditMajorViewModel { Value = "Secondary Education Math", Text = "SEC MAT" }, //Secondary Education Math
                    new EditMajorViewModel { Value = "Secondary Education English", Text = "SEC ENG" } //Secondary Education English
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
                return Page();
            }
            // Handle the form submission logic here

            return RedirectToPage("Profile");
        }
    }
}
