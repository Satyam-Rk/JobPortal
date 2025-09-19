using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Repository
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly JobPortalContext _dbContext;

        public DepartmentRepo(JobPortalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Department> InsertDepartment(DepartmentModel department)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Department data = new Department
                    {
                        Name = department.DepartmentName,
                        CreatedDate = DateTime.UtcNow
                    };

                    _dbContext.Departments.Add(data);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return data;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<Department> UpdateDepartment(int id, DepartmentModel department)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var dept = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);

                    if(dept != null)
                    {
                        dept.Name = department.DepartmentName;
                        dept.UpdatedDate = DateTime.UtcNow;

                        _dbContext.Entry(dept).State = EntityState.Modified;
                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return dept;
                    }

                    return dept;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                return await _dbContext.Departments.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
