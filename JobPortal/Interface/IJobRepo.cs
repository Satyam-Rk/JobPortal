using JobPortal.DataModels;
using JobPortal.Models;

namespace JobPortal.Interface
{
    public interface IJobRepo
    {
        Task<Job> InsertJob(JobModel job);
        Task<Job> UpdateJob(int id, JobModel job);
    }
}
