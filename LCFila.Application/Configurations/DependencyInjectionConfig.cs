using LCFila.Application.Interfaces.Identity;
using LCFila.Application.AppServices;
using LCFila.Application.Interfaces;
using LCFila.Infra.Context;
using LCFila.Infra.Interfaces;
using LCFila.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LCFila.Application.IdentityService;

namespace LCFila.Application.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<FilaDbContext>();
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<IFilaRepository, FilaRepository>();
        services.AddScoped<IEmpresaLoginRepository, EmpresaLoginRepository>();
        services.AddScoped<IEmpresaConfiguracaoRepository, EmpresaConfiguracaoRepository>();
        services.AddScoped<IConfigAppService, ConfigAppService>();
        services.AddScoped<IAdminSysAppService, AdminSysAppService>();
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IPessoaAppService, PessoaAppService>();
        services.AddScoped<IFilaAppService, FilaAppService>();
        services.AddScoped<IIdentityService, IdentitysService>();
        services.AddScoped<IIdentityManagerService, IdentityManagerService>();

        services.AddTransient<IEmailSender, EmailSender>(i =>
            new EmailSender(
                configuration["EmailSender:Host"]!,
                configuration.GetValue<int>("EmailSender:Port"),
                configuration.GetValue<bool>("EmailSender:EnableSSL"),
                configuration["EmailSender:UserName"]!,
                configuration["EmailSender:Password"]!
            )
        );

        return services;
    }
}
