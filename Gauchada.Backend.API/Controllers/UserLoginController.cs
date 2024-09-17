using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;
using Gauchada.Backend.Services;
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gauchada.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IDriverService _driverService;
        private readonly IPassengerService _passengerService;

        public UserLoginController(ILoginService loginService, IDriverService driverService, IPassengerService passengerService)
        {
            _driverService = driverService;
            _loginService = loginService;
            _passengerService = passengerService;
        }

        [HttpPost("Driver")]
        public async Task<ActionResult<ControllerResponse>> DriverLogin(string username, string password)
        {
            try
            {
                var userToken = await _loginService.AuthenticateDriver(username, password);
                return Ok(ControllerResponse.SuccessResponse(userToken, "Token Generated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Passenger")]
        public async Task<ActionResult<ControllerResponse>> PassengerLogin(string username, string password)
        {
            try
            {
                var userToken = await _loginService.AuthenticatePassenger(username, password);
                return Ok(ControllerResponse.SuccessResponse(userToken, "Token Generated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register/Driver")]
        public async Task<ActionResult<ControllerResponse>> PostDriver([FromForm] AddUserDTO driver)
        {
            try
            {
                await _driverService.AddDriver(driver);
                return StatusCode(201, ControllerResponse.SuccessResponse(null, "Driver Registered"));

            }
            catch (Exception ex)
            {
                return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpPost("Register/Passenger")]
        public async Task<ActionResult<ControllerResponse>> PostPassenger([FromForm] AddUserDTO passenger)
        {
            try
            {
                await _passengerService.AddPassenger(passenger);
                return StatusCode(201, ControllerResponse.SuccessResponse(null, "Passenger Registered"));
            }
            catch (Exception ex)
            {
                return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }
        }
    }
}
