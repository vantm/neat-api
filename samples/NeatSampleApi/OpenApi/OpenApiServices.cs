using NeatApi.DependencyInjection;

namespace NeatSampleApi.OpenApi;

public class OpenApiServices : IServiceModule
{
    public void Register(IServiceCollection services, IServiceModuleContext context)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
