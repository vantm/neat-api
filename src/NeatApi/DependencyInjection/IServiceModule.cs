using Microsoft.Extensions.DependencyInjection;

namespace NeatApi.DependencyInjection;

public interface IServiceModule
{
    void Register(IServiceCollection services, IServiceModuleContext context);
}
