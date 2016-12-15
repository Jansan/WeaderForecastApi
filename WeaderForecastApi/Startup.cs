using System;
using System.Threading.Tasks;
using Microsoft.Owin.Cors;
using Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(WeaderForecastApi.Startup))]

namespace WeaderForecastApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };

                map.RunSignalR(hubConfiguration);

            });
        }
    }
}
