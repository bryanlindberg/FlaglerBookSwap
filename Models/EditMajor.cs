using FlaglerBookSwap.Models;
using FlaglerBookSwap.ViewModels;

namespace FlaglerBookSwap.Models
{
    public class EditMajor
    {
        private static readonly List<(string Value, string Text)> Majors = new()
        {
            ("Computer Information Systems", "CIS"),
            ("Business Administration", "BUS ADMIN"),
            ("Psychology", "PSY"),
            ("Coastal Environmental Science", "ENV SCI"),
            ("Elementary Education", "ELE EDU"),
            ("Elementary Exceptional Student Education", "ELE EXC EDU"),
            ("Graphic Design", "ECO"),
            ("Accounting", "ACC"),
            ("Marketing", "MARK"),
            ("Journalism", "JOURN"),
            ("Public Relations", "PBR"),
            ("Fine Arts", "ART"),
            ("History", "HIS"),
            ("Sport Management", "SPT"),
            ("Hospitality", "HSP"),
            ("English", "ENG"),
            ("English Literature", "ENG LIT"),
            ("Public Administration", "PUB ADMIN"),
            ("Global Studies", "GLB STU"),
            ("Political Science", "POLY SCI"),
            ("Education", "EDU"),
            ("Finance", "FIN"),
            ("Theatre Arts", "THT ART"),
            ("Liberal Arts", "LBL ART"),
            ("Sociology", "SOC"),
            ("Entrepreneurial", "ENT"),
            ("Media Studies", "MDA STU"),
            ("Deaf Education", "DF EDU"),
            ("International Business", "INT BUS"),
            ("International Studies", "INT STU"),
            ("Economics", "ECON"),
            ("Anthropology", "ANTH"),
            ("Mathematics", "MAT"),
            ("Philosophy", "PHI"),
            ("Art History", "ART HIST"),
            ("Management Information Systems", "MIS"),
            ("Public History", "PUB HIST"),
            ("Spanish", "SPAN"),
            ("Biology", "BIO"),
            ("Cinematic Arts", "CIN ARTS"),
            ("Criminology", "CRM"),
            ("Data Science", "DTA SCI"),
            ("Social Entrepreneurship", "SOC ENT"),
            ("Secondary Education Math", "SEC MAT"),
            ("Secondary Education English", "SEC ENG")
        };

        public static List<EditMajorViewModel> GetMajors(Users user)
        {
            return Majors.Select(major => new EditMajorViewModel
            {
                Value = major.Value,
                Text = major.Text,
                Selected = user.major == major.Value
            }).ToList();
        }
    }
}
