using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NeatApi.DependencyInjection;

public interface IServiceModuleContext
{
    IHostEnvironment Environment { get; }
    IConfiguration Configuration { get; }
}
