using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactContext _context;

        public ContactsController(ContactContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public IActionResult Index(int pg = 1)
        {

            List<Contact> contacts = _context.Contacts.ToList();
            //  var list = PaginatedList<Contact>.Create(_context.Contacts.ToList(), pageNumber ?? 1, pageSize);
            int pageSize = 5;
            if (pg < 1) pg = 1;
            int recsCount = contacts.Count();
            var paper = new Page(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = contacts.Skip(recSkip).Take(paper.PageSize).ToList();
            this.ViewBag.Page = paper;
            return View(data);


        }

     

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public  IActionResult Create()
        {
         
            return View();
            
        }


     

    

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Country,Subject")] Contact contact)
        {
            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(captchaImage))
            {
                return View(contact);
            }

            var verified = await CheckCaptcha();
           
        
            if (!verified && ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(contact);
            }
           
        }

        public async Task<bool> CheckCaptcha()

        {
            var postData = new List<KeyValuePair<string, string>>();
            {
                new KeyValuePair<string, string>("secret", "6LcTArkhAAAAAD-ynaHouXUNIQ7dvl0BvQwSla6O");
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["google-recaptcha-response"]);
            };
            var client = new HttpClient();

            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return (bool)o["success"];

        }




        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Country,Subject")] Contact contact)
        {
            if (id != contact.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
    }
}
