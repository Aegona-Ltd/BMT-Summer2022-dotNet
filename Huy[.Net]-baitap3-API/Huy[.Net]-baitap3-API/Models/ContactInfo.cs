using System.ComponentModel.DataAnnotations;

namespace Huy_.Net__baitap3_API.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        [Required]
        public DateTime DaySend { get; set; }
        public string? FilePath { get; set; }

    }
}
