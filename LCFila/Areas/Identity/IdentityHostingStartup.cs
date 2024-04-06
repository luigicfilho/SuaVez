using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LCFila.Areas.Identity.IdentityHostingStartup))]
namespace LCFila.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}