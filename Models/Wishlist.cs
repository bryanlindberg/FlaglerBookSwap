using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlaglerBookSwap.Models
{
    public class Wishlist
    {
        [Key]
        public int WishlistID { get; set; }
        public DateTime date_wishlisted { get; set; }
        public bool wishlist_status { get; set; }
        public short userID { get; set; }
        public short textbook_id { get; set; }
        [ForeignKey("textbook_id")]
        public Textbooks Textbooks { get; set; } // Navigation property to the Textbooks table

    }
}
