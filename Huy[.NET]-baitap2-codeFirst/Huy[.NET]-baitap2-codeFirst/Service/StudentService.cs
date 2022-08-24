using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;

namespace Huy_.NET__baitap2_codeFirst.Service
{
    public interface StudentService
    {
        public void Create(StudentInfo studentInfo);
        public void Delete(int id);
        public IQueryable<StudentInfo> FindAll();
        public Student FindByID(int id);
        public StudentInfo FindID(int id);
        public void Update(StudentInfo studentInfo);
        public IQueryable<Student> Search(string keyword);


    }
}
