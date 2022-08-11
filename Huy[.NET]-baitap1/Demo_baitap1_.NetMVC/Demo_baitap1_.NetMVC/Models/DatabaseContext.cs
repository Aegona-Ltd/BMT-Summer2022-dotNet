using Microsoft.EntityFrameworkCore;

namespace Demo_baitap1_.NetMVC.Models
{
    public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public DatabaseContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectString = configuration.GetConnectionString("WebApiDatabase");
            optionsBuilder.UseMySql(connectString, ServerVersion.AutoDetect(connectString));
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Account> account { get; set; }
        public DbSet<Contact> contact { get; set; }
    }
}
