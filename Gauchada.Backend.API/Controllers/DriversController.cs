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

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostDriver([FromForm] AddUserDTO driver)
        {
            try
            {
                await _driverService.AddDriver(driver);
                return Ok(ControllerResponse.SuccessResponse(null, "Driver Registered"));

            }
            catch(Exception ex)
            {
                return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }
        }
    }
}
