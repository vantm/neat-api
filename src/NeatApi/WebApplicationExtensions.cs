using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NeatApi.DependencyInjection;
using NeatApi.Routing;

using System.Reflection;

#pragma warning disable IDE0130, S1200
namespace Microsoft.AspNetCore.Builder;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Find and add NeatApi modules in the entry assembly.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddNeatApi(this WebApplicationBuilder builder)
    {
        builder.AddNeatApi(Assembly.GetEntryAssembly()!);
    }

    /// <summary>
    /// Find and add NeatApi modules are in the provided assemblies.
    /// </summary>
    public static void AddNeatApi(this WebApplicationBuilder builder, Assembly assembly, params Assembly[] moreAssemblies)
    {
        Assembly[] assemblies = [assembly, .. moreAssemblies];

        // Register service modules
        var serviceModuleTypes = ModuleHelper.FindServiceModuleTypes(assemblies);
        var serviceModuleContext = new WebServiceModuleContext(builder);
        foreach (var serviceModuleType in serviceModuleTypes)
        {
            var serviceModule = (IServiceModule)Activator.CreateInstance(serviceModuleType)!;
            serviceModule.Register(builder.Services, serviceModuleContext);

            // Register service module to let container manages their life cycle.
            builder.Services.AddSingleton(serviceModule);
        }

        // Register routing modules
        var routingModuleTypes = ModuleHelper.FindRoutingModuleTypes(assemblies);
        foreach (var routingModuleType in routingModuleTypes)
        {
            builder.Services.AddSingleton(typeof(IRoutingModule), routingModuleType);
        }
    }

    /// <summary>
    /// Map NeatApi routes
    /// </summary>
    public static void MapNeatApi(this WebApplication app)
    {
        LogServicesModules(app);

        var routingModules = app.Services.GetRequiredService<IEnumerable<IRoutingModule>>();
        var logger = app.Services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(IRoutingModule).FullName!);
        var context = new WebRoutingModuleContext(app);

        foreach (var routingModule in routingModules)
        {
            routingModule.AddRoutes(app, context);

            logger.LogInformation(
                "The module {ModuleName} had been registered",
                routingModule.GetType().FullName!);
        }
    }

    private static void LogServicesModules(WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(IServiceModule).FullName!);

        if (!logger.IsEnabled(LogLevel.Information))
        {
            return;
        }

        var servicesModule = app.Services.GetRequiredService<IEnumerable<IServiceModule>>();

        foreach (var serviceModule in servicesModule)
        {
            logger.LogInformation(
                "The service module {ModuleName} had been registered",
                serviceModule.GetType().FullName!);
        }
    }
}
