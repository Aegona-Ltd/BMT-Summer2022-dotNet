using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Models.Entities
{
    public partial class ASPcore5_DBContext : DbContext
    {
        public ASPcore5_DBContext()
        {
        }

        public ASPcore5_DBContext(DbContextOptions<ASPcore5_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

     //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //       if (!optionsBuilder.IsConfigured)
  //          {
 //  #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
         //       optionsBuilder.UseSqlServer("Data Source=DESKTOP-16619D9;Initial Catalog=ASPcore5_DB;Persist Security Info=True;User ID=sa;Password=toiyeuanime1996");
     //      }
    //    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
