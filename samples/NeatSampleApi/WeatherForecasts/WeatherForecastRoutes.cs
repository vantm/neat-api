using NeatApi.Routing;

namespace NeatSampleApi.WeatherForecasts;

public class WeatherForecastRoutes() : PathBaseRoutingModule("/weatherforecast")
{
    protected override void AddRoutes(IEndpointRouteBuilder app, IEndpointConventionBuilder convention, IRoutingModuleContext context)
    {
        convention.WithTags("WeatherForecast").WithOpenApi();

        app.MapGet("", (IWeatherForecastRepo repo) =>
        {
            return repo.GetForecasts();
        }).WithName("GetWeatherForecast");

        if (context.Environment.IsDevelopment())
        {
            app.MapGet("debug", () =>
            {
                return "dotnet v" + Environment.Version.ToString();
            });
        }
    }
}
