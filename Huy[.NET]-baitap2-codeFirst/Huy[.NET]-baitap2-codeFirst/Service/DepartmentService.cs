using Huy_.NET__baitap2_codeFirst.Entities;
using Huy_.NET__baitap2_codeFirst.Models;

namespace Huy_.NET__baitap2_codeFirst.Service
{
    public interface DepartmentService
    {
        public IQueryable<DepartmentInfo> FindAll();
        public Task<DepartmentInfo> FindDpm(int id);
        public void Create(DepartmentInfo departmentInfo);
        

    }
}
