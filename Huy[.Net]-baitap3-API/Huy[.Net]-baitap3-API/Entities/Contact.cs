using System.ComponentModel.DataAnnotations;

namespace Huy_.Net__baitap3_API.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
        [Required]
        [StringLength(50)]
        public string Subject { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Message { get; set; }
        [Required]
        public DateTime DaySend { get; set; }
        public string? FilePath { get; set; }

    }
}
