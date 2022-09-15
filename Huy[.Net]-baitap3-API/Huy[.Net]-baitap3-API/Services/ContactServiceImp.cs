using Huy_.Net__baitap3_API.Data;
using Huy_.Net__baitap3_API.Entities;
using Huy_.Net__baitap3_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Web.Helpers;
using System.Net;
using ClosedXML.Excel;
using System.Diagnostics;

namespace Huy_.Net__baitap3_API.Services
{
    public class ContactServiceImp : ContactService
    {
        private WebContext _db;
        private IWebHostEnvironment webHostEnvironment;
        public ContactServiceImp(WebContext db, IWebHostEnvironment _webHostEnvironment)
        {
            _db = db;
            webHostEnvironment = _webHostEnvironment;
        }

        public IQueryable<ContactListInfo> FindAll()
        {
            return _db.Contacts.Select(c => new ContactListInfo
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                Subject = c.Subject,
                DaySend = c.DaySend
            }).AsNoTracking();
        }
        //public static string GenerateFileName(string contentType)
        //{
        //    var filename = Guid.NewGuid().ToString().Replace("-", "");
        //    var ext = contentType.Split(new char[] { '/' })[1];
        //    return filename + "." + ext;
        //}
        //public void UploadFile(IFormFile fileContact)
        //{
        //    var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads", GenerateFileName(fileContact.FileName));
        //    using(var fs = new FileStream(path, FileMode.Create))
        //    {
        //        fileContact.CopyTo(fs);
        //    }
        //}

        public void Create(ContactFormInfo contactFormInfo)
        {
            var contact = new Contact()
            {
                FullName = contactFormInfo.FullName,
                Email = contactFormInfo.Email,
                Phone = contactFormInfo.Phone,
                Subject = contactFormInfo.Subject,
                Message = contactFormInfo.Message,
                DaySend = contactFormInfo.DaySend,
                FilePath = contactFormInfo.FilePath,
            };
            _db.Contacts.Add(contact);
            _db.SaveChanges();
        }

        public ContactInfo FindbyId(int id)
        {

            Contact contact = _db.Contacts.Find(id);
            if(contact == null)
            {
                return null;
            }
            return new ContactInfo()
            {
                Id = contact.Id,
                FullName = contact.FullName,
                Email = contact.Email,
                Phone = contact.Phone,
                Subject = contact.Subject,
                Message = contact.Message,
                DaySend = contact.DaySend,
                FilePath = contact.FilePath
            };
        }

        public void DeleteContact(int id)
        {
            var contact = _db.Contacts.Where(c => c.Id == id).SingleOrDefault();
            _db.Contacts.Remove(contact);
            _db.SaveChanges();
        }

        public ContactFormInfo Update(ContactFormInfo contactFormInfo)
        {
            var updateContact = _db.Contacts.SingleOrDefault(c=>c.Id == contactFormInfo.Id);
            var oldFilePath = updateContact.FilePath;
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            updateContact.FullName = contactFormInfo.FullName;
            updateContact.Email = contactFormInfo.Email;
            updateContact.Phone = contactFormInfo.Phone;
            updateContact.Subject = contactFormInfo.Subject;
            updateContact.Message = contactFormInfo.Message;
            updateContact.DaySend = contactFormInfo.DaySend;
            updateContact.FilePath = contactFormInfo.FilePath;
            _db.Entry(updateContact).State = EntityState.Modified;
            _db.SaveChanges();
            return new ContactFormInfo()
            {
                Id = updateContact.Id,
                FullName = updateContact.FullName,
                Email = updateContact.Email,
                Phone = updateContact.Phone,
                Subject = updateContact.Subject,
                Message = updateContact.Message,
                DaySend = updateContact.DaySend,
                FilePath = updateContact.FilePath
            }; 
        }

        public List<Contact> FindList()
        {
            return _db.Contacts.ToList();
        }
    }
}
