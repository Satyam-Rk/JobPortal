using JobPortal.DataModels;
using JobPortal.Models;

namespace JobPortal.Interface
{
    public interface ILocationRepo
    {
        Task<Location> InsertLocation(LocationModel location);
        Task<Location> UpdateLocation(int id, LocationModel location);
        Task<IEnumerable<Location>> GetLocations();
    }
}
