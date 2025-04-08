using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Users
    {
        public string flagler_email { get; set; }
        public string password { get; set; }
        public string major { get; set; }
        public string expected_grad_year { get; set; }
        public string phone_number { get; set; }
        public string date_created { get; set; }
       // public string birth_year { get; set; }

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
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string? gender { get; set; }
        public string phone_number { get; set; }
        public DateTime? date_created { get; set; }
        public string? birth_year { get; set; } // will be a required field later but i set it to optional for testing purposes for now
    }
}
