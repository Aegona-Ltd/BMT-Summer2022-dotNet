using Huy_.NET__baitap2_codeFirst.Data;
using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Huy_.NET__baitap2_codeFirst.Service
{
    public class CourseServiceImp : CourseService
    {
        private SchoolContext db;
        public CourseServiceImp(SchoolContext schoolContext)
        {
            db = schoolContext;
        }

        public void Create(CourseInfo courseInfo)
        {
            Course course = new Course()
            {
                CourseID = courseInfo.CourseID,
                Title = courseInfo.Title,
                Credits = courseInfo.Credits,
                DepartmentID = courseInfo.DepartmentID,
                Department = courseInfo.Department
            };
            db.Courses.Add(course);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(db.Courses.Find(id));
            db.SaveChanges();
        }

        public IQueryable<CourseInfo> FindAll()
        {
            return db.Courses.Include(c => c.Department).Select(c => new CourseInfo
            {
                CourseID = c.CourseID,
                Title = c.Title,
                Credits = c.Credits,
                DepartmentID = c.DepartmentID,
                Department = c.Department
            }).AsNoTracking();
        }

        public CourseInfo FindById(int id)
        {
            Course course = db.Courses.Find(id);
            CourseInfo courseInfo = new CourseInfo()
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Credits = course.Credits,
                DepartmentID = course.DepartmentID,
                DepartmentName = db.Departments.Where(d => d.DepartmentID == course.DepartmentID).Select(d =>d.Name ).FirstOrDefault()
            };
            return courseInfo;
        }

        public void Update(CourseInfo courseInfo)
        {
            Course course = new Course()
            {
                CourseID=courseInfo.CourseID,
                Title=courseInfo.Title,
                Credits=courseInfo.Credits,
                DepartmentID = courseInfo.DepartmentID,
                Department = courseInfo.Department
            };
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
