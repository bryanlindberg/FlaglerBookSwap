using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlaglerBookSwap.Pages.Account
{
    public class ProfileWishlistModel : PageModel
    {
        public List<Wishlist> WishlistInfo { get; set; } = new List<Wishlist>();
        private readonly AppDbContext _context;

        public ProfileWishlistModel(AppDbContext context)
        {
            _context = context;
        }

        private short GetNextAvailableWishlistId()
        {
            short maxId = (short)(_context.Wishlist.Any() ? _context.Wishlist.Max(w => w.WishlistID) : (short)0);
            return (short)(maxId + 1);
        }

        public async Task<IActionResult> OnGetAddToWishlistAsync(short textbookId)
        {

            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user is not logged in, handle accordingly (e.g., redirect to login page)
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login"); // Redirect to login if not logged in
            }

            short userId = short.Parse(userIdString);

            //Checks if the textbook is already in the wishlist
            bool isAlreadyInWishlist = _context.Wishlist.Any(w => w.userID == userId && w.textbook_id == textbookId);
            if (isAlreadyInWishlist)
            {
                return RedirectToPage("/Account/ProfileWishlist");
            }
            else
            {
                var wishlistItem = new Wishlist
                {
                    WishlistID = GetNextAvailableWishlistId(),
                    wishlist_status = true,
                    userID = userId,
                    textbook_id = textbookId,
                    date_wishlisted = DateTime.Now
                };

                _context.Wishlist.Add(wishlistItem);
                await _context.SaveChangesAsync();

                // Redirect to wishlist page (without query param)
                return RedirectToPage("/Account/ProfileWishlist");
            }
        }


        public void OnGet()
        {
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (short.TryParse(userIdString, out short userId))
            {
                WishlistInfo = _context.Wishlist
                    .Where(w => w.userID == userId)
                    .Include(w => w.Textbooks)
                    .ToList();
            }
            else
            {
                throw new Exception("User ID is not valid.");
            }

        }
    }
}
