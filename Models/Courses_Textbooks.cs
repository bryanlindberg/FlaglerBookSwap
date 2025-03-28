using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Courses_Textbooks
    {
        [Key]
        public byte unique_id { get; set; }
        public short course_id  { get; set; }
        public short textbook_id { get; set; }
    }
}
