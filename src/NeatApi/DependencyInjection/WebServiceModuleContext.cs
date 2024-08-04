using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NeatApi.DependencyInjection;

public class WebServiceModuleContext(WebApplicationBuilder builder) : IServiceModuleContext
{
    public IHostEnvironment Environment => builder.Environment;
    public IConfiguration Configuration => builder.Configuration;
}
