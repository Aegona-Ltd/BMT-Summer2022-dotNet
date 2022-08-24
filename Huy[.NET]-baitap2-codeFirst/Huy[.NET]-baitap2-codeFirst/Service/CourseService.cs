using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;

namespace Huy_.NET__baitap2_codeFirst.Service
{
    public interface CourseService
    {
        public IQueryable<CourseInfo> FindAll();
        public void Create(CourseInfo courseInfo);
        public CourseInfo FindById(int id);
        public void Update(CourseInfo courseInfo);
        public void Delete(int id);
    }
}
