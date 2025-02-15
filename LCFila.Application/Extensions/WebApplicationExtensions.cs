using LCFila.Application.Configurations;
using LCFila.Domain.Models;
using LCFila.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LCFila.Application.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseLCFila(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseDeveloperExceptionPage();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                FilaDbContext context = services.GetRequiredService<FilaDbContext>();
                
                var migrated = context.MigrateDatabase();
                if (migrated)
                {
                    //DataSeeder.Seed(context);
                    CreateRoles(services).Wait();
                    CreateAdmin(services).Wait();
                }
            }

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

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }

    static async Task CreateRoles(IServiceProvider serviceProvider)
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

    static async Task CreateAdmin(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var finduser = await userManager.FindByEmailAsync("admin@suavez.com.br");
        var dbcontext = serviceProvider.GetRequiredService<FilaDbContext>();
        var configuracaoEmpresa = new EmpresaConfiguracao()
        {
            CorSegundariaEmpresa = "",
            CorPrincipalEmpresa = "",
            FooterEmpresa = "",
            LinkLogodaEmpresa = "http://",
            NomeDaEmpresa = "System"
        };
        var emplogin = new EmpresaLogin()
        {
            NomeEmpresa = "System",
            Ativo = true,
            CNPJ = "12323123213123",
            EmpresaConfiguracao = configuracaoEmpresa,
            EmpresaFilas = [],
            UsersEmpresa = []
        };
        var user = new AppUser { Id = Guid.NewGuid().ToString(), UserName = "admin@suavez.com.br", Email = "admin@suavez.com.br" };
        emplogin.IdAdminEmpresa = Guid.Parse(user.Id);
        var emploginsystem = dbcontext.EmpresasLogin.Select(s => s.NomeEmpresa == emplogin.NomeEmpresa);
        if (emploginsystem == null)
        {
            var aa = dbcontext.EmpresasLogin.Add(emplogin);
            var xx = dbcontext.SaveChanges();
        }

        if (finduser == null)
        {

            var password = "Abc@123";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            var result = await userManager.CreateAsync(user, password);
            var roles = await userManager.AddToRoleAsync(user, "SysAdmin");
        }
    }
}
