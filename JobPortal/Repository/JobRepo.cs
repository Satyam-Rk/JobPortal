using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Models;
using Microsoft.EntityFrameworkCore;
using static JobPortal.DataModels.JobResponse;

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

		public async Task<JobResponse> GetJobsList(JobRequestDTO request)
		{
			try
			{
				var query = _dbContext.Jobs.Include(j => j.Location).Include(j => j.Department).AsQueryable();

				if (!string.IsNullOrWhiteSpace(request.SearchKey))
					query = query.Where(j => j.Title.Contains(request.SearchKey) || j.Description.Contains(request.SearchKey));

				if (request.LocationId != 0)
					query = query.Where(j => j.LocationId == request.LocationId);

				if (request.DepartmentId != 0)
					query = query.Where(j => j.DepartmentId == request.DepartmentId);

				var total = await query.CountAsync();

				var jobs = await query
					.OrderByDescending(j => j.PostedDate)
					.Skip((request.PageNo - 1) * request.PageSize)
					.Take(request.PageSize)
					.Select(j => new JobDTO
					{
						Id = j.Id,
						Code = j.Code,
						Title = j.Title,
						Location = j.Location.Name,
						Department = j.Department.Name,
						PostedDate = j.PostedDate,
						ClosingDate = j.ClosingDate
					})
					.ToListAsync();

				return new JobResponse
				{
					Total = total,
					Data = jobs,
					Message = "Data retrived."
				};
			}
			catch (Exception ex)
			{
				throw ex;
			}
			}

		public async Task<JobDetailDTO> GetJobById(int id)
		{
			var query = await _dbContext.Jobs
				.Include(j => j.Location)
				.Include(j => j.Department)
				.Where(j => j.Id == id)
				.Select(j => new JobDetailDTO
				{
					Id = j.Id,
					Code = j.Code,
					Title = j.Title,
					Description = j.Description,
					PostedDate = j.PostedDate,
					ClosingDate = j.ClosingDate,
					Location = new JobDetailDTO.LocationDTO
					{
						Id = j.Location.Id,
						Title = j.Location.Name,
						City = j.Location.City,
						State = j.Location.State,
						Country = j.Location.Country,
						Zip = j.Location.Zip
					},
					Department = new JobDetailDTO.DepartmentDTO
					{
						Id = j.Department.Id,
						Title = j.Department.Name
					}
				}).FirstOrDefaultAsync();

			return query;
		}
	}
}
