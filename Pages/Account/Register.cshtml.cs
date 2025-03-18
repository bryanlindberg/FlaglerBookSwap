using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlaglerBookSwap.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public IActionResult Register()
        {
            return RedirectToPage("/Account/Register");
        }
    }
}
