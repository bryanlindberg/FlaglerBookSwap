using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Users : IdentityUser
    {
    
        public string FullName { get; set; }      

    }
}
