using System.ComponentModel.DataAnnotations;

namespace FlaglerBookSwap.Models
{
    public class ContactForms
    {
        [Key]
        public int FormID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public short UserID { get; set; }
    }
}
