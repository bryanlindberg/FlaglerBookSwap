using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Wishlist
    {
        [Key]
        public short WishlistID { get; set; }
        public DateTime date_wishlisted { get; set; }
        public bool wishlist_status { get; set; }
        public short userID { get; set; }
        public short textbook_id { get; set; }
        //public string image { get; set; } NEED TO ADD THIS TO DB
    }
}
