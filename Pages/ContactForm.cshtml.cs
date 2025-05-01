using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FlaglerBookSwap.Pages
{
    public class ContactFormModel : PageModel
    {
        public int FormID { get; set; }
        [BindProperty]
        public string Subject { get; set; }
        [BindProperty]
        [StringLength(255, ErrorMessage = "Message cannot exceed 255 characters.")]
        public string Message { get; set; }

        public string Email { get; set; }
        public short UserID { get; set; }

        private readonly AppDbContext _context;

        public ContactFormModel(AppDbContext context)
        {
            _context = context;
        }



        public IActionResult OnPost()
        {

            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user is not logged in, handle accordingly (e.g., redirect to login page)
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login"); // Redirect to login if not logged in
            }

            short userId = short.Parse(userIdString);

            //fetch user email
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            if (user != null)
            {
                Email = user.flagler_email;
            }
            else
            {
                return RedirectToPage("/Account/Login");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var contactForm = new ContactForms
            {
                FormID = FormID,
                Subject = Subject,
                Message = Message,
                Email = Email,
                UserID = userId
            };
            _context.ContactForms.Add(contactForm);
            _context.SaveChanges();
            //add tempdata
            TempData["ContactMessage"] = "Your contact request form has been sent. We will review your message and contact you soon if necessary.";
            return RedirectToPage("Index");
        }

        public void OnGet()
        {
        }
    }
}
