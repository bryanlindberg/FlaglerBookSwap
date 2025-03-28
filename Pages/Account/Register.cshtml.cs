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
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
            if(ModelState.IsValid) 
            {
                Users user = new Users
                {
                    FullName = RegisterViewModel.Name,
                    Email = RegisterViewModel.Email,
                    UserName = RegisterViewModel.Email,                 
                };

                var result = await userManager.CreateAsync(user, RegisterViewModel.Password);

                if(result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");
                    await signInManager.SignInAsync(user, isPersistent: false);

                    //code to send email to user
                    string resultMsg = "Welcome to Flagler Book Swap website! Your account has been created successfully.";

                    bool emailSent = SendStudentEmail(user.Email, user.FullName, resultMsg);

                    if (emailSent)
                    {
                        logger.LogInformation("Email sent successfully.");
                    }
                    else
                    {
                        logger.LogInformation("Email failed to send.");
                    }                  
                    return RedirectToPage("/Account/Login"); //should bring the user to the login page but it doesn't and i need to                 
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
            string sendFromEmail = "reuelcis33@gmail.com"; //put in my email
            string sendFromName = "Mortgage Calculator";
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
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("***@gmail.com", "****"); //put in app code
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
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

