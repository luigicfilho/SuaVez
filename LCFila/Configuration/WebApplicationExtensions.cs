using LCFila.Application.Interfaces.Configuration;

namespace LCFila.Web.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder builder)
    {
        // Cached for reflection performance 
        var applicationAssembly = typeof(ILCFilaConfigurator).Assembly;
        var allApplicationTypes = applicationAssembly.GetTypes();

        var componentType = allApplicationTypes
            .SingleOrDefault(t => typeof(ILCFilaConfigurator).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        if (componentType != null) // Important to check if componentType is found
        {
            builder.Services.AddScoped(typeof(ILCFilaConfigurator), componentType);
            //builder.Services.AddScoped(typeof(ILCFilaConfigurator), sp =>
            //{
            //    // Use Activator.CreateInstance to create an instance:
            //    object? implementation = Activator.CreateInstance(componentType!);
            //    if (implementation is not null)
            //    {
            //        return implementation;
            //    }
            //    throw new InvalidOperationException($"Cannot create instance of {componentType}");
            //    // Or, if your type has constructor dependencies:
            //    // return Activator.CreateInstance(componentType, /* your dependencies here */);
            //});
        }
        else
        {
            // Handle the case where the type is not found (log an error, throw an exception, etc.)
            throw new InvalidOperationException("ILCFilaConfigurator implementation not found.");
        }

        var initializerType = allApplicationTypes
            .SingleOrDefault(t => typeof(ILCFilaInitializer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        if (initializerType != null)
        {
            builder.Services.AddScoped(typeof(ILCFilaInitializer), initializerType);
            //builder.Services.AddScoped(typeof(ILCFilaInitializer), sp =>
            //{
            //    object? implementation = Activator.CreateInstance(initializerType!);
            //    if (implementation is not null)
            //    {
            //        return implementation;
            //    }
            //    throw new InvalidOperationException($"Cannot create instance of {initializerType}");
            //});
        }
        else
        {
            throw new InvalidOperationException("ILCFilaInitializer implementation not found.");
        }

        // Resolve the configurator
        using var scope = builder.Services.BuildServiceProvider().CreateScope();
        var configurator = scope.ServiceProvider.GetRequiredService<ILCFilaConfigurator>();
        configurator.ConfigureServices(builder.Services, builder.Configuration);

        return builder;
    }

    public static WebApplication InitializeApplication(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ILCFilaInitializer>();
        initializer.Initialize(app);
        return app;
    }
}
