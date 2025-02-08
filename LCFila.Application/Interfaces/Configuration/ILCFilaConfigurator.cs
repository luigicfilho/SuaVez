using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LCFila.Application.Interfaces.Configuration;

public interface ILCFilaConfigurator
{
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}
