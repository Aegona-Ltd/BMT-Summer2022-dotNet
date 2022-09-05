using Huy_.Net__baitap3_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Huy_.Net__baitap3_API.Data
{
    public class WebContext : DbContext
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
