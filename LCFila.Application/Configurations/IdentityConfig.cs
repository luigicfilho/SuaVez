using LCFila.Application.Extensions;
using LCFila.Domain.Models;
using LCFila.Infra.Context;
using LCFila.Infra.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LCFila.Application.Configurations;
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

        services.AddAuthentication(o =>
        {
            o.DefaultScheme = IdentityConstants.ApplicationScheme;
            o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
       .AddIdentityCookies(o => { });

        services.AddIdentityCore<AppUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.User.RequireUniqueEmail = true;
            //options.Password.RequiredLength = 8;
            //options.Password.RequireNonAlphanumeric = true;
            //options.Password.RequireLowercase = false;
            //options.Password.RequireUppercase = true;
            //options.Password.RequireDigit = true;
        }).AddRoles<IdentityRole>()
          .AddSignInManager<SignInManager<AppUser>>()
          .AddUserManager<UserManager<AppUser>>()
          .AddErrorDescriber<AppErrorDescriber>()
          .AddRoleManager<RoleManager<IdentityRole>>()
          .AddEntityFrameworkStores<FilaDbContext>()
          .AddDefaultTokenProviders();

        return services;
    }
}
