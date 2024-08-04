using System.ComponentModel.DataAnnotations;

namespace NeatSampleApi.WeatherForecasts;

public class WeatherForecastOptions
{
    public const string Name = "WeatherForecast";

    [Required]
    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}
