using Microsoft.AspNetCore.Mvc;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;
using Microsoft.AspNetCore.Authorization;

namespace Gauchada.Backend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("GetDriverTrips")]
        public async Task<ActionResult<ControllerResponse>> GetDriverTrips(string userName)
        {
            try
            {
                var drivers = await _driverService.GetDriverTrips(userName);
                return Ok(ControllerResponse.SuccessResponse(drivers, "Trips Found"));

            }
            catch (Exception ex)
            {
                return NotFound(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ControllerResponse>> GetDriverByUserName(string driverUserName)
        {
            try
            {
                var driver = await _driverService.GetDriverByUserName(driverUserName);
                return Ok(ControllerResponse.SuccessResponse(driver, "Driver Found"));

            }
            catch(Exception ex)
            {
                return NotFound(ControllerResponse.FailureResponse(ex.Message));
            }
        }
    }
}
