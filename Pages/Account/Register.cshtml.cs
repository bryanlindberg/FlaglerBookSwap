using Azure.Messaging;
using FlaglerBookSwap.Models;
using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlaglerBookSwap.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        public RegisterModel(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
                    
                    return RedirectToPage("/Account/Login"); //should bring the user to the login page but it doesn't and i need to
                    //add the logic to send an email here too once i fix the registration button to work
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
    }
}
