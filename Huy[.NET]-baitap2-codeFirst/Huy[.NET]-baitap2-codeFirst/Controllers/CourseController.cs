using Huy_.NET__baitap2_codeFirst.Data;
using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;
using Huy_.NET__baitap2_codeFirst.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Huy_.NET__baitap2_codeFirst.Controllers
{
    [Route("course")]
    public class CourseController : Controller
    {
        private CourseService courseService;
        private SchoolContext db;
        public CourseController(CourseService _courseService, SchoolContext _db)
        {
            courseService = _courseService;
            db = _db;
        }


        [Route("index")]
        public IActionResult Index()
        {
            var courses = courseService.FindAll().ToList();

            return View("Index",courses);
        }

        private void DepartmenDropdowntList(object sD = null)
        {
            var departments = db.Departments.Select(d => d).OrderBy(d => d.Name).AsNoTracking();
            ViewBag.DeID = new SelectList(departments, "DepartmentID", "Name", sD);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            CourseInfo courseInfo = new CourseInfo();
            DepartmenDropdowntList();
            //courseInfo.DepartMents = db.Departments.OrderBy(d=>d.Name).Select(d => new SelectListItem
            //{
            //    Value = d.DepartmentID.ToString(),
            //    Text = d.Name
            //}).ToList();
            return View("Create", courseInfo);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create(CourseInfo courseInfo)
        {
                if (ModelState.IsValid)
                {
                    courseService.Create(courseInfo);
                    return RedirectToAction("Index");
                }
            Debug.WriteLine(courseInfo.CourseID);
            Debug.WriteLine(courseInfo.Title);
            Debug.WriteLine(courseInfo.Credits);
            Debug.WriteLine(courseInfo.DepartmentID);
            var er = ModelState.ValidationState;
            Debug.WriteLine(er);
            Debug.WriteLine(ModelState.ErrorCount);
            Debug.WriteLine(ModelState.Values);
            DepartmenDropdowntList(courseInfo.DepartmentID);
            return View("Create", courseInfo);
        }
        [Route("details")]
        public IActionResult Details(int id)
        {
            if(id== null)
            {
                return NotFound();
            }
            var courseInfo = courseService.FindById(id);
            if (courseInfo == null)
            {
                return NotFound();
            }
            return View("Details", courseInfo);
        }
        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CourseInfo courseInfo = courseService.FindById(id);
            DepartmenDropdowntList(courseInfo.DepartmentID);
            return View("Edit", courseInfo);
        }
        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(CourseInfo courseInfo)
        {
            if (ModelState.IsValid)
            {
                courseService.Update(courseInfo);
                return RedirectToAction("Index");
            }
            DepartmenDropdowntList(courseInfo.DepartmentID);
            return View("Edit", courseInfo);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult DeleteInfo(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courseInfo = courseService.FindById(id);
            if (courseInfo == null)
            {
                return NotFound();
            }
            return View("Delete", courseInfo);
        }
        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            courseService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
