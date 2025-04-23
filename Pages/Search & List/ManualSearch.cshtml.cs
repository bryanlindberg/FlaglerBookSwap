using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlaglerBookSwap.Pages.Search___List
{
    public class ManualSearchModel : PageModel
    {
        [BindProperty]
        public string? SearchOption { get; set; }
        [BindProperty]
        public string? SearchObject { get; set; }
        public List<Textbooks> TextbookList { get; set; } = new List<Textbooks>();
        public string Message { get; set; } = string.Empty;

        private readonly AppDbContext _context;

        public ManualSearchModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnPost()
        {
            if (SearchOption == "ISBN")
            {
                if (string.IsNullOrWhiteSpace(SearchObject) || !double.TryParse(SearchObject, out _))
                {
                    Message = "Please enter a valid ISBN. Please enter numbers only without hyphens.";
                    return;
                }
                else if (string.IsNullOrWhiteSpace(SearchObject))
                {
                    Message = "Please enter an ISBN. Please enter numbers only without hyphens";
                    return;
                }

                double isbnID = double.Parse(SearchObject);
                TextbookList = _context.Textbooks
                    .Where(t => t.ISBN == isbnID)
                    .ToList();
            }
            if (SearchOption == "Textbook Title")
            {
                if (string.IsNullOrWhiteSpace(SearchObject))
                {
                    Message = "Please enter a textbook title.";
                    return;
                }
                else if (SearchObject.Length < 3)
                {
                    Message = "Please enter at least 3 characters.";
                    return;
                }
                TextbookList = _context.Textbooks
                    .Where(t => t.Book_Title.Contains(SearchObject))
                    .ToList();
            }
            if (SearchOption == "Author")
            {
                if (string.IsNullOrWhiteSpace(SearchObject))
                {
                    Message = "Please enter an author.";
                    return;
                }
                else if (SearchObject.Length < 3)
                {
                    Message = "Please enter at least 3 characters.";
                    return;
                }
                TextbookList = _context.Textbooks
                    .Where(t => t.Authors.Contains(SearchObject))
                    .ToList();
            }
        }
        public void OnGet()
        {
        }
    }
}