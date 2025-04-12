using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlaglerBookSwap.Models
{
    public class Listings
    {
        [Key]
        public short ListingID { get; set; }
        public DateTime date_listed { get; set; }
        public string condition { get; set; }
        public string edition { get; set; } 
        public decimal price { get; set; }
        public byte[]? photo { get; set; }
        public bool list_status { get; set; }
        public bool is_willing_to_trade { get; set; } 
        public short userID { get; set; }
        public string? contact_preference { get; set; } 
        public short textbook_id { get; set; }
        [ForeignKey("textbook_id")]
        public Textbooks Textbooks { get; set; } // Navigation property to the Textbooks table
        [ForeignKey("userID")]
        public Users Users { get; set; } // Navigation property to the Users table

    }
}
