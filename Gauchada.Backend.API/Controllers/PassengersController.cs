using Microsoft.AspNetCore.Mvc;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;

namespace Gauchada.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private IPassengerService _passengerService;

        public PassengersController(IPassengerService passengerService)
        {
            _passengerService = passengerService;
        }
        
        [HttpGet]
        public async Task<ActionResult<ControllerResponse>> GetPassengerByUserName(string passengerUserName)
        {
            var passenger = await _passengerService.GetPassengerByUserName(passengerUserName);

            if (passenger == null)
            {
                return NotFound(ControllerResponse.FailureResponse("Passager Not Found"));
            }
            return Ok(ControllerResponse.SuccessResponse(passenger, "Passenger Found"));
        }

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostPassenger(UserDTO passenger)
        {
            var savedPassenger = await _passengerService.AddPassenger(passenger);
            
            if (!savedPassenger)
            {
                return BadRequest(ControllerResponse.FailureResponse("Passenger Not Registered"));
            }
            
            return Ok(ControllerResponse.SuccessResponse(null, "Passenger Registered"));
        }
    }
}
