using Demo_baitap1_.NetMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo_baitap1_.NetMVC.Controllers
{
    [Route("account")]
    
    public class AccountController : Controller
    {
        private AccountService accountService;

        public AccountController(AccountService _accountService)
        {
            accountService = _accountService;
        }
        [Route("login")]
        public IActionResult Login(string email, string password)
        {

            
            if(accountService.Login(email, password)!= null)
            {
                HttpContext.Session.SetString("email",email);
                return RedirectToAction("Welcome");
            }
            else
            {
                
                return View();
            }
                
            
        }
        [Route("welcome")]
        public IActionResult Welcome()
        {

            if (ViewBag.email = HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.email = HttpContext.Session.GetString("email");
            return View("Welcome");

        }
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction("Login");
        }
        [Route("findall")]
        
        public IActionResult FindAll()
        {
            ViewBag.accounts = accountService.FindAll();
            return View("FindAll");

        }
    }
}
