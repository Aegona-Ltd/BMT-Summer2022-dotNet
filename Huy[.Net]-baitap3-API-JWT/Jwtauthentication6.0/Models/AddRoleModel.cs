using System.ComponentModel.DataAnnotations;

namespace Jwtauthentication6._0.Models
{
    public class AddRoleModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
