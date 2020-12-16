using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SimpleCryptoChat.Models.Startup))]

namespace SimpleCryptoChat.Models
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
                app.MapSignalR();
        }
    }
}
