using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IWeatherForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public TempRange Get()
        {
            var range = new TempRange() { MaxTemp = 10, MinTemp = 20 };

            return range;
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Genereate([FromQuery] int numOfResults, [FromBody] TempRange range)
        {
            // HttpContext.Response.StatusCode = 401;
            // return StatusCode(401, $"Hello {name}");

            if (numOfResults > 0 && range.MaxTemp > range.MinTemp)
            {
                var results = _service.Get(numOfResults, range.MinTemp, range.MaxTemp);
                return Ok(results);
            }
                return BadRequest();
        }
    }
}