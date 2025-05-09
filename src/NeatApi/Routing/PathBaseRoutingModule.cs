using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Diagnostics.CodeAnalysis;

namespace NeatApi.Routing;

public abstract class PathBaseRoutingModule(
    [StringSyntax("Route")] 
    string pathBase) : IRoutingModule
{
    public void AddRoutes(IEndpointRouteBuilder app, IRoutingModuleContext context)
    {
        var group = app.MapGroup(pathBase);
        var groupContext = new PathBaseRoutingModuleContext(context, group);
        AddRoutes(group, group, groupContext);
    }

    protected abstract void AddRoutes(IEndpointRouteBuilder app, IEndpointConventionBuilder convention, IRoutingModuleContext context);
}

