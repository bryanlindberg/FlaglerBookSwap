using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Security.Claims;
using static System.Collections.Specialized.BitVector32;

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
        [BindProperty(SupportsGet = true)]
        public int ListingId { get; set; }
        [BindProperty]
        public string Message { get; set; } = string.Empty;
        public IFormFile textbookImage { get; set; }


        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }




        public async Task<IActionResult> OnGetAsync(short textbookId, short listingId)
        {

            TextbookId = textbookId;
            ListingId = listingId;

            var listing = await _context.Listings.FindAsync(listingId);

            if (listing != null)
            {
                Price = listing.price;
                textbookEdition = listing.edition;
                isSwapping = listing.is_willing_to_trade;
                contactPref = listing.contact_preference;
                textbookCondition = listing.condition;
            }
            return Page();
        }

        private short GetNextAvailableListingId()
        {
            short maxId = (short)(_context.Listings.Any() ? _context.Listings.Max(l => l.ListingID) : (short)0);
            return (short)(maxId + 1);
        }



        public IActionResult OnPost()
        {

            //input validation
            if (string.IsNullOrWhiteSpace(textbookCondition))
            {
                Message = "Please select a condition.";
                return Page();
            }
            else if (string.IsNullOrWhiteSpace(textbookEdition))
            {
                Message = "Please enter an edition.";
                return Page();
            }
            else if (string.IsNullOrWhiteSpace(contactPref))
            {
                Message = "Please select a contact preference.";
                return Page();
            }
            else if (Price < 0)
            {
                Message = "Please enter a valid price.";
                return Page();
            }
            else if (textbookImage == null || textbookImage.Length == 0)
            {
                Message = "Please upload an image.";
                return Page();
            }


            // Retrieve the logged-in user's ID
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user is not logged in, handle accordingly (e.g., redirect to login page)
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login"); // Redirect to login if not logged in
            }

            short userId = short.Parse(userIdString);

            byte[] imageBytes = null;

            // Check if the image file is not null and has content
            if (textbookImage != null && textbookImage.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                textbookImage.CopyTo(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            var newTextbookListing = new Listings
            {
                ListingID = GetNextAvailableListingId(),
                date_listed = DateTime.Now,
                price = Price,
                is_willing_to_trade = isSwapping,
                list_status = true,
                condition = textbookCondition,
                edition = textbookEdition,
                photo = imageBytes,
                textbook_id = TextbookId,
                userID = userId,
                contact_preference = contactPref,
            };

            // Save the new textbook listing to the database or perform any other necessary actions
            _context.Listings.Add(newTextbookListing);
            _context.SaveChanges();
            // Redirect to a confirmation page or another action
            return RedirectToPage("/Account/ProfileListing");

        }

        public IActionResult OnPostEdit()
        {
            // Retrieve the logged-in user's ID
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user is not logged in, handle accordingly (e.g., redirect to login page)
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login"); // Redirect to login if not logged in
            }

            short userId = short.Parse(userIdString);

            byte[] imageBytes = null;

            // Check if the image file is not null and has content
            if (textbookImage != null && textbookImage.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                textbookImage.CopyTo(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            // Assuming textbookId is passed as a parameter or bound property
            var listing = _context.Listings.FirstOrDefault(l => l.ListingID == ListingId && l.userID == userId);

            if (listing == null)
            {
                return NotFound();
            }

            listing.price = Price;
            listing.is_willing_to_trade = isSwapping;
            listing.condition = textbookCondition;
            listing.edition = textbookEdition;
            listing.contact_preference = contactPref;

            if (textbookImage != null && textbookImage.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                textbookImage.CopyTo(memoryStream);
                listing.photo = memoryStream.ToArray();
            }

            // Save changes
            _context.SaveChanges();
            return RedirectToPage("/Account/ProfileListing");

        }

    }
}
