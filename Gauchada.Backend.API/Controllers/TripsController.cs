using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gauchada.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private ITripService _tripService;
        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<ActionResult<ControllerResponse>> GetTripsByLocations(string origin, string destination)
        {
            try
            {
                var trips = await _tripService.GetTripsByLocation(origin, destination);
                return Ok(ControllerResponse.SuccessResponse(trips, "Trips Found"));
            }
            catch(Exception ex)
            {
                return NotFound(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostTrip(TripDTO trip)
        {
            try
            {
                await _tripService.SetTrip(trip);
                return Ok(ControllerResponse.SuccessResponse(trip, "Trip Created"));
            }
            catch (Exception ex)
            {
                return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }

        }

        [HttpPost("AddPassengerToATrip")]
        public async Task<ActionResult<ControllerResponse>> AddPassengerToATrip(int tripId, string passengerUserName)
        {
            try
            {
                await _tripService.AddPassengerToATrip(tripId, passengerUserName);
                return Ok(ControllerResponse.SuccessResponse(null, "Passenger Added To The Trip"));
            }
            catch (Exception ex)
            {
                return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }

        }

        [HttpDelete("RemovePassagerFromATrip")]
        public async Task<ActionResult<ControllerResponse>> RemovePassengerFromATrip(int tripId, string passengerUserName)
        {
            try
            {
                await _tripService.RemovePassengerFromATrip(tripId, passengerUserName);
                return Ok(ControllerResponse.SuccessResponse(null, "Passenger Removed From The Trip"));
            }
            catch(Exception ex)
            {
                return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }
        }
    }
}
