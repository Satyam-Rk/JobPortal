using JobPortal.DataModels;

namespace JobPortal.Interface
{
    public interface IJobService
    {
        Task<ResultDTO> InsertJob(JobModel job);
        Task<ResultDTO> UpdateJob(int id, JobModel job);
        Task<JobResponse> GetJobs(JobRequestDTO request);
        Task<JobDetailDTO> GetJobByIdAsync(int id);
    }
}
