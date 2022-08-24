using Microsoft.AspNetCore.Mvc;

namespace Huy_.NET__baitap2_codeFirst.Controllers
{
    [Route("home")]
    [Route("")]
    public class HomeController : Controller
    {
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
