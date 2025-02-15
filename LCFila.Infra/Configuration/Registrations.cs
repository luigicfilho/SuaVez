using LCFila.Infra.Context;
using LCFila.Infra.External;
using LCFila.Infra.Interfaces;
using LCFila.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LCFila.Infra.Configuration;

public static class Registrations
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<FilaDbContext>();
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<IFilaRepository, FilaRepository>();
        services.AddScoped<IEmpresaLoginRepository, EmpresaLoginRepository>();
        services.AddScoped<IEmpresaConfiguracaoRepository, EmpresaConfiguracaoRepository>();


        services.AddTransient<IEmailSender, EmailSender>(i =>
            new EmailSender(
                configuration["EmailSender:Host"]!,
                int.Parse(configuration["EmailSender:Port"]!),
                bool.Parse(configuration["EmailSender:EnableSSL"]!),
                configuration["EmailSender:UserName"]!,
                configuration["EmailSender:Password"]!
            )
        );
        return services;
    }
}