using Microsoft.AspNetCore.Mvc;
using SmartHome.DTOs.Weather;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTemperature()
        {
            var result = _repository.GetTemperature().Result;

            return Ok(new Response<string>(result));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WeekWeatherDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWeeklyWeatherReport()
        {        
            var result = _repository.GetWeeklyWeatherReport().Result;

            return Ok(new Response<IEnumerable<WeekWeatherDto>>(result));
        }
    }
}
