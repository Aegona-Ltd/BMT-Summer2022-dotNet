using Jwtauthentication6._0.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jwtauthentication6._0.Data
{
    public class JWTDbContext : IdentityDbContext<ApplicationUser>
    {
        public JWTDbContext(DbContextOptions<JWTDbContext> options) : base(options)
        {
        }
    }
}
