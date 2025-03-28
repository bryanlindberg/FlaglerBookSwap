using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap.Pages
{
    public class AboutUsModel : PageModel  // Inherit from PageModel
    {
        private readonly AppDbContext _context;

        //this method is used to connect to the database
        public AboutUsModel(AppDbContext context)
        {
            _context = context;
        }

        //using textbooks to test the database connection, it's unclear to me
        //if we'll need to do this on every individual page, or if we
        //can just do it once in the AppDbContext.cs file
        public IList<Textbooks> Textbooks { get; set; }

        //this method is used to get the data from the database
        public async Task OnGetAsync()
        {

            Textbooks = await _context.Textbooks.ToListAsync();
        }
    }
}
