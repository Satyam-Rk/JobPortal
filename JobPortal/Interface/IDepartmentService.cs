using JobPortal.DataModels;

namespace JobPortal.Interface
{
    public interface IDepartmentService
    {
        Task<ResultDTO> InsertDepartment(DepartmentModel department);
        Task<ResultDTO> UpdateDepartment(int id, DepartmentModel department);
        Task<ResultDTO> GetDepartments();
    }
}
