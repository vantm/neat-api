using NeatApi.DependencyInjection;

namespace NeatSampleApi.WeatherForecasts;

public class WeatherForecastServices : IServiceModule
{
    public void Register(IServiceCollection services, IServiceModuleContext context)
    {
        services.AddSingleton<IWeatherForecastRepo, SampleWeatherForecastRepo>();

        services.AddOptionsWithValidateOnStart<WeatherForecastOptions>()
            .BindConfiguration(WeatherForecastOptions.Name);
    }
}
