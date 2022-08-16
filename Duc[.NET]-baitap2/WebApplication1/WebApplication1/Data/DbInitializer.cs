using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DbInitializer
    {
        public static void Initialize(ContactContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Contacts.Any())
            {
                return;   // DB has been seeded
            }

            var contacts = new Contact[]
            {
            new Contact{FirstName="Carson",LastName="Alexander",Country="VN",Subject="Good"},
          
            };
            foreach (Contact s in contacts)
            {
                context.Contacts.Add(s);
            }
            context.SaveChanges();
        }
        }
}
