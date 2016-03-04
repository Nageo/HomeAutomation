using Devkoes.Restup.WebServer.File;
using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;

namespace HomeAutomationCore
{
    public static class Server
    {
        public static int Port { get; set; } = 80;
        public static HttpServer HttpServer { get; private set; }

        public static async Task Run()
        {
            // initialize sqlite context
            await Model.Context.Initialize();

            // startup server
            await ThreadPool.RunAsync(async workItem => {
                Log.i("Init Restup-Server");
                HttpServer = new HttpServer(Port);
                try
                {
                    // initialize webserver
                    var restRouteHandler = new RestRouteHandler();

                    restRouteHandler.RegisterController<Controller.Home.Home>();
                    restRouteHandler.RegisterController<Controller.Web.Web>();
                    restRouteHandler.RegisterController<Controller.PhilipsHUE.Main>();

                    HttpServer.RegisterRoute("api", restRouteHandler);
                    HttpServer.RegisterRoute(new StaticFileRouteHandler(@"RemoteWebApp\Web"));

                    Log.i($"Init Restup-Server done, starting on Port {Port}");
                    // start webserver
                    await HttpServer.StartServerAsync();
                    Log.i($"Restup-Server running on Port {Port}");
                }
                catch (Exception ex)
                {
                    Log.e(ex);
                    HttpServer.StopServer();
                }

            }, WorkItemPriority.High);
        }
    }
}
