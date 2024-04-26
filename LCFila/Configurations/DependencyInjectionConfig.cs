using LCAppFila.Domain.Notificacoes;
using LCFila.Extensions;
using LCFilaApplication.Context;
using LCFilaApplication.Interfaces;
using LCFilaApplication.Repository;
using LCFilaApplication.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LCFila.Configurations
{
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
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    configuration["EmailSender:Host"],
                    configuration.GetValue<int>("EmailSender:Port"),
                    configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    configuration["EmailSender:UserName"],
                    configuration["EmailSender:Password"]
                )
            );

            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
