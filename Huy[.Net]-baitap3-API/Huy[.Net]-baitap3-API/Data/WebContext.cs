using Huy_.Net__baitap3_API.Entities;
using Huy_.Net__baitap3_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Huy_.Net__baitap3_API.Data
{
    public class WebContext : IdentityDbContext<ApplicationUser>
    {
        public WebContext(DbContextOptions<WebContext> options): base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contact>().ToTable("Contact");
        }
    }
}
