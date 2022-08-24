using Huy_.NET__baitap2_codeFirst.Data;
using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Huy_.NET__baitap2_codeFirst.Service
{
    public class DepartmentServiceImp : DepartmentService
    {
        private SchoolContext db;
        public DepartmentServiceImp(SchoolContext schoolContext)
        {
            db = schoolContext;
        }

        public void Create(DepartmentInfo departmentInfo)
        {
            Department department = new Department()
            {
                DepartmentID = departmentInfo.DepartmentID,
                Budget = departmentInfo.Budget,
                Name = departmentInfo.Name,
                Administrator = departmentInfo.Administrator,
                InstructorID = departmentInfo.InstructorID,
                StartDate = departmentInfo.StartDate,
                RowVersion = departmentInfo.RowVersion
            };
            db.Departments.Add(department);
            
        }

        public IQueryable<DepartmentInfo> FindAll()
        {
           var departmentInfo = db.Departments.Select(d => new DepartmentInfo
            {
                DepartmentID = d.DepartmentID,
                Name = d.Name,
                Budget = d.Budget,
                StartDate = d.StartDate,
                Administrator = d.Administrator
            });
            return  departmentInfo;

        }

        public async Task<DepartmentInfo> FindDpm(int id)
        {
            return await db.Departments.Include(i => i.Administrator)
                 .Select(d => new DepartmentInfo
                 {
                     DepartmentID = d.DepartmentID,
                     Budget = d.Budget,
                     Name = d.Name,
                     Administrator = d.Administrator,
                     InstructorID = d.InstructorID,
                     StartDate = d.StartDate,
                     RowVersion = d.RowVersion
                     
                 }).AsNoTracking().FirstOrDefaultAsync(d => d.DepartmentID == id);
        }
    }
}
