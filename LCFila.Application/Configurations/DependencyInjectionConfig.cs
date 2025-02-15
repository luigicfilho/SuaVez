using LCFila.Application.Interfaces.Identity;
using LCFila.Application.AppServices;
using LCFila.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LCFila.Application.IdentityService;
using LCFila.Infra.Configuration;

namespace LCFila.Application.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterInfrastructure(configuration);
        services.AddScoped<IConfigAppService, ConfigAppService>();
        services.AddScoped<IAdminSysAppService, AdminSysAppService>();
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IPessoaAppService, PessoaAppService>();
        services.AddScoped<IFilaAppService, FilaAppService>();
        services.AddScoped<IIdentityService, IdentitysService>();
        services.AddScoped<IIdentityManagerService, IdentityManagerService>();

        return services;
    }
}
