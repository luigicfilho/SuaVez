using LCFila.Application.Configurations;
using LCFila.Application.MVC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace LCFila.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureLCFila(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddControllersWithViews();

        services.AddIdentityConfiguration(configuration);

        // builder.Services.AddFeatureManagement();
        services.AddScopedFeatureManagement(configuration.GetSection("LCFilaFeatures"));

        services.Configure<FeatureManagementOptions>(options =>
        {
            options.IgnoreMissingFeatureFilters = true;
            options.IgnoreMissingFeatures = true;
        });

        services.AddMvcConfiguration();

        services.ResolveDependencies(configuration);

        //var serviceProvider = services.BuildServiceProvider();
        return services;
    }
}
