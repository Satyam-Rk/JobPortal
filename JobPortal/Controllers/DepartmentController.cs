using JobPortal.DataModels;
using JobPortal.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<ResultDTO> AddDepartment(DepartmentModel department)
        {
            if (department.DepartmentName == string.Empty || department.DepartmentName == "string")
            {
                return new ResultDTO
                {
                    Result = false,
                    Details = department,
                    ResultMessage = "Incomplete data!",
                    Status = HttpStatusCode.BadRequest
                };
            }

            try
            {
                ResultDTO departmentData = await _departmentService.InsertDepartment(department);
                return departmentData;
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Result = false,
                    Details = ex.ToString(),
                    ResultMessage = "Something went wrong!",
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPut("id")]
        public async Task<ResultDTO> UpdateDepartment(int id, DepartmentModel department)
        {
            if (id == 0)
            {
                return new ResultDTO
                {
                    Result = false,
                    Details = id,
                    ResultMessage = "Enter a valid Id",
                    Status = HttpStatusCode.BadRequest
                };
            }

            try
            {
                ResultDTO updatedDepartment = await _departmentService.UpdateDepartment(id, department);
                return updatedDepartment;
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Result = false,
                    Details = ex.ToString(),
                    ResultMessage = "Something went wrong!",
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }


        [HttpGet]
        public async Task<ResultDTO> GetDepartmentsList()
        {
            try
            {
                ResultDTO studentsList = await _departmentService.GetDepartments();
                return studentsList;
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    Result = false,
                    Details = ex.ToString(),
                    ResultMessage = "Something went wrong!",
                    Status = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
