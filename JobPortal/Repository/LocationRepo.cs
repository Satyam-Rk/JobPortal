using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace JobPortal.Repository
{
    public class LocationRepo : ILocationRepo
    {
        private readonly JobPortalContext _dbContext;

        public LocationRepo(JobPortalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Location> InsertLocation(LocationModel location)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Location data = new Location
                    {
                        Name = location.Name,
                        City = location.City,
                        State = location.State,
                        Country = location.Country,
                        Zip = location.Zip,
                        CreatedDate = DateTime.UtcNow
                    };

                    _dbContext.Locations.Add(data);
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

        public async Task<Location> UpdateLocation(int id, LocationModel location)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = await _dbContext.Locations.FirstOrDefaultAsync(d => d.Id == id);

                    if (data != null)
                    {
                        data.Name = location.Name;
                        data.City = location.City;
                        data.State = location.State;
                        data.Country = location.Country;
                        data.Zip = location.Zip;
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

        public async Task<IEnumerable<Location>> GetLocations()
        {
            try
            {
                return await _dbContext.Locations.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
