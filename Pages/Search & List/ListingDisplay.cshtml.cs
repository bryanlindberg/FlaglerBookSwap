using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap.Pages.Search___List
{
    public class ListingDisplayModel : PageModel
    {

        public string? Book_Title { get; set; }
        public double ISBN { get; set; }
        public string? Authors { get; set; }


        [BindProperty(SupportsGet = true)]
        public short TextbookId { get; set; }
        public List<Listings> ListingDisplay { get; set; } = new List<Listings>();

        private readonly AppDbContext _context;

        public ListingDisplayModel(AppDbContext context)
        {
            _context = context;
        }


        public void OnGet(short textbookId)
        {
            TextbookId = textbookId;
            //var textbookId = await _context

            ListingDisplay = _context.Listings
                .Where(l => l.textbook_id == TextbookId)
                .Include(l => l.Textbooks)
                .Include(l => l.Users)
                .ToList();

            var textbook = _context.Textbooks.FirstOrDefault(t => t.textbook_id == TextbookId);
            if (textbook != null)
            {
                Book_Title = textbook.Book_Title;
                ISBN = textbook.ISBN;
                Authors = textbook.Authors;
            }


        }
    }
}
