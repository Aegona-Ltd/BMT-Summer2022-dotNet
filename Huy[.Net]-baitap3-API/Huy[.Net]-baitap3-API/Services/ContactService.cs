using Huy_.Net__baitap3_API.Entities;
using Huy_.Net__baitap3_API.Models;

namespace Huy_.Net__baitap3_API.Services
{
    public interface ContactService
    {
        public IQueryable<ContactListInfo> FindAll();
        public void Create(ContactFormInfo contactFormInfo);
        public ContactInfo FindbyId(int id);
        public void DeleteContact(int id);
        public ContactFormInfo Update(ContactFormInfo contactFormInfo);
        public List<Contact> FindList();
    }
}
