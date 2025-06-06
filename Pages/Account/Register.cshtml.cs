using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FlaglerBookSwap.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(AppDbContext context, ILogger<RegisterModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            // set above code to show error messages if the model state is invalid

            if (await _context.Users.AnyAsync(u => u.flagler_email == RegisterViewModel.flagler_email))
            {
                ModelState.AddModelError("RegisterViewModel.Email", "Email is already registered.");
                return Page();
            }

            var user = new Users
            {
                UserID = GetNextAvailableId(),
                first_name = RegisterViewModel.Name.Split(' ')[0],
                last_name = RegisterViewModel.Name.Contains(" ") ?
                RegisterViewModel.Name.Substring(RegisterViewModel.Name.IndexOf(' ') + 1) :
                string.Empty,
                flagler_email = RegisterViewModel.flagler_email,
                date_created = DateTime.Now,
                birth_year = RegisterViewModel.BirthYear.ToString()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Your account has been created successfully. Please go to your email to making your profile!.";

            _logger.LogInformation("User created a new account.");

            //code to send email to user
            string createProfileLink = Url.Page("/Account/CreateProfile", pageHandler: null, 
                values: new { area = "Identity", email = user.flagler_email }, protocol: Request.Scheme);
            string resultMsg = $@"                  
                     <h1 style='color: #A3282F;'> Welcome to Flagler Textbook Swap! </h1>
                     <p style='font-size:24px'>Your account has been created successfully. We're thrilled to have you on board!</p>
                      <p><a href='{createProfileLink}' style='display: inline-block; background-color: #A3282F; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Create My Profile</a></p>
                     <p>Start swapping, saving, and connecting with other Flagler Saints! </p>";

            bool emailSent = SendStudentEmail(user.flagler_email, user.FullName, resultMsg);

            if (emailSent)
            {
                _logger.LogInformation("Email sent successfully.");
            }
            else
            {
                _logger.LogInformation("Email failed to send.");
            }

            TempData["SuccessMessage"] = "Your account has been created successfully. Please go to your email to complete your account creation";

            return RedirectToPage("/Account/Login"); // should bring the user to the login page but it doesn't and i need to                 

        }
        private short GetNextAvailableId()
        {
            short maxId = (short)(_context.Users.Any() ? _context.Users.Max(u => u.UserID) : (short)0);
            return (short)(maxId + 1);
        }
        //put code for hashing from the video here if you want for later implementation


        public bool SendStudentEmail(string sendStudentEmail, string sendStudentName, string resultMsg)
        {
            string sendFromEmail = "flaglerbookswap@gmail.com"; //put in your email
            string sendFromName = "Flagler Textbook Swap";
            string sendToEmail = sendStudentEmail;
            string sendToName = sendStudentName;

            string messageSubject = "Welcome to the textbook swap community!";
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
                _logger.LogError($"An error occurred while sending email: {ex.Message}");
                return false;
            }
        }
    }
}

