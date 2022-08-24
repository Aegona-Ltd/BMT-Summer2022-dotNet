using Huy_.NET__baitap2_codeFirst.Data;
using Huy_.NET__baitap2_codeFirst.Models;
using Huy_.NET__baitap2_codeFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace Huy_.NET__baitap2_codeFirst.Service
{
    public class StudentServiceImp : StudentService
    {
        private SchoolContext db;
        public StudentServiceImp( SchoolContext schoolContext)
        {
            db = schoolContext;
        }

        public void Create(StudentInfo studentInfo)
        {
            Student student = new Student()
            {
                LastName = studentInfo.LastName,
                FirstMidName = studentInfo.FirstMidName,
                EnrollmentDate = studentInfo.EnrollmentDate,
            };
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(db.Students.Find(id));
            db.SaveChanges();
        }

        public IQueryable<StudentInfo> FindAll()
        {
             return db.Students.Select(s => new StudentInfo
             {
                ID = s.ID,
                LastName = s.LastName,
                FirstMidName = s.FirstMidName,
                EnrollmentDate= s.EnrollmentDate
            }).AsNoTracking();
        }

        public Student FindByID(int id)
        {
            return db.Students.Include(s=>s.Enrollments).ThenInclude(e=>e.Course).AsNoTracking().FirstOrDefault(m=>m.ID==id);
        }

        public StudentInfo FindID(int id)
        {
            Student student = db.Students.Find(id);
            StudentInfo studentInfo = new StudentInfo()
            {
                ID = student.ID,
                LastName = student.LastName,
                FirstMidName = student.FirstMidName,
                EnrollmentDate = student.EnrollmentDate
            };
            return studentInfo ;
        }
        public void Update(StudentInfo studentInfo)
        {
            Student student = new Student()
            {
                ID = studentInfo.ID,
                LastName = studentInfo.LastName,
                FirstMidName = studentInfo.FirstMidName,
                EnrollmentDate = studentInfo.EnrollmentDate
            };
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            
        }

        public IQueryable<Student> Search(string keyword)
        {
            return db.Students.Where(s=> s.LastName.Contains(keyword) || s.FirstMidName.Contains(keyword)).AsNoTracking();
        }
    }
}
