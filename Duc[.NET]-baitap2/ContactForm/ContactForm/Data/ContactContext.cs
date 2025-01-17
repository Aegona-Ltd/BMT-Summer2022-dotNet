﻿using ContactForm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactForm.Data
{
    public class ContactContext: DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }

      
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Contact>().ToTable("Contact");
        }
    }
}
