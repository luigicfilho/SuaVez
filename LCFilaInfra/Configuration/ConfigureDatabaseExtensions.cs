using LCFilaInfra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LCFilaInfra.Configuration;

public static class ConfigureDatabaseExtensions
{
    public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, IConfiguration configuration)
    {
        var dbtype = configuration.GetSection("ConnectionStrings:Databasetype");
        switch (dbtype.Value)
        {
            case "sqlite":
                builder.UseSqlite(configuration.GetConnectionString("SqliteCS"), opt =>
                {
                    opt.CommandTimeout((int)TimeSpan.FromSeconds(60).TotalSeconds);
                    opt.MigrationsAssembly("LCFila.Infra");
                });
                break;
            case "sqlserver":
                builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!, opt =>
                {
                    opt.MigrationsAssembly("LCFila.Infra");
                });
                break;
            default:
                builder.UseInMemoryDatabase($"data-{Guid.NewGuid()}");
                break;
        }

        return builder;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FilaDbContext>(options =>
        {
            options.UseDatabase(configuration);
        });

        return services;
    }
}
