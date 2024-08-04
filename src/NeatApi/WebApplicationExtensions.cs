using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NeatApi.DependencyInjection;
using NeatApi.Routing;
using System.Reflection;

#pragma warning disable IDE0130, S1200
namespace Microsoft.AspNetCore.Builder;

public static class WebApplicationExtensions
{
    public static void AddModules(this WebApplicationBuilder builder, Assembly assembly, params Assembly[] moreAssemblies)
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

    public static void MapModuleRoutes(this WebApplication app)
    {
        var routingModules = app.Services.GetRequiredService<IEnumerable<IRoutingModule>>();
        var logger = app.Services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(typeof(IServiceModule).FullName!);
        var context = new WebRoutingModuleContext(app);

        foreach (var routingModule in routingModules)
        {
            routingModule.AddRoutes(app, context);

            logger.LogInformation(
                "The module {ModuleName} had been registered",
                routingModule.GetType().FullName!);
        }
    }
}
