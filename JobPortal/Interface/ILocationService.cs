using JobPortal.DataModels;

namespace JobPortal.Interface
{
    public interface ILocationService
    {
        Task<ResultDTO> InsertLocation(LocationModel location);
        Task<ResultDTO> UpdateLocation(int id, LocationModel location);
        Task<ResultDTO> GetLocations();
    }
}
