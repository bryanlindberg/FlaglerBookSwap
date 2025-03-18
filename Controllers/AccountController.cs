using Microsoft.AspNetCore.Mvc;
using FlaglerBookSwap.ViewModels;

namespace FlaglerBookSwap.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
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
