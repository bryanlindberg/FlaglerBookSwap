using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap.Pages.Account
{
    public class AdminModel : PageModel
    {
        private readonly AppDbContext _context;

        public AdminModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public short TextbookID { get; set; }

        [BindProperty]
        public IFormFile? TextbookPhotoUpload { get; set; }

        public List<Textbooks> Textbooks { get; set; } = new();



        public async Task<IActionResult> OnPostAsync()
        {
            if (TextbookPhotoUpload != null && TextbookPhotoUpload.Length > 0)
            {
                var textbook = await _context.Textbooks.FindAsync(TextbookID);
                if (textbook != null)
                {
                    using var ms = new MemoryStream();
                    await TextbookPhotoUpload.CopyToAsync(ms);
                    textbook.TextbookPhoto = ms.ToArray();

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage(); // Refresh the page or go to a confirmation
        
        }


        public async Task OnGetAsync()
        {
            Textbooks = await _context.Textbooks.OrderBy(t => t.Book_Title).ToListAsync();
        }
    }
}
