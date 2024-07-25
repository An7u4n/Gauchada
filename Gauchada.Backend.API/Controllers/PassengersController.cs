using Microsoft.AspNetCore.Mvc;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Services.Interfaces;
using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;
using Microsoft.Data.SqlClient;

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
            try
            {
                var passenger = await _passengerService.GetPassengerByUserName(passengerUserName);
                return Ok(ControllerResponse.SuccessResponse(passenger, "Passenger Found"));
            }
            catch (Exception ex)
            {
                return NotFound(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostPassenger([FromForm] AddUserDTO passenger)
        {
            try
            {
                await _passengerService.AddPassenger(passenger);
                return Ok(ControllerResponse.SuccessResponse(null, "Passenger Registered"));
            }
            catch (Exception ex)
            {
                return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }
        }
    }
}
