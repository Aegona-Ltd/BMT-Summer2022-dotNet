using Huy_.Net__baitap3_API.Helpers;
using Huy_.Net__baitap3_API.Models;
using Huy_.Net__baitap3_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Huy_.Net__baitap3_API.Data;
using Huy_.Net__baitap3_API.Entities;
using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;

namespace Huy_.Net__baitap3_API.Controllers
{
    [Route("contactapi")]
    public class ContactApiController : Controller
    {
        private RecaptchaResult captchaResult;
        private ContactService contactService;
        private IWebHostEnvironment webHostEnvironment;
        private IMemoryCache memoryCache;
        private IDistributedCache distributedCache;
        public ContactApiController(ContactService _contactService, IWebHostEnvironment _webHostEnvironment
            , RecaptchaResult _captchaResult, IMemoryCache _memoryCache, IDistributedCache _distributedCache)
        {
            contactService = _contactService;
            webHostEnvironment = _webHostEnvironment;
            captchaResult = _captchaResult;
            distributedCache = _distributedCache;
            memoryCache = _memoryCache;
        }
        //[HttpGet("redis")]
        //[Produces("application/json")]
        public async Task<IActionResult> GetContactListUsingRedisCache()
        {
            var cacheKey = "contactList";
            string serializedContactList;
            var contactList = new List<ContactListInfo>();
            var redisContactList = await distributedCache.GetAsync(cacheKey);
            if(redisContactList != null)
            {
                serializedContactList = Encoding.UTF8.GetString(redisContactList);
                contactList = JsonConvert.DeserializeObject<List<ContactListInfo>>(serializedContactList);
            }
            else
            {
                contactList = contactService.FindAll().ToList();
                serializedContactList = JsonConvert.SerializeObject(contactList);
                //redisContactList = Encoding.UTF8.GetBytes(serializedContactList);
                var option = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));
               
                await distributedCache.SetStringAsync(cacheKey, serializedContactList, option);
            }
            return Json(new
            {
                data = contactList
            });
        }


        [HttpGet("findall/{pageNumber}")]
        [Produces("application/json")]
        public async Task<IActionResult> FindAll(int? pageNumber, int pageSize = 3)
        {
            try
            {
                var cacheKey = "contactList";
                await GetContactListUsingRedisCache();
                var contactRedis = await distributedCache.GetStringAsync(cacheKey);
                var contacts = JsonConvert.DeserializeObject<List<ContactListInfo>>(contactRedis).AsQueryable();
                var totalPage1 = (int)Math.Ceiling(contacts.Count() / (double)pageSize);
                var data1 = PaginatedList<ContactListInfo>.Create(contacts, pageNumber ?? 1, pageSize);
                return Json(new
                {
                    data = data1,
                    totalPage = totalPage1,
                    pageNum = pageNumber
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        public static string GenerateFileName(string contentType)
        {
            var filename = Guid.NewGuid().ToString().Replace("-", "");
            var ext = contentType.Split(new char[] { '/' })[1];
            return filename + "." + ext;
        }
        [HttpPost("create")]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromForm] ContactFormInfo contactFormInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (!Request.Form.ContainsKey("g-recaptcha-response")) return BadRequest();
                    var captcha = Request.Form["g-recaptcha-response"].ToString();
                    if (!await captchaResult.IsValid(captcha)) return NotFound();
                    if (contactFormInfo.File != null)
                    {
                        var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads", GenerateFileName(contactFormInfo.File.ContentType));
                        using (var fs = new FileStream(path, FileMode.Create))
                        {
                            contactFormInfo.File.CopyTo(fs);
                        }
                        contactFormInfo.FilePath = path;
                        contactFormInfo.DaySend = DateTime.Now;
                        contactService.Create(contactFormInfo);
                        var status = false;
                    }
                    return Json(new
                    {
                        //captcha = captcha,
                        status = true,
                    });
                }
                else
                {

                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        [HttpGet("findcontact/{id}")]
        public IActionResult FindContact(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    ContactInfo contactInfo = contactService.FindbyId(id);
                    return Json(new
                    {
                        data = contactInfo
                    });
                }

                
            }catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
