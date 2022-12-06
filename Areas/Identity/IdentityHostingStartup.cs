using Co_Mute.Areas.Identity;
using Co_Mute.Areas.Identity.Pages;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace Co_Mute.Areas.Identity
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