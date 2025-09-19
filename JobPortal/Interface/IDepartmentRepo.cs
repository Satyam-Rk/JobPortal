using JobPortal.DataModels;
using JobPortal.Models;

namespace JobPortal.Interface
{
    public interface IDepartmentRepo
    {
        Task<Department> InsertDepartment(DepartmentModel department);
        Task<Department> UpdateDepartment(int id, DepartmentModel department);
        Task<IEnumerable<Department>> GetDepartments();
    }
}
