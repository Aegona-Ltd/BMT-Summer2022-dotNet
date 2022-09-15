using Microsoft.AspNetCore.Identity;

namespace Huy_.Net__baitap3_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
