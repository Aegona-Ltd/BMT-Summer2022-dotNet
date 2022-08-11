using Demo_baitap1_.NetMVC.Models;
using Demo_baitap1_.NetMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo_baitap1_.NetMVC.Controllers
{
    [Route("contact")]
    [Route("")]
    public class ContactController : Controller
    {
        private ContactService contactService;
        public ContactController(ContactService _contactService)
        {
            contactService = _contactService;
        }

        [Route("addcontactform")]
        [Route("")]
        public IActionResult ContactForm()
        {
            return View("ContactForm", new Contact());
        }
        [HttpPost]
        [Route("addcontactform")]
        public IActionResult ContactForm(Contact contact)
        {
            contactService.CreateContact(contact);
            return View();
        }
    }
}
