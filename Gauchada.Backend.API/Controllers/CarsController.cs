using Gauchada.Backend.Model.DTO;
using Gauchada.Backend.Model.Response;
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gauchada.Backend.API.Controllers
{
    [Authorize]
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

        [HttpGet("GetCarsByUserName")]
        public async Task<ActionResult<ControllerResponse>> GetCarsByUserName(string userName)
        {
            try
            {
                var cars = await _carService.GetCarsByUserName(userName);
                return Ok(ControllerResponse.SuccessResponse(cars, "User Cars Found"));
            }
            catch (Exception ex)
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
                return StatusCode(201, ControllerResponse.SuccessResponse(null, "Car Registered"));
            }
            catch(Exception ex)
            {
               return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ControllerResponse>> DeleteCar(string carPlate)
        {
            try
            {
                await _carService.DeleteCar(carPlate);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ControllerResponse.FailureResponse(ex.Message));
            }
        }
    }
}
