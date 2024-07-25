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
            try
            {
                var car = await _carService.GetCarByPlate(carPlate);
                return Ok(ControllerResponse.SuccessResponse(car, "Car Found"));
            }
            catch(Exception ex)
            {
                return NotFound(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ControllerResponse>> PostCar(CarDTO car)
        {
            try
            {
                await _carService.SaveCar(car);
                return Ok(ControllerResponse.SuccessResponse(null, "Car Registered"));
            }
            catch(Exception ex)
            {

               return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }
        }
    }
}
