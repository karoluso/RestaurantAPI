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

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var results=_service.Get();
            return results;
        }

        [HttpGet("currentDay/{max}")]
        //[Route("currentDay")]
        public IEnumerable<WeatherForecast> Get2([FromQuery]int? take, [FromRoute]int? max)
        {
            var results = _service.Get();
            return results;
        }

        [HttpPost]
        public ActionResult<string>   Hello([FromBody] string name)
        {
            // HttpContext.Response.StatusCode = 401;
            // return StatusCode(401, $"Hello {name}");
            return NotFound($"Hello {name}");
        }
    }
}