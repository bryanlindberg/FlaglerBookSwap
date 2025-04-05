using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using FlaglerBookSwap.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;


namespace FlaglerBookSwap.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly AppDbContext _context;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(AppDbContext context, ILogger<LoginModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public LoginViewModel LoginView { get; set; }

        public class LoginViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Flagler Email")]
            public string flagler_email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
        }
       
        public async Task<IActionResult> OnGetAsync()
        {
            // If the user is already signed in, redirect to home page
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Find user by email
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.flagler_email == LoginView.flagler_email);

                if (user == null)
                {
                    _logger.LogWarning("User not found.");
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                // Verify password
               bool isPasswordValid = VerifyPassword(LoginView.Password, user.password);

                if (isPasswordValid)
                {
                    _logger.LogInformation("User logged in.");

                    // Create claims for the authenticated user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Email, user.flagler_email)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = LoginView.RememberMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToPage("/Index");
                }
                else
                {
                    _logger.LogWarning("Password is incorrect.");
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return Page();
        }

        // Verify password by hashing the input and comparing to stored hash
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            // If you haven't implemented hashing yet, use this simple comparison
            // WARNING: This is only for development and should be replaced with proper hashing
            return inputPassword == storedPasswordHash;
        }


    }
}
