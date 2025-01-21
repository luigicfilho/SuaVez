using System;
using System.Threading.Tasks;
using LCFila.Configurations;
using LCFilaApplication.Context;
using LCFilaApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddDbContext<FilaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// builder.Services.AddFeatureManagement();
builder.Services.AddScopedFeatureManagement(builder.Configuration.GetSection("LCFilaFeatures"));

builder.Services.Configure<FeatureManagementOptions>(options =>
{
    options.IgnoreMissingFeatureFilters = true;
    options.IgnoreMissingFeatures = true;
});

builder.Services.AddMvcConfiguration();

builder.Services.ResolveDependencies(builder.Configuration);

var serviceProvider = builder.Services.BuildServiceProvider();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseDeveloperExceptionPage();

    CreateRoles(serviceProvider).Wait();
    CreateAdmin(serviceProvider).Wait();
}
else
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalizationConfig();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();

async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] rolesNames = { "SysAdmin", "EmpAdmin", "OperatorEmp", "User" };
    IdentityResult result;
    foreach (var namesRole in rolesNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(namesRole);
        if (!roleExist)
        {
            result = await roleManager.CreateAsync(new IdentityRole(namesRole));
        }
    }
}

async Task CreateAdmin(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    var finduser = await userManager.FindByEmailAsync("admin@suavez.com.br");
    if (finduser == null)
    {
        var user = new AppUser { UserName = "admin@suavez.com.br", Email = "admin@suavez.com.br" };
        var password = "Abc.123!";
        user.EmailConfirmed = true;
        user.LockoutEnabled = false;
        var result = await userManager.CreateAsync(user, password);
        var roles = await userManager.AddToRoleAsync(user, "SysAdmin");
    }
}