using Demo_baitap1_.NetMVC.Models;

namespace Demo_baitap1_.NetMVC.Services
{
    public interface AccountService
    {
        public Account Login(string email, string password);
        public List<Account> FindAll();
    }
}
