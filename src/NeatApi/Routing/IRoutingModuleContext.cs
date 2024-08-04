using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NeatApi.Routing;

public interface IRoutingModuleContext
{
    IHostEnvironment Environment { get; }
    IConfiguration Configuration { get; }
}
