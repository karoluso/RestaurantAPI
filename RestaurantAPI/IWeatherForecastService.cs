using System.Collections.Generic;

namespace RestaurantAPI
{
    public interface IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> Get(int numOfresults, int minTemp, int maxTemp);
    }
}