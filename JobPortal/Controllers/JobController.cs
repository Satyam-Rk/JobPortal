using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using static JobPortal.DataModels.JobRequestDTO;
using static JobPortal.DataModels.JobResponse;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        public async Task<ResultDTO> AddJob(JobModel job)
        {
            if (job == null)
            {
                return new ResultDTO
                {
                    Result = false,
                    Details = job,
                    ResultMessage = "Incomplete data!",
                    Status = HttpStatusCode.BadRequest
                };
            }

            try
            {
                ResultDTO jobData = await _jobService.InsertJob(job);
                return jobData;
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
        public async Task<ResultDTO> UpdateJob(int id, JobModel job)
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
                ResultDTO updatedJob = await _jobService.UpdateJob(id, job);
                return updatedJob;
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

        [HttpPost("list")]
        public async Task<JobResponse> GetJobsList(JobRequestDTO request)
        {
            try
            {
                if(!string.IsNullOrEmpty(request.SearchKey) && request.PageNo != 0 && request.PageSize != 0)
                {
                    var jobsList = await _jobService.GetJobs(request);
                    return jobsList;
                }
                else
                {
                    return new JobResponse
                    {
                        Message = "Invalid parameters."
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("id")]
        public async Task<JobDetailDTO> GetJobById(int id)
        {
            try
            {
                var job = await _jobService.GetJobByIdAsync(id);
                return job;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
