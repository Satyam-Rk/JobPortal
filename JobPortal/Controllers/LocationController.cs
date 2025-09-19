using JobPortal.DataModels;
using JobPortal.Interface;
using JobPortal.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost]
        public async Task<ResultDTO> AddLocation(LocationModel location)
        {
            if (location == null)
            {
                return new ResultDTO
                {
                    Result = false,
                    Details = location,
                    ResultMessage = "Incomplete data!",
                    Status = HttpStatusCode.BadRequest
                };
            }

            try
            {
                ResultDTO locationData = await _locationService.InsertLocation(location);
                return locationData;
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
        public async Task<ResultDTO> UpdateLocation(int id, LocationModel location)
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
                ResultDTO updatedlocation = await _locationService.UpdateLocation(id, location);
                return updatedlocation;
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


        [HttpGet]
        public async Task<ResultDTO> GetLocationsList()
        {
            try
            {
                ResultDTO studentsList = await _locationService.GetLocations();
                return studentsList;
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
    }
}
