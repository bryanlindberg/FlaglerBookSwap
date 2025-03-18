using Microsoft.AspNetCore.Mvc;
using FlaglerBookSwap.ViewModels;
using FlaglerBookSwap.Views.Account;

namespace FlaglerBookSwap.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle login logic here
            }
            return View(model);
        }
    }
}
