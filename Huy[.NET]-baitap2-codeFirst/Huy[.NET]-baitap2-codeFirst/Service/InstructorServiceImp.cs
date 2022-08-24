using Huy_.NET__baitap2_codeFirst.Data;
using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Grade = Huy_.NET__baitap2_codeFirst.Entities.Grade;

namespace Huy_.NET__baitap2_codeFirst.Service
{
    public class InstructorServiceImp : InstructorService
    {
        private SchoolContext db;
        public InstructorServiceImp(SchoolContext _db)
        {
            db = _db;
        }

        public void Create(InstructorInfo instructorInfo)
        {
            Instructor instructor = new Instructor()
            {
                LastName = instructorInfo.LastName,
                FirstMidName = instructorInfo.FirstMidName,
                HireDate = instructorInfo.HireDate,
                CourseAssignments = instructorInfo.CourseAssignments,
                OfficeAssignment = instructorInfo.OfficeAssignment
            };
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Instructor instructor = db.Instructors.Include(i => i.CourseAssignments).SingleOrDefault(i=>i.ID == id);
            var departments = db.Departments.Where(d=>d.InstructorID == id).ToList();
            departments.ForEach(d=>d.InstructorID = null);
            db.Instructors.Remove(instructor);
            db.SaveChanges();
        }

        public IQueryable<InstructorInfo> FindAll()
        {
            return db.Instructors.Include(i=>i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Enrollments)
                .ThenInclude(i => i.Student)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Department)
                .AsNoTracking()
                .Select(i=>new InstructorInfo
                {
                    ID = i.ID,
                    FirstMidName = i.FirstMidName,
                    LastName = i.LastName,
                    HireDate = i.HireDate,
                    CourseAssignments = i.CourseAssignments,
                    OfficeAssignment = i.OfficeAssignment
                }).OrderBy(i=>i.LastName);
        }
        public IQueryable<CourseInfo> FindAllC()
        {
            return db.Courses.Select(c => new CourseInfo
            {
                CourseAssignments = c.CourseAssignments,
                CourseID = c.CourseID,
                Credits = c.Credits,
                Department = c.Department,
                Enrollments = c.Enrollments,
                Title = c.Title
            });
        }

        public IEnumerable<EnrollmentInfo> FindAllE(int courseID)
        {

            return FindAllC().Where(c => c.CourseID == courseID)
                .Single()
                .Enrollments
                .Select(e => new EnrollmentInfo
                {
                    EnrollmentID = e.EnrollmentID,
                    CourseID = e.CourseID,
                    Grade = (Grade?)e.Grade,
                    StudentID = e.StudentID,
                    FullName = db.Students.Where(s => s.ID == e.StudentID).Select(s => s.FullName).FirstOrDefault()
                });
        }

        public InstructorInfo FindById(int? id)
        {
            return db.Instructors.Include(i => i.OfficeAssignment)
                                 .Include(i => i.CourseAssignments)
                                        .ThenInclude(i => i.Course)
                                    .Select(i => new InstructorInfo
                                    {
                                        ID = i.ID,
                                        LastName = i.LastName,
                                        FirstMidName = i.FirstMidName,
                                        HireDate = i.HireDate,
                                        OfficeAssignment = i.OfficeAssignment,
                                        CourseAssignments = i.CourseAssignments,
                                    })
                                    .FirstOrDefault(m=>m.ID == id); 
            
            
        }

        
        public void Update( InstructorInfo instructorInfo)
        {
            Instructor instructor = new Instructor()
            {
                ID = instructorInfo.ID,
                LastName = instructorInfo.LastName,
                FirstMidName = instructorInfo.FirstMidName,
                HireDate = instructorInfo.HireDate,
                CourseAssignments = instructorInfo.CourseAssignments,
                OfficeAssignment = instructorInfo.OfficeAssignment
            };
            db.Entry(instructor).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
