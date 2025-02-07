using LCFilaApplication.Extensions;
using LCFilaApplication.Models;
using LCFilaInfra.Context;
using LCFilaInfra.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LCFilaApplication.Configurations;
public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.AddDatabase(configuration);

        //services.AddDbContext<FilaDbContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddAuthentication(o =>
        {
            o.DefaultScheme = IdentityConstants.ApplicationScheme;
            o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
       .AddIdentityCookies(o => { });

        services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddUserManager<UserManager<AppUser>>()
            .AddErrorDescriber<AppErrorDescriber>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<FilaDbContext>()
            .AddDefaultTokenProviders();

        //services.AddIdentityApiEndpoints<IdentityUser>()
        //                .AddEntityFrameworkStores<FilaDbContext>();

        services.Configure<IdentityOptions>(opts => {
            opts.User.RequireUniqueEmail = true;
            //opts.SignIn.RequireConfirmedEmail = true;
            //opts.Password.RequiredLength = 8;
            //opts.Password.RequireNonAlphanumeric = true;
            //opts.Password.RequireLowercase = false;
            //opts.Password.RequireUppercase = true;
            //opts.Password.RequireDigit = true;
        });

        return services;
    }
}
