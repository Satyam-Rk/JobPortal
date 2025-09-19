using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Repository
{
    public class JobRepo : IJobRepo
    {
        private readonly JobPortalContext _dbContext;

        public JobRepo(JobPortalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Job> InsertJob(JobModel job)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var lastData = await _dbContext.Jobs.OrderByDescending(d => d.Id).FirstOrDefaultAsync();
                    int nextId = lastData != null ? lastData.Id + 1 : 1;

                    Job data = new Job
                    {
                        Code = "Job-" + nextId.ToString("D4"),
                        Title = job.Title,
                        Description = job.Description,
                        LocationId = job.LocationId,
                        DepartmentId = job.DepartmentId,
                        PostedDate = DateTime.UtcNow,
                        ClosingDate = job.ClosingDate
                    };

                    _dbContext.Jobs.Add(data);
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

		public async Task<Job> UpdateJob(int id, JobModel job)
		{
			using (var transaction = _dbContext.Database.BeginTransaction())
			{
				try
				{
					var data = await _dbContext.Jobs.FirstOrDefaultAsync(d => d.Id == id);

					if (data != null)
					{
						data.Title = job.Title;
						data.Description = job.Description;
						data.LocationId = job.LocationId;
						data.DepartmentId = job.DepartmentId;
						data.ClosingDate = job.ClosingDate;
						data.UpdatedDate = DateTime.UtcNow;

						_dbContext.Entry(data).State = EntityState.Modified;
						await _dbContext.SaveChangesAsync();
						await transaction.CommitAsync();

						return data;
					}

					return data;
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					throw ex;
				}
			}
		}
	}
}
