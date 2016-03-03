using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Devkoes.Restup.WebServer;
using Devkoes.Restup.WebServer.Rest;
using Devkoes.Restup.WebServer.Http;
using Windows.System.Threading;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace HomeAutomation
{
    public sealed class StartupTask : IBackgroundTask
    {
        internal static BackgroundTaskDeferral Deferral = null;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //

            Deferral = taskInstance.GetDeferral();

            await ThreadPool.RunAsync(async workItem => {

                RestWebServer restWebServer = new RestWebServer(80);
                try
                {
                    // initialize webserver
                    restWebServer.RegisterController<Controller.Home.Home>();
                    restWebServer.RegisterController<Controller.PhilipsHUE.Main>();
                    await restWebServer.StartServerAsync();
                }
                catch (Exception ex)
                {
                    Log.e(ex);
                    restWebServer.StopServer();
                    Deferral.Complete();
                }

            }, WorkItemPriority.High);
        }
    }
}
