using FlaglerBookSwap.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlaglerBookSwap.Pages
{
    public class SearchingModel : PageModel
    {

        [BindProperty]
        public string ClassNumber { get; set; }
        [BindProperty]
        public string Subject { get; set; }
        [BindProperty]
        public string Section { get; set; }
        [BindProperty]
        public string Term { get; set; }
        public string Message { get; set; }

        private readonly AppDbContext _context;

        public SearchingModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(ClassNumber))
            {
                Message = "Please enter a class number.";
                return;
            }

            else if (string.IsNullOrWhiteSpace(Section))
            {
                Message = "Please enter a section.";
                return;
            }

            /*
             * alternative way to search for course, could be a bit cleaner/easier to debug
            string courseSubject = Subject;
            string courseNumber = ClassNumber;
            string courseLookup = courseSubject + " " + courseNumber;
            var course = _context.Courses
                .Where(c => c.course_code == courseLookup && c.section == Section && c.term == Term)
                .FirstOrDefault();
            */

            var course = _context.Courses
                .Where(c => c.course_code == Subject + " " + ClassNumber && c.section == Section && c.term == Term)
                .FirstOrDefault();

            if (course == null)
            {
                Message = "No courses found with that class number.";
                return;
            }

            var courseTextbooks = _context.Courses_Textbooks
                .Where(ct => ct.course_id == course.course_id)
                .ToList();

            if (!courseTextbooks.Any())
            {
                Message = "No textbooks found for this course.";
                return;
            }

            var textbookIds = courseTextbooks.Select(ct => ct.textbook_id).ToList();

            var textbooks = _context.Textbooks
                .Where(t => textbookIds.Contains(t.textbook_id))
                .ToList();

            if (!textbooks.Any())
            {
                Message = "No textbooks found.";
            }
            else
            {
                Message = "Textbooks: \n" + string.Join("\n", textbooks.Select(t => t.Book_Title));
            }
        }
        public void OnGet()
        {
        }
    }
}
