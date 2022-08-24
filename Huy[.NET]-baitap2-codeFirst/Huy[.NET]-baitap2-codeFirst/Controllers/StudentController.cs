using Huy_.NET__baitap2_codeFirst.Models;
using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Helpers;
using Huy_.NET__baitap2_codeFirst.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Huy_.NET__baitap2_codeFirst.Data;

namespace Huy_.NET__baitap2_codeFirst.Controllers
{
    [Route("student")]
    //[Route("")]
    public class StudentController : Controller
    {
        private SchoolContext db;
        private StudentService studentService;
        public StudentController(StudentService _studentService, SchoolContext _db)
        {
            studentService = _studentService;
            db= _db;
        }

        [Route("index")]
        //[Route("")]
        public IActionResult Index(string sortOrder, string keyword, string CurrentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (keyword != null)
            {
                pageNumber = 1;
            }
            else
            {
                keyword = CurrentFilter;
            }

            ViewData["CurrentFilter"] = keyword;
            IQueryable<StudentInfo> students = studentService.FindAll();
            
            if (!String.IsNullOrEmpty(keyword))
            {
                students = students.Where(s => s.LastName.Contains(keyword)
                               || s.FirstMidName.Contains(keyword));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students = students.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            return View("Index", PaginatedList<StudentInfo>.Create(students, pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            var student = new Student();
            return View("Create", student);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create(StudentInfo studentInfo)
        {


            if (ModelState.IsValid)
            {
                studentService.Create(studentInfo);
                return RedirectToAction("Index");
            }
            var error = ModelState.ValidationState;
            Debug.WriteLine(error);
            Debug.WriteLine(false);
            return View("Create");

        }
        [Route("details")]
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.student = studentService.FindByID(id);
            if (ViewBag.student == null)
            {
                return NotFound();
            }
            return View("Details");
        }
        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            StudentInfo student = studentService.FindID(id);
            ViewBag.student = student;
            return View("Edit", student);
        }
        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(StudentInfo studentInfo)
        {
            try
                {
                    if (ModelState.IsValid)
                    {
                        studentService.Update(studentInfo);
                        return RedirectToAction("Index");
                    } 

                    return View("Edit", studentInfo);
                }
            catch (Exception ex)
                {
                Debug.WriteLine(ex.Message);
            }

            var error = ModelState.ValidationState;
            Debug.WriteLine(error);
            Debug.WriteLine(false);
            return View("Edit");

        }
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            studentService.Delete(id);
            return RedirectToAction("Index");
        }
        
    }
}
