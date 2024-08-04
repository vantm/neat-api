using Microsoft.AspNetCore.Routing;

namespace NeatApi.Routing;

public interface IRoutingModule
{
    void AddRoutes(IEndpointRouteBuilder app, IRoutingModuleContext context);
}
