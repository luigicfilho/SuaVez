using LCFila.Application.Interfaces.Configuration;
using LCFilaApplication.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LCFila.Application.Configurations;

internal class LCFilaConfigurator : ILCFilaConfigurator
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureLCFila(configuration);
    }
}
