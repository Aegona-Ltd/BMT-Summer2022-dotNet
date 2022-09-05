using System.ComponentModel.DataAnnotations;

namespace Demo_baitap1_.NetMVC.Models
{
    public class Contact
    {
        public int Id { get; set; } 
        [StringLength(250)]
        public string FullName { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(250)]
        public string Phone { get; set; }
        [StringLength(250)]
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
