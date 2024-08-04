# NEAT API

![NuGet Version](https://img.shields.io/nuget/v/NeatApi)
![GitHub last commit](https://img.shields.io/github/last-commit/vantm/neat-api)

A super simple library to help you modularize your minimal API projects.

## Install

```bash
dotnet add package NeatApi
```

## Usage

### Add **NeatApi** to your app

Looking for detail implementations in the folder `/samples`. Here is the basic usage:

```diff
var builder = WebApplication.CreateBuilder(args);

+ builder.AddNeatApi();

var app = builder.Build();

+ app.MapNeatApi();

app.Run();
```

### Register Services

```csharp
public class WeatherForecastServices : IServiceModule
{
    public void Register(IServiceCollection services, IServiceModuleContext context)
    {
        services.AddSingleton<IWeatherForecastRepo, SampleWeatherForecastRepo>();

        services.AddOptionsWithValidateOnStart<WeatherForecastOptions>()
            .BindConfiguration(WeatherForecastOptions.Name);
    }
}
```

### Define Routes

#### General Cases

```csharp
public class WeatherForecastRoutes : IRoutingModule
{
    protected void AddRoutes(IEndpointRouteBuilder app, IRoutingModuleContext context)
    {
        app.MapGet("/weatherforecast", (IWeatherForecastRepo repo) =>
        {
            return repo.GetForecasts();
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        if (context.Environment.IsDevelopment())
        {
            app.MapGet("debug", () =>
            {
                return "dotnet v" + Environment.Version.ToString();
            });
        }
    }
}
```

#### Grouped Routes

```csharp
public class WeatherForecastRoutes() : GroupedRoutingModuleBase("/weatherforecast")
{
    protected override void AddRoutes(IEndpointRouteBuilder app, IEndpointConventionBuilder convention, IRoutingModuleContext context)
    {
        convention.WithTags("WeatherForecast").WithOpenApi();

        app.MapGet("", (IWeatherForecastRepo repo) =>
        {
            return repo.GetForecasts();
        }).WithName("GetWeatherForecast");
    }
}
```

## License

MIT