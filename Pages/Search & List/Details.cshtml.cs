using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using static System.Collections.Specialized.BitVector32;
using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;




namespace FlaglerBookSwap.Pages.Search___List
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public decimal? Price { get; set; }
        [BindProperty]
        public bool? isSwapping { get; set; }
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
            Price = null;
            isSwapping = null;

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
            else if (Price < 0 || Price == null)
            {
                Message = "Please enter a valid price.";
                return Page();
            }
            else if (textbookImage == null || textbookImage.Length == 0)
            {
                Message = "Please upload an image.";
                return Page();
            }
            else if (isSwapping == null)
            {
                Message = "Please select if you're willing to trade.";
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
                price = (decimal)Price,
                is_willing_to_trade = (bool)isSwapping,
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

            // Notify users who have the textbook on their wishlist
            NotifyUserWishListItem(TextbookId).Wait();

            // Redirect to a confirmation page or another action
            TempData["ListingSuccess"] = "Your listing has been posted successfully!";
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

            listing.price = (decimal)Price;
            listing.is_willing_to_trade = (bool)isSwapping;
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

            TempData["EditSuccess"] = "Your listing has been edited successfully!";
            return RedirectToPage("/Account/ProfileListing");

        }
        private async Task NotifyUserWishListItem(short textbookId)
        {
            var textbook = await _context.Textbooks.FindAsync(textbookId);
            if (textbook == null)
            {
                // Log error or handle case where textbook doesn't exist
                return;
            }

            var wishlists = await _context.Wishlist
                .Where(w => w.textbook_id == textbookId && w.wishlist_status)
                .ToListAsync();

            foreach (var wishlist in wishlists)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == wishlist.userID);
                if (user != null && !string.IsNullOrEmpty(user.flagler_email))
                {
                    string home = Url.Page("/Index", pageHandler: null,
                values: new { area = "Identity", email = user.flagler_email }, protocol: Request.Scheme);
                    string resultMsg = $@"
            <h1>Good news!</h1>
            <p style='font-size:24px'>The item <strong>{textbook.Book_Title}</strong> is now listed on Flagler Book Swap.</p>
            <p style='font-size:20px'>Visit the website to check it out <a href='{home}'>here</a>!</p>";

                    bool emailSent = SendStudentEmail(user.flagler_email, user.FullName, resultMsg);

                    // Log the result of the email sending
                    if (emailSent)
                    {
                        // Log success
                        Console.WriteLine($"Successfully sent notification to {user.FullName} for textbook {textbook.Book_Title}");
                    }
                    else
                    {
                        // Log failure
                        Console.WriteLine($"Failed to send notification to {user.FullName} for textbook {textbook.Book_Title}");
                    }
                }
            }
        }
        public bool SendStudentEmail(string sendStudentEmail, string sendStudentName, string resultMsg)
        {
            string sendFromEmail = "flaglerbookswap@gmail.com"; //put in your email
            string sendFromName = "Flagler Book Swap";
            string sendToEmail = sendStudentEmail;
            string sendToName = sendStudentName;

            string messageSubject = "An item on your wishlist is now available!";
            string messageBody = resultMsg;

            MailAddress from = new MailAddress(sendFromEmail, sendFromName);
            MailAddress to = new MailAddress(sendToEmail, sendToName);
            MailMessage emailMessage = new MailMessage(from, to);

            emailMessage.Subject = messageSubject;
            emailMessage.Body = messageBody;
            emailMessage.IsBodyHtml = true;

            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                //client.Host = "smtp.office365.com";
                //client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("flaglerbookswap@gmail.com", "jbsk jggt jcqs teeg\r\n");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(emailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
