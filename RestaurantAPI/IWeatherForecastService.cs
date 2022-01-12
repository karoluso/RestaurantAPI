using System.Collections.Generic;

namespace RestaurantAPI
{
    public interface IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> Get();
    }
}