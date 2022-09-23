using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery] int resultsToReturn, [FromBody] TemperatureSetter setter)
        {
            if (resultsToReturn < 0 || setter.Min > setter.Max)
            {
                return BadRequest();
            }

            var result = _service.Get(resultsToReturn, setter.Min, setter.Max);
            return Ok(result);
        }
    }
}