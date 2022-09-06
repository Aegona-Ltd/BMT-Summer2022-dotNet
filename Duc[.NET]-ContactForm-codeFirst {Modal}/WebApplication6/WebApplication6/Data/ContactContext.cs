using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Data
{
    public class ContactContext: DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }


        public DbSet<Contact> Contacts { get; set; }
    }
}
