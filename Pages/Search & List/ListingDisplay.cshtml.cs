using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap.Pages.Search___List
{
    public class ListingDisplayModel : PageModel
    {
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

            ListingDisplay = _context.Listings
                .Where(l => l.textbook_id == TextbookId)
                .Include(l => l.Textbooks)
                .Include(l => l.Users)
                .ToList();
        }
    }
}
