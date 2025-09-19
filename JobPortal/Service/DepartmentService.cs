using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobPortal.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo _departmentRepo;

        public DepartmentService(IDepartmentRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public async Task<ResultDTO> InsertDepartment(DepartmentModel department)
        {
            try
            {
                Department departmentData = await _departmentRepo.InsertDepartment(department);
                return new ResultDTO
                {
                    Result = true,
                    Details = departmentData.Name,
                    ResultMessage = "Department added successfully!",
                    Status = HttpStatusCode.Created
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResultDTO> UpdateDepartment(int id, DepartmentModel department)
        {
            try
            {
                Department updatedDepartment = await _departmentRepo.UpdateDepartment(id, department);

                if (updatedDepartment != null)
                {
                    return new ResultDTO
                    {
                        Result = true,
                        Details = updatedDepartment,
                        ResultMessage = "Department updated successfully!",
                        Status = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new ResultDTO
                    {
                        Result = true,
                        Details = updatedDepartment,
                        ResultMessage = "Department not found",
                        Status = HttpStatusCode.NotFound
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDTO> GetDepartments()
        {
            try
            {
                IEnumerable<Department> departmentList = await _departmentRepo.GetDepartments();

                if (departmentList != null)
                {
                    if (departmentList.Any())
                    {
                        return new ResultDTO
                        {
                            Result = true,
                            Details = departmentList,
                            ResultMessage = "Data found!",
                            Status = HttpStatusCode.OK
                        };
                    }
                    else
                    {
                        return new ResultDTO
                        {
                            Result = false,
                            Details = departmentList,
                            ResultMessage = "No data found!",
                            Status = HttpStatusCode.NoContent
                        };
                    }
                }
                else
                {
                    return new ResultDTO
                    {
                        Result = false,
                        Details = departmentList,
                        ResultMessage = "Error retrieving data!",
                        Status = HttpStatusCode.InternalServerError
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
