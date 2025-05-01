using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Users
    {
        public string? flagler_email { get; set; }
        public string? Password { get; set; }
        public string? major { get; set; }
        public string? expected_grad_year { get; set; }
        public byte[]? profile_picture { get; set; } // i got an error in the create profile page so changing it from byte to string fixed it



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
        [Key]
        public short UserID { get; internal set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? gender { get; set; }
        public string? phone_number { get; set; }
        public DateTime? date_created { get; set; }
        public string? birth_year { get; set; } 

        public string? second_major { get; set; }

        public string? third_major { get; set; }

        public string? fourth_major { get; set; }

    }
}
