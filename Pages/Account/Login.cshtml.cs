using FlaglerBookSwap.Models;
using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace FlaglerBookSwap.Pages.Account
{
    public class LoginModel : PageModel
    {
       
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly ILogger<LoginModel> logger; //help me troubleshoot how 

        public LoginModel(UserManager<Users> userManager, SignInManager<Users> signInManager, ILogger<LoginModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(LoginViewModel.Email);
                if (user == null)
                {
                    logger.LogWarning("User not found."); //confirms if email is in database
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return Page();
                }
                var result = await signInManager.PasswordSignInAsync(LoginViewModel.Email, LoginViewModel.Password, LoginViewModel.RememberMe, false);

                if (result.Succeeded)
                {
                    logger.LogInformation("User logged in.");
                    return RedirectToPage("/Index");
                }
                else
                {
                    logger.LogWarning("Password Is incorrect.");
                    ModelState.AddModelError(string.Empty, "Password Is incorrect");
                }
            }
            return Page();
        }
    }
}
