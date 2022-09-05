using Demo_baitap1_.NetMVC.Models;

namespace Demo_baitap1_.NetMVC.Services
{
    public interface ContactService
    {
        public List<Contact> FindAllC();
        public void CreateContact(Contact contact);
        public Contact FindById(int id);
    }
}
