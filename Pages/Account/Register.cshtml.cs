using FlaglerBookSwap.Models;
using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FlaglerBookSwap.Pages.Account
{
    public class RegisterModel : PageModel
    {
        //private readonly SignInManager<Users> signInManager; used to store password
        private readonly UserManager<Users> userManager;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(UserManager<Users> userManager, ILogger<RegisterModel> logger)
        {
            this.userManager = userManager;
            //this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Users user = new Users
                {
                    FullName = RegisterViewModel.Name,
                    Email = RegisterViewModel.Email,
                    UserName = RegisterViewModel.Email,
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account");
                    
                    // Send email to the user
                    string createProfileLink = Url.Page("/Account/CreateProfile", pageHandler: null, values: new { area = "Identity" }, protocol: Request.Scheme);
                    string resultMsg = $@"
                            <p>Welcome to Flagler Book Swap website! Your account has been created successfully.<p>
                            <p> Please <a href='{createProfileLink}'> click here </a> to create your profile <p>";
                    //We'll add more things to the message like font, pictures, etc ...
                    bool emailSent = SendStudentEmail(user.Email, user.FullName, resultMsg);

                    if (emailSent)
                    {
                        logger.LogInformation("Email sent successfully.");
                    }
                    else
                    {
                        logger.LogWarning("Email failed to send.");
                    }

                    return RedirectToPage("/Index", new {userid = user.Id});
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Page();
        }

        public bool SendStudentEmail(string sendStudentEmail, string sendStudentName, string resultMsg)
        {
            string sendFromEmail = "flaglerbookswap@gmail.com"; //put in your email
            string sendFromName = "Flagler Book Swap";
            string sendToEmail = sendStudentEmail;
            string sendToName = sendStudentName;

            string messageSubject = "Welcome to Flagler Bookswap!";
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
                client.Credentials = new System.Net.NetworkCredential("flaglerbookswap@gmail.com", "jbsk jggt jcqs teeg\r\n"); //use an app password
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(emailMessage);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred while sending email: {ex.Message}");
                return false;
            }
        }
    }
}

