using ContactForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactForm.Data
{
    public class DbInitializer
    {
        public static void Initialize(ContactContext context)
        {
            context.Database.EnsureCreated();
            if (context.Contacts.Any())
            {
                return;   // DB has been seeded
            }

            var contacts = new Contact[]
          {
            new Contact{FirstName="Duc",LastName="Nguyen",Country="VietNam",Subject="Good"},
            new Contact{FirstName="Minh",LastName="Nguyen",Country="Laos",Subject="Bad"},
             new Contact{FirstName="Kien",LastName="Nguyen",Country="USA",Subject="Huhu"},
             
          };
            foreach (Contact s in contacts)
            {
                context.Contacts.Add(s);
            }
            context.SaveChanges();
        }
    }
}
