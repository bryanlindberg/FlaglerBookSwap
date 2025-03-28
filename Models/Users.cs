using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Users : IdentityUser
    {
        [Key]
        public short UserID { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string flagler_email { get; set; }
        public string password { get; set; }
        public string major { get; set; }
        public string expected_grad_year { get; set; }
        public string phone_number { get; set; }
        public string date_created { get; set; }
        //makes a full name from first and last name
        public string FullName
        {
            get { return $"{first_name} {last_name}"; }
            set
            {
                var names = value.Split(' ');
                if (names.Length > 0)
                    first_name = names[0];
                if (names.Length > 1)
                    last_name = names[1];
            }
        }
        //public string? profile_picture { get; set; } NEED TO ADD THIS TO DATABASE
        //public string? gender { get; set; } NEED TO ADD THIS TO DATABASE

    }
}
