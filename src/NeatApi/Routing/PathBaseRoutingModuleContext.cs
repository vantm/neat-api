using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NeatApi.Routing;

public class PathBaseRoutingModuleContext(IRoutingModuleContext context, RouteGroupBuilder routeGroupBuilder) : IRoutingModuleContext
{
    public IHostEnvironment Environment => context.Environment;
    public IConfiguration Configuration => context.Configuration;
    public IEndpointRouteBuilder App => routeGroupBuilder;
}
