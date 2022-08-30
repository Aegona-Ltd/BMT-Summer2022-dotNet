using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Country { get; set; }
        [StringLength(250, MinimumLength = 3)]
        [Required]
        public string Subject { get; set; }
    }
}
