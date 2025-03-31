using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class Courses
    {
        [Key]
        public short course_id { get; set; }
        public string term { get; set; }
        public string course_code { get; set; }
        public string section { get; set; }
        public string course_title { get; set; }
    }
}
