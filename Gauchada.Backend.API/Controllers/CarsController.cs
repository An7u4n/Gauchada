using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gauchada.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<ControllerResponse>> GetCar(string carPlate)
        {
            var car = await _carService.GetCar(carPlate);

            if (car == null)
            {
                return NotFound(ControllerResponse.FailureResponse("Car Not Found"));
            }
            return Ok(ControllerResponse.SuccessResponse(car, "Car Found"));
        }

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostCar(CarDTO car)
        {
            var savedCar = await _carService.SaveCar(car);

            if (!savedCar)
            {
                return BadRequest(ControllerResponse.FailureResponse("Car Not Registered"));
            }

            return Ok(ControllerResponse.SuccessResponse(null, "Car Registered"));
        }
    }
}
