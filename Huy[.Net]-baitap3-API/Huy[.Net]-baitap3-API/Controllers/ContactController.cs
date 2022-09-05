﻿using Huy_.Net__baitap3_API.Services;
using Microsoft.AspNetCore.Mvc;
using Huy_.Net__baitap3_API.Helpers;
using Huy_.Net__baitap3_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Huy_.Net__baitap3_API.Controllers
{
    [Route("contact")]
    [Route("")]
    public class ContactController : Controller
    {
       
        [HttpGet]
        [Route("contactform")]
        [Route("")]
        public async Task<IActionResult> ContactForm()
        {
            
                    return View("ContactForm");
        }
        [HttpPost]
        [Route("contactform")]
        public async Task<IActionResult> ContactForm1()
        {
            return View("ContactForm");
        }
        [Route("contactlist")]
        public async Task<IActionResult> ContactList()
        {
            return View("ContactList");
        }

    }
}
