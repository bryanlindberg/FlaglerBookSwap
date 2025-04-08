using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FlaglerBookSwap.Pages.Search___List
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public decimal Price { get; set; }
        [BindProperty]
        public bool isSwapping { get; set; }
        [BindProperty]
        public string contactPref { get; set; }
        [BindProperty]
        public string textbookCondition { get; set; }
        [BindProperty]
        public string textbookEdition { get; set; }
        [BindProperty(SupportsGet = true)]
        public short TextbookId { get; set; }
        public byte[] textbookImage { get; set; }

        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }




        public void OnGet(short textbookId)
        {
            TextbookId = textbookId;
        }

        private short GetNextAvailableListingId()
        {
            short maxId = (short)(_context.Listings.Any() ? _context.Listings.Max(l => l.ListingID) : (short)0);
            return (short)(maxId + 1);
        }

        public IActionResult OnPost()
        {

            // Retrieve the logged-in user's ID
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user is not logged in, handle accordingly (e.g., redirect to login page)
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login"); // Redirect to login if not logged in
            }

            short userId = short.Parse(userIdString);



            var newTextbookListing = new Listings
            {
                ListingID = GetNextAvailableListingId(), 
                date_listed = DateTime.Now,
                price = Price,
                is_willing_to_trade = isSwapping,
                //contact_pref = contactPref,
                condition = textbookCondition,
                edition = textbookEdition,
                //photo = textbookImage
                textbook_id = TextbookId,
                userID = userId
            };

            // Save the new textbook listing to the database or perform any other necessary actions
            _context.Listings.Add(newTextbookListing);
            _context.SaveChanges();
            // Redirect to a confirmation page or another action
            return RedirectToPage("/Account/ProfileListing");

        }

    }
}
