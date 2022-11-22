using Microsoft.AspNetCore.Mvc;
using SmartHome.Models.Common;
using SmartHome.Repository;

namespace SmartHome.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherRepository _repository;
        public WeatherController(IWeatherRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTemperature()
        {
            var result = _repository.GetTemperature().Result;

            return Ok(new Response<string>(result));
        }
    }
}
