using Microsoft.AspNetCore.Mvc;

namespace Huy_.Net__baitap3_API.Controllers
{
    [Route("account")]
    [Route("")]
    public class AccountController : Controller
    {
        [Route("login")]
        [Route("")]
        public IActionResult Login()
        {

            return View();
        }
    }
}
