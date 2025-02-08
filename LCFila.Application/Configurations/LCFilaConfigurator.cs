using LCFila.Application.Interfaces.Configuration;
using LCFila.Application.Extensions;
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
