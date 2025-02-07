using LCAppFila.Domain.Notificacoes;
using LCFilaApplication.AppServices;
using LCFilaApplication.Interfaces;
using LCFilaApplication.MVC;
using LCFilaApplication.Repository;
using LCFilaApplication.Services;
using LCFilaInfra.Context;
using LCFilaInfra.Interfaces;
using LCFilaInfra.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LCFilaApplication.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<FilaDbContext>();
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<IFilaRepository, FilaRepository>();
        services.AddScoped<IFilaPessoaRepository, FilaPessoaRepository>();
        services.AddScoped<IEmpresaLoginRepository, EmpresaLoginRepository>();
        services.AddScoped<IEmpresaConfiguracaoRepository, EmpresaConfiguracaoRepository>();
        services.AddScoped<IConfigAppService, ConfigAppService>();
        services.AddScoped<IAdminSysAppService, AdminSysAppService>();
        services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

        services.AddTransient<IEmailSender, EmailSender>(i =>
            new EmailSender(
                configuration["EmailSender:Host"]!,
                configuration.GetValue<int>("EmailSender:Port"),
                configuration.GetValue<bool>("EmailSender:EnableSSL"),
                configuration["EmailSender:UserName"]!,
                configuration["EmailSender:Password"]!
            )
        );

        services.AddScoped<INotificador, Notificador>();

        return services;
    }
}
