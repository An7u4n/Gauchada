using Microsoft.AspNetCore.Mvc;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;

namespace Gauchada.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private UserService _userService;

        public PassengersController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult<ControllerResponse>> GetPassengerInfo(string passengerUserName)
        {
            var passenger = await _userService.GetPassengerByUserName(passengerUserName);

            if (passenger == null)
            {
                return NotFound(ControllerResponse.FailureResponse("Passager Not Found"));
            }
            return Ok(ControllerResponse.SuccessResponse(passenger, ""));
        }

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostPassenger(PassengerDTO passenger)
        {
            var savedPassenger = await _userService.RegisterPassenger(passenger);
            
            if (!savedPassenger)
            {
                return BadRequest(ControllerResponse.FailureResponse("Passenger not registered"));
            }
            
            return Ok(ControllerResponse.SuccessResponse(null, "Passenger registered"));
        }
    }
}
