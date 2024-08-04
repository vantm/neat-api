using NeatApi.Routing;

namespace NeatSampleApi.WeatherForecasts;

public class WeatherForecastRoutes() : GroupedRoutingModuleBase("/weatherforecast")
{
    protected override void AddRoutes(IEndpointRouteBuilder app, IEndpointConventionBuilder convention, IRoutingModuleContext context)
    {
        convention.WithName("GetWeatherForecast").WithOpenApi();

        app.MapGet("", (IWeatherForecastRepo repo) =>
        {
            return repo.GetForecasts();
        });

        if (context.Environment.IsDevelopment())
        {
            app.MapGet("debug", () =>
            {
                return "dotnet v" + Environment.Version.ToString();
            });
        }
    }
}
