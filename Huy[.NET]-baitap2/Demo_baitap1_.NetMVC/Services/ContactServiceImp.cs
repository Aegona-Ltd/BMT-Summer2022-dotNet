﻿using Demo_baitap1_.NetMVC.Models;

namespace Demo_baitap1_.NetMVC.Services
{
    public class ContactServiceImp : ContactService
    {
        DatabaseContext db;
        public ContactServiceImp(DatabaseContext _db)
        {
            db = _db;
        }
        public void CreateContact(Contact con)
        {
            db.contact.Add(con);
            db.SaveChanges();
        }

        public List<Contact> FindAllC()
        {
            return db.contact.ToList();
        }

        public Contact FindById(int id)
        {
            return db.contact.Find(id);
        }
    }
}
