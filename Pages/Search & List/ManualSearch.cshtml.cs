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

        private readonly AppDbContext _context;

        public ManualSearchModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnPost()
        {
            if (SearchOption == "ISBN")
            {
                double isbnID = double.Parse(SearchObject);
                TextbookList = _context.Textbooks
                    .Where(t => t.ISBN == isbnID)
                    .ToList();
            }
            if (SearchOption == "Textbook Title")
            {
                TextbookList = _context.Textbooks
                    .Where(t => t.Book_Title.Contains(SearchObject))
                    .ToList();
            }
            if (SearchOption == "Author")
            {
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