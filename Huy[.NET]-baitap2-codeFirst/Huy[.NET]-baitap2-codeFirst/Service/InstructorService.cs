using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;

namespace Huy_.NET__baitap2_codeFirst.Service
{
    public interface InstructorService
    {
        public IQueryable<InstructorInfo> FindAll();
        public IQueryable<CourseInfo> FindAllC();
        public IEnumerable<EnrollmentInfo> FindAllE(int courseID);
        public InstructorInfo FindById(int? id);
        public void Update(InstructorInfo instructorInfo);
        public void Create(InstructorInfo instructorInfo);
        public void Delete(int id);

    }
}
