using FlaglerBookSwap.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FlaglerBookSwap.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap.Pages.Account
{
    public class ProfileListingModel : PageModel
    {
        public List<Listings> ListingInfo { get; set; } = new List<Listings>();


        private readonly AppDbContext _context;

        public ProfileListingModel(AppDbContext context)
        {
            _context = context;
        }



        public void OnGet()
        {
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (short.TryParse(userIdString, out short userId))
            {
                ListingInfo = _context.Listings
                    .Where(l => l.userID == userId)
                    .Include(l => l.Textbooks)
                    .ToList();
            }
            else
            {
                throw new Exception("User ID is not valid.");
            }


        }

    }
}
