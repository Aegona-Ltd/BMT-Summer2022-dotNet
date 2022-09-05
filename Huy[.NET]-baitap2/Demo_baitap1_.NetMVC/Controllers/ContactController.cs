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
        [Route("index")]
        
        public IActionResult Index()
        {
            return View();
        }
        [Route("loadData")]
        [HttpGet]
        public JsonResult loadData()
        {
            var listContact = contactService.FindAllC();
            return Json(new
            {
                data = listContact,
                status = true
            });
        }


        //[HttpGet]
        //[Route("addcontactform")]
        //public IActionResult ContactForm()
        //{
        //    return View("ContactForm", new Contact());
        //}
        //[HttpPost]
        //[Route("addcontactform")]
        //public IActionResult ContactForm(Contact contact)
        //{
        //    contact.DateTime = DateTime.Now;
        //    contactService.CreateContact(contact);
        //    return RedirectToAction("FindAllC");
        //}
        //[Route("findallc")]
        //[Route("")]
        //public IActionResult FindAllC()
        //{
        //    ViewBag.contacts = contactService.FindAllC();
        //    return View("ContactList");

        //}
        //[Route("details/{id}")]
        //public IActionResult Details(int id)
        //{
        //    ViewBag.contact = contactService.FindById(id);

        //    return View("ContactListShow");
        //}
    }
}
