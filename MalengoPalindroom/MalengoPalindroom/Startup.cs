using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MalengoPalindroom.Startup))]

namespace MalengoPalindroom
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
