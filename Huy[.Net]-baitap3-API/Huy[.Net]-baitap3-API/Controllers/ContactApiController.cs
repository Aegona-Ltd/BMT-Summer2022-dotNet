using Huy_.Net__baitap3_API.Helpers;
using Huy_.Net__baitap3_API.Models;
using Huy_.Net__baitap3_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Huy_.Net__baitap3_API.Entities;
using OfficeOpenXml;
using System.Data;

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
        public async Task<ActionResult> GetContactListUsingRedisCache()
        {
            try
            {
                var cacheKey = "contactList";
                string serializedContactList;
                var contactList = new List<ContactListInfo>();
                var redisContactList = await distributedCache.GetAsync(cacheKey);
                if (redisContactList != null)
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
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [Authorize]
        [HttpGet("findall/{pageNumber}")]
        [Produces("application/json")]
        public async Task<IActionResult> FindAll(int? pageNumber, int pageSize = 3)
        {

            try
            {

                //var cacheKey = "contactList";
                var contacts = contactService.FindAll();
                var totalPage = (int)Math.Ceiling(contacts.Count() / (double)pageSize);
                var data = PaginatedList<ContactListInfo>.Create(contacts, pageNumber ?? 1, pageSize);
                return Json(new
                {
                    data = data,
                    totalPage = totalPage,
                    pageNum = pageNumber
                });
                //await GetContactListUsingRedisCache();
                //var contactRedis = await distributedCache.GetStringAsync(cacheKey);
                //var contactsRedis = JsonConvert.DeserializeObject<List<ContactListInfo>>(contactRedis).AsQueryable();
                //var totalPageRedis = (int)Math.Ceiling(contactsRedis.Count() / (double)pageSize);
                //var dataRedis = PaginatedList<ContactListInfo>.Create(contactsRedis, pageNumber ?? 1, pageSize);
                //return Json(new
                //{
                //    data = dataRedis,
                //    totalPage = totalPageRedis,
                //    pageNum = pageNumber
                //});

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
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleteC/{id}")]
        public IActionResult DeleteContact(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                contactService.DeleteContact(id);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut("update/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Update([FromForm] ContactFormInfo contactFormInfo)
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
                        contactService.Update(contactFormInfo);
                        var status = false;
                    }
                    return Json(new
                    {
                        status = true,
                    });

                }
                else
                {

                    return Json(new
                    {
                        status = false,
                    });

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        //Cach 1

        //[HttpGet("ExportToExcel")]
        //public async Task<IActionResult> DownLoadExcelFile()
        //{
        //    try
        //    {
        //        //
        //        var contactList = contactService.FindList();
        //        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        string fileName = "contactlist.xlsx";
        //        using (var workbook = new XLWorkbook())
        //        {
        //            IXLWorksheet worksheet =
        //            workbook.Worksheets.Add("ContactList");
        //            worksheet.Cell(1, 1).Value = "Id";
        //            worksheet.Cell(1, 2).Value = "FullName";
        //            worksheet.Cell(1, 3).Value = "Email";
        //            worksheet.Cell(1, 4).Value = "Phone";
        //            worksheet.Cell(1, 5).Value = "Subject";
        //            worksheet.Cell(1, 6).Value = "Message";
        //            worksheet.Cell(1, 7).Value = "DaySend";
        //            worksheet.Cell(1, 8).Value = "FilePath";
        //            for (int index = 1; index <= contactList.Count; index++)
        //            {
        //                worksheet.Cell(index + 1, 1).Value = contactList[index - 1].Id;
        //                worksheet.Cell(index + 1, 2).Value = contactList[index - 1].FullName;
        //                worksheet.Cell(index + 1, 3).Value = contactList[index - 1].Email;
        //                worksheet.Cell(index + 1, 4).Value = contactList[index - 1].Phone;
        //                worksheet.Cell(index + 1, 5).Value = contactList[index - 1].Subject;
        //                worksheet.Cell(index + 1, 6).Value = contactList[index - 1].Message;
        //                worksheet.Cell(index + 1, 7).Value = contactList[index - 1].DaySend;
        //                worksheet.Cell(index + 1, 8).Value = contactList[index - 1].FilePath;
        //            }
        //            //required using System.IO;
        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                stream.Position = 0;
        //                var content = stream.ToArray();
        //                return File(content, contentType, fileName);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}


        //cach 2

        //[HttpGet("ExporttoExcel")]
        //public ActionResult ExportRecordtoExcel()
        //{
        //    //add test data
        //    var obj = contactService.FindList();
        //    //using System.Text;
        //    StringBuilder str = new StringBuilder();
        //    str.Append("<table border=`" + "1px" + "`b>");
        //    str.Append("<tr>");
        //    str.Append("<td><b><font face=Arial Narrow size=3>Id</font></b></td>");
        //    str.Append("<td><b><font face=Arial Narrow size=3>FullName</font></b></td>");
        //    str.Append("<td><b><font face=Arial Narrow size=3>Email</font></b></td>");
        //    str.Append("<td><b><font face=Arial Narrow size=3>Phone</font></b></td>");
        //    str.Append("<td><b><font face=Arial Narrow size=3>Subject</font></b></td>");
        //    str.Append("<td><b><font face=Arial Narrow size=3>Message</font></b></td>");
        //    str.Append("<td><b><font face=Arial Narrow size=3>DaySend</font></b></td>");
        //    str.Append("<td><b><font face=Arial Narrow size=3>FilePath</font></b></td>");
        //    str.Append("</tr>");
        //    foreach (var contact in obj)
        //    {
        //        str.Append("<tr>");
        //        str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + contact.Id.ToString() + "</font></td>");
        //        str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + contact.FullName.ToString() + "</font></td>");
        //        str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + contact.Email.ToString() + "</font></td>");
        //        str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + contact.Phone.ToString() + "</font></td>");
        //        str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + contact.Subject.ToString() + "</font></td>");
        //        str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + contact.Message.ToString() + "</font></td>");
        //        str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + contact.DaySend.ToString() + "</font></td>");
        //        str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + contact.FilePath?.ToString() + "</font></td>");
        //        str.Append("</tr>");
        //    }
        //    str.Append("</table>");
        //    HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=Information" + DateTime.Now.Year.ToString() + ".xlsx");
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
        //    return File(temp, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //}

        //application/vnd.ms-excel


        //cach 3


        [HttpGet("ExportToExcel")]
        public IActionResult ExportToExcel()
        {
            var obj = contactService.FindList();
            //using System.Data;
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] {
                new DataColumn("Id"),
                new DataColumn("FullName"),
                new DataColumn("Email"),
                new DataColumn("Phone"),
                new DataColumn("Subject"),
                new DataColumn("Message"),
                new DataColumn("DaySend"),
                new DataColumn("FilePath")
            });

            foreach (var con in obj)
            {
                dt.Rows.Add(con.Id, con.FullName, con.Email, con.Phone, con.Subject, con.Message, con.DaySend, con.FilePath);
            }
            //using ClosedXML.Excel;
            using (XLWorkbook wb = new XLWorkbook(XLEventTracking.Disabled))
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "contactlist.xlsx");
                }
            }
        }
    }
}
