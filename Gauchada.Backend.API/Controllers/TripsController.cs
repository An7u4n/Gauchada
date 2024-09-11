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

        [HttpGet("ExactDate")]
        public async Task<ActionResult<ControllerResponse>> GetTripsByExactDate(string origin, string destination, DateTime date)
        {
            try
            {
                var trips = await _tripService.GetTripsByExactDate(origin, destination, date);
                return Ok(ControllerResponse.SuccessResponse(trips, "Trips Found"));
            }
            catch (Exception ex)
            {
                return NotFound(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpGet("DateRange")]
        public async Task<ActionResult<ControllerResponse>> GetTripsByDateRange(string origin, string destination, DateTime minDate, DateTime maxDate)
        {
            try
            {
                var trips = await _tripService.GetTripsByDateRange(origin, destination, minDate, maxDate);
                return Ok(ControllerResponse.SuccessResponse(trips, "Trips Found"));
            }
            catch (Exception ex)
            {
                return NotFound(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpGet("GetUserTrips")]
        public async Task<ActionResult<ControllerResponse>> GetUserTrips(string userName)
        {
            try
            {
                var trips = await _tripService.GetUserTrips(userName);
                return Ok(ControllerResponse.SuccessResponse(trips, "Trips Found"));
            }
            catch (Exception ex)
            {
                return NotFound(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpGet("GetTripPassengers")]
        public async Task<ActionResult<ControllerResponse>> GetTripPassengers(int tripId)
        {
            try
            {
                var passengers = await _tripService.GetTripPassengers(tripId);
                return Ok(ControllerResponse.SuccessResponse(passengers, "Trips Found"));
            }
            catch (Exception ex)
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
