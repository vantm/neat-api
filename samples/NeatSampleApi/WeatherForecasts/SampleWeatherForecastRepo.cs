
using Microsoft.Extensions.Options;

namespace NeatSampleApi.WeatherForecasts;

public class SampleWeatherForecastRepo(IOptions<WeatherForecastOptions> options) : IWeatherForecastRepo
{
    private static readonly string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        return Enumerable.Range(1, options.Value.PageSize).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaries[Random.Shared.Next(_summaries.Length)]
            ))
            .ToArray();
    }
}
