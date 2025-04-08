using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Textbooks
    {
        [Key]
        public short textbook_id { get; set; }
        public string? Book_Title { get; set; }
        public string? Authors { get; set; }
        public double ISBN { get; set; }
        public string? Image { get; set; }
        public short? Publication_Year { get; set; }
        public string? Edition { get; set; }
        public string? Publisher { get; set; }

    }
}
