using Huy_.NET__baitap2_codeFirst.Data;
using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;
using Huy_.NET__baitap2_codeFirst.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;



namespace Huy_.NET__baitap2_codeFirst.Controllers
{
    [Route("instructor")]
    public class InstructorController : Controller
    {
        private InstructorService instructorService;
        private SchoolContext db;
        public InstructorController(InstructorService _instructorService, SchoolContext _db)
        {
            instructorService = _instructorService;
            db = _db;
        }


        [Route("index")]
        public IActionResult Index(int? id, int? courseID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.InstructorsInfo = instructorService.FindAll().ToList();

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                InstructorInfo instructorInfo = viewModel.InstructorsInfo.Where(i => i.ID == id.Value).Single();
                viewModel.CoursesInfo = instructorInfo.CourseAssignments.Select(c => new CourseInfo
                {
                    CourseID = c.CourseID,
                    Title = c.Course.Title,
                    Credits = c.Course.Credits,
                    Department = c.Course.Department
                });
            }
            if (courseID != null)
            {
                ViewData["CourseID"] = courseID.Value;
                viewModel.EnrollmentsInfo = instructorService.FindAllE(courseID.Value);
            }
            return View("Index",viewModel);
        }
        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var instructorInfo = instructorService.FindById(id);
            if(instructorInfo == null)
            {
                return NotFound();
            }
            PopulateAssignedCourseData(id,instructorInfo);
            return View("Edit", instructorInfo);
        }
        private void PopulateAssignedCourseData(int id, InstructorInfo instructorInfo)
        {
            instructorInfo = instructorService.FindById(id);
            var allCourses = db.Courses.Select(c=>new CourseInfo
            {
                CourseID=c.CourseID,
                Title = c.Title,
                Credits=c.Credits,
                CourseAssignments = c.CourseAssignments,
                DepartmentID=c.DepartmentID,
                Department= c.Department,
                Enrollments=c.Enrollments
            });
            var instructorCourses = new HashSet<int>(instructorInfo.CourseAssignments.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewData["Courses"] = viewModel;
        }
        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, string[] selectedCourses, InstructorInfo instructorToUpdate)
        {

            if (ModelState.IsValid)
            {

                if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
            {
                var o1 = db.OfficeAssignments.Find(id);
                    if(o1 == null)
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }
                    else
                    {
                        db.OfficeAssignments.Remove(o1);
                    }
               
                
                
            }
            else { 
                var o1 = db.OfficeAssignments.Find(id);
                if(o1 == null)
                {
                    OfficeAssignment o = new OfficeAssignment();
                    o.InstructorID = instructorToUpdate.ID;
                    o.Location = instructorToUpdate.OfficeAssignment.Location;
                    db.Add(o);
                }
                else {
                    db.OfficeAssignments.Remove(o1);
                    OfficeAssignment o2 = new OfficeAssignment();
                    o2.InstructorID = instructorToUpdate.ID;
                    o2.Location = instructorToUpdate.OfficeAssignment.Location;
                    db.Add(o2);
                }
            }
            UpdateInstructorCourses(id, selectedCourses, instructorToUpdate);
                instructorService.Update(instructorToUpdate);
                return RedirectToAction("Index",instructorToUpdate);
            }
            UpdateInstructorCourses(id, selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(id, instructorToUpdate);
            return View("Edit");
        }
        private void PopulateAssignedCourseDataCreate(InstructorInfo instructorInfo)
        {
            var allCourses = db.Courses.Select(c => new CourseInfo
            {
                CourseID = c.CourseID,
                Title = c.Title,
                Credits = c.Credits,
                CourseAssignments = c.CourseAssignments,
                DepartmentID = c.DepartmentID,
                Department = c.Department,
                Enrollments = c.Enrollments
            });
            var instructorCourses = new HashSet<int>(instructorInfo.CourseAssignments.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewData["Courses"] = viewModel;
        }
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            InstructorInfo instructorInfo = new InstructorInfo();
            instructorInfo.CourseAssignments = new List<CourseAssignment>();
            PopulateAssignedCourseDataCreate(instructorInfo);
            return View("Create",instructorInfo);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create(string[] selectedCourses, InstructorInfo instructorInfo)
        {
            if (String.IsNullOrWhiteSpace(instructorInfo.OfficeAssignment.Location))
            {
                instructorInfo.OfficeAssignment = null;
            }
                if (selectedCourses != null)
            {
                instructorInfo.CourseAssignments = new List<CourseAssignment>();
                foreach (var course in selectedCourses)
                {
                    var courseAdd = new CourseAssignment { InstructorID = instructorInfo.ID, CourseID = int.Parse(course) };
                    instructorInfo.CourseAssignments.Add(courseAdd);
                }
            }
            if (ModelState.IsValid)
            {
                instructorService.Create(instructorInfo);
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseDataCreate(instructorInfo);
            return View("Create");
        }
        public IActionResult Delete(int id)
        {
            instructorService.Delete(id);
            return RedirectToAction("Index");
        }


        private void UpdateInstructorCourses(int id,string[] selectedCourses, InstructorInfo instructorToUpdate)
        {
            instructorToUpdate = instructorService.FindById(id);
            if (selectedCourses == null)
            {

                instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
                return;
            }
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.CourseAssignments.Select(c => c.CourseID));
            foreach (var course in db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                      CourseAssignment c = new CourseAssignment();
                        c.CourseID = course.CourseID;
                        c.InstructorID = instructorToUpdate.ID;
                        db.Add(c);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        CourseAssignment courseRemove = instructorToUpdate.CourseAssignments
                            .FirstOrDefault(i=>i.CourseID == course.CourseID);
                        db.Remove(courseRemove);
                    }
                }
            }
        }
    }
}
