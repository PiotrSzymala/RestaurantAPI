namespace RestaurantAPI;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get(int resultsToReturn, int minTemperature, int maxTemperature);
}