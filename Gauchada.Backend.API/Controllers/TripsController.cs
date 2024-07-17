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
            var trips = await _tripService.GetTripsByLocation(origin, destination);

            if (trips == null)
            {
                return NotFound(ControllerResponse.FailureResponse("There Isnt Trips Between This Locations"));
            }
            return Ok(ControllerResponse.SuccessResponse(trips, "Trips Found"));
        }

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostTrip(TripDTO trip)
        {
            var result = await _tripService.SetTrip(trip);

            if (result == true)
                return BadRequest(ControllerResponse.FailureResponse("Trip Not Created"));

            return Ok(ControllerResponse.SuccessResponse(trip, "Trip Created"));
        }

        [HttpPost("AddPassengerToATrip")]
        public async Task<ActionResult<ControllerResponse>> AddPassengerToATrip(int tripId, string passengerUserName)
        {
            var result = await _tripService.AddPassengerToATrip(tripId, passengerUserName);

            if (result == false)
                return BadRequest(ControllerResponse.FailureResponse("Passenger Not Added"));

            return Ok(ControllerResponse.SuccessResponse(null, "Passenger Added To The Trip"));
        }
    }
}
