using System.ComponentModel.DataAnnotations;

namespace Demo_baitap1_.NetMVC.Models
{
    public class Account
    {
        public int Id { get; set; } 
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(250)]
        public string Password { get; set; }
    }
}
