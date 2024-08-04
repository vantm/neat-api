using NeatApi.Routing;

namespace NeatSampleApi.OpenApi;

public class OpenApiRoutes : IRoutingModule
{
    public void AddRoutes(IEndpointRouteBuilder app, IRoutingModuleContext context)
    {
        if (context.Environment.IsDevelopment())
        {
            if (app is WebApplication web)
            {
                web.UseSwaggerUI();
            }

            app.MapSwagger();
        }
    }
}
