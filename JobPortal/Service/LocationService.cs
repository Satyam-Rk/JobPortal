using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Models;
using System.Net;

namespace JobPortal.Service
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepo _locationRepo;

        public LocationService(ILocationRepo locationRepo)
        {
            _locationRepo = locationRepo;
        }

        public async Task<ResultDTO> InsertLocation(LocationModel location)
        {
            try
            {
                Location locationData = await _locationRepo.InsertLocation(location);
                return new ResultDTO
                {
                    Result = true,
                    Details = locationData,
                    ResultMessage = "Location added successfully!",
                    Status = HttpStatusCode.Created
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResultDTO> UpdateLocation(int id, LocationModel location)
        {
            try
            {
                Location updatedLocation = await _locationRepo.UpdateLocation(id, location);

                if (updatedLocation != null)
                {
                    return new ResultDTO
                    {
                        Result = true,
                        Details = updatedLocation,
                        ResultMessage = "Location updated successfully!",
                        Status = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new ResultDTO
                    {
                        Result = true,
                        Details = updatedLocation,
                        ResultMessage = "Location not found",
                        Status = HttpStatusCode.NotFound
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDTO> GetLocations()
        {
            try
            {
                IEnumerable<Location> locationList = await _locationRepo.GetLocations();

                if (locationList != null)
                {
                    if (locationList.Any())
                    {
                        return new ResultDTO
                        {
                            Result = true,
                            Details = locationList,
                            ResultMessage = "Data found!",
                            Status = HttpStatusCode.OK
                        };
                    }
                    else
                    {
                        return new ResultDTO
                        {
                            Result = false,
                            Details = locationList,
                            ResultMessage = "No data found!",
                            Status = HttpStatusCode.NoContent
                        };
                    }
                }
                else
                {
                    return new ResultDTO
                    {
                        Result = false,
                        Details = locationList,
                        ResultMessage = "Error retrieving data!",
                        Status = HttpStatusCode.InternalServerError
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
