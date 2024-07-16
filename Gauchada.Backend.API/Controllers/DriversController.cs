using Microsoft.AspNetCore.Mvc;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;

namespace Gauchada.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<ControllerResponse>> GetDriverByUserName(string driverUserName)
        {
            var driver = await _driverService.GetDriverByUserName(driverUserName);

            if (driver == null)
            {
                return NotFound(ControllerResponse.FailureResponse("Driver Not Found"));
            }
            return Ok(ControllerResponse.SuccessResponse(driver, ""));
        }

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostDriver(UserDTO driver)
        {
            var savedDriver = await _driverService.AddDriver(driver);

            if (!savedDriver)
            {
                return BadRequest(ControllerResponse.FailureResponse("Driver not registered"));
            }

            return Ok(ControllerResponse.SuccessResponse(null, "Driver registered"));
        }
    }
}
