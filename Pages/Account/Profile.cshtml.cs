using FlaglerBookSwap.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace FlaglerBookSwap.Pages.Account
{
    public class ProfileModel : PageModel
    {
        public string? flagler_email { get; set; }
        public string major { get; set; }
        public string expected_grad_year { get; set; }
        public byte[]? profile_picture { get; set; }
        public string phone_number { get; set; }
        public string? birth_year { get; set; }
        public string? gender { get; set; }
        public string first_name { get; set; }


        //  public short UserID { get; internal set; }


        private readonly AppDbContext _context;
        public ProfileModel(AppDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userProfile = await _context.Users
            .FirstOrDefaultAsync(p => p.UserID == Convert.ToInt16(userId));
            if (userProfile != null)
            {
                // Set the properties for the view
                flagler_email = userProfile.flagler_email;
                major = userProfile.major;
                expected_grad_year = userProfile.expected_grad_year;
                phone_number = userProfile.phone_number;
                gender = userProfile.gender;
                profile_picture = userProfile.profile_picture;
                first_name = userProfile.first_name;
            }
        }
    }
}
