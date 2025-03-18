using Microsoft.AspNetCore.Identity;

namespace FlaglerBookSwap.Models
{
    public class Users : IdentityUser
    {

        public string FullName { get; set; }
    }
}
