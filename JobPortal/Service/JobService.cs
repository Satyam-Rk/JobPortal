using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Models;
using JobPortal.Repository;
using System.Net;

namespace JobPortal.Service
{
    public class JobService : IJobService
    {
        private readonly IJobRepo _jobRepo;

        public JobService(IJobRepo jobRepo)
        {
            _jobRepo = jobRepo;
        }

        public async Task<ResultDTO> InsertJob(JobModel job)
        {
            try
            {
                Job jobData = await _jobRepo.InsertJob(job);
                return new ResultDTO
                {
                    Result = true,
                    Details = jobData,
                    ResultMessage = "Job added successfully!",
                    Status = HttpStatusCode.Created
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResultDTO> UpdateJob(int id, JobModel job)
        {
            try
            {
                Job updatedJob = await _jobRepo.UpdateJob(id, job);

                if (updatedJob != null)
                {
                    return new ResultDTO
                    {
                        Result = true,
                        Details = updatedJob,
                        ResultMessage = "Job updated successfully!",
                        Status = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new ResultDTO
                    {
                        Result = true,
                        Details = updatedJob,
                        ResultMessage = "Job not found",
                        Status = HttpStatusCode.NotFound
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
