using Gauchada.Backend.Model.Response;
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gauchada.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : Controller
    {
        private ILoginService _loginService;

        public UserLoginController(ILoginService loginService)
        {
            _loginService = loginService;
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
    }
}
