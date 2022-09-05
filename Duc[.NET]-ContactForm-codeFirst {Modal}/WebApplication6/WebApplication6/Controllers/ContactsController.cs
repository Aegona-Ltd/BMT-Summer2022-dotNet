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

        // GET: Contacts/AddOrEdit
        public async  Task<IActionResult> AddOrEdit(int id = 0)
        {
             if(id == 0)
            {
                return View(new Contact());
            }
            else
            {
                var contact = await _context.Contacts.FindAsync(id);
                if (contact == null)
                {
                    return NotFound();
                }
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




      

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ID,FirstName,LastName,Country,Subject")] Contact contact)
        {
          

            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                }
                else
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
                }
              
                return Json(new {isValid = true,html = Helper.RenderRazorViewToString(this, "Index", _context.Contacts.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", contact) });
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
            return Json(new {  html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Contacts.ToList()) });
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
    }
}
