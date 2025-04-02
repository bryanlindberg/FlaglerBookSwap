using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlaglerBookSwap.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly ILogger<LogoutModel> logger;  

        public LogoutModel(UserManager<Users> userManager, SignInManager<Users> signInManager, ILogger<LogoutModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await signInManager.SignOutAsync();
            logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }
    }
}
