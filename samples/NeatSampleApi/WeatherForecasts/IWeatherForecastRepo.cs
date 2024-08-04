namespace NeatSampleApi.WeatherForecasts;

public interface IWeatherForecastRepo
{
    IEnumerable<WeatherForecast> GetForecasts();
}
