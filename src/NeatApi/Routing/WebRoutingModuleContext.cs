using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NeatApi.Routing;

public class WebRoutingModuleContext(WebApplication app) : IRoutingModuleContext
{
    public IHostEnvironment Environment => app.Environment;
    public IConfiguration Configuration => app.Configuration;
}
