using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Huy_.Net__baitap3_API.Models
{
    public class ContactFormInfo
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "FullName is not null 1")]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is not null 1")]
        [EmailAddress]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Invalid Length")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is not null 1")]
        [StringLength(10,ErrorMessage = "Phone number must be 10 ")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Subject is not null")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Invalid Length")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Message is not null")]
        [DataType(DataType.Text)]
        public string Message { get; set; }
        [Required]
        public DateTime DaySend { get; set; }
        public string? FilePath { get; set; }
        public IFormFile? File { get; set; }
        //[BindProperty(Name = "g-recaptcha-response")]
        //public string? GCaptchaResponse { get; set; }
    }
}
