using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class NewController : Controller
    {
        /*
        // GET: NewController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
        public IActionResult Index()

        {
            var articles = new List<Article>
            {
               new Article{Id=1,Title="Title01",Content="This is content 1",Author="Ducdaica"},
                new Article{Id=2,Title="Title02",Content="This is content 2",Author="Ducdaica"},
                 new Article{Id=3,Title="Title03",Content="This is content 3",Author="Ducdaica"},
            new Article { Id = 4, Title = "Title04", Content = "This is content 4", Author = "Ducdaica" },
        };
            // cach 1: using ViewBag
            // ViewBag.Article = articles;

            // cach 2: using ViewData
          //  ViewData["Articles"] = articles;
            // cach 3: using Model
            return View(articles);
        }
      //  public String StringOut(int id, string firstname, string lastname)
        public String StringOut(int id,Empoyee empoyee)
        {
            return ($"Say hello from DKM : My number  is {id} my full name is {empoyee.FirstName}{empoyee.LastName}");
        }

        public IActionResult StringOut2(int id, Empoyee empoyee)
        {
            var obj = new { Id = id, Empoyee = empoyee };
            return Json(obj);
        }
    }
    public class Empoyee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}
