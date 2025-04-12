using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlaglerBookSwap.Models
{
    public class Courses_Textbooks
    {
        [Key]
        public byte unique_id { get; set; }
        public short course_id { get; set; }
        public short textbook_id { get; set; }
        [ForeignKey("course_id")]
        public Courses Courses { get; set; } // Navigation property to the Courses table
    }
}
