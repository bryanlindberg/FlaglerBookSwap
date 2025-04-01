using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Listings
    {
        [Key]
        public short ListingID { get; set; }
        public DateTime date_listed { get; set; }
        public string condition { get; set; }
        //public string edition { get; set; } NEED TO ADD TO LOCAL DB
        public decimal price { get; set; }
        public string photo { get; set; }
        public bool list_status { get; set; }
        public short userID { get; set; }
        public short textbook_id { get; set; }
    }
}
