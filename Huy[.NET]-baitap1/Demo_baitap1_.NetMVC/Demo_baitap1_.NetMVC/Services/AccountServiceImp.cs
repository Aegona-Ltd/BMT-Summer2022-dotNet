using Demo_baitap1_.NetMVC.Models;

namespace Demo_baitap1_.NetMVC.Services
{
    public class AccountServiceImp : AccountService
    {
        DatabaseContext db;
        public AccountServiceImp(DatabaseContext _db)
        {
            db = _db;
        }

        public List<Account> FindAll()
        {
            return db.account.ToList();
        }

        public Account Login(string email, string password)
        {
            return db.account.SingleOrDefault(a => a.Email.Equals(email) && a.Password.Equals(password));
        }
    }
}
