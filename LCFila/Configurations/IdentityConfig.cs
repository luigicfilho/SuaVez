using LCFila.Extensions;
using LCFilaApplication.Context;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LCFila.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<FilaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true )
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<AppErrorDescriber>()
                .AddEntityFrameworkStores<FilaDbContext>();

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
}
