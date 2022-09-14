using Microsoft.AspNetCore.Mvc;

namespace Jwtauthentication6._0.Controllers
{
    [Route("account")]
    [Route("")]
    public class AccountController : Controller
    {
        [Route("login")]
        [Route("")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [Route("index")]
        public IActionResult Index()
        {
            return View("Index");
        }
        [Route("index2")]
        public IActionResult Index2()
        {
            return View("Index2");
        }
    }
}
