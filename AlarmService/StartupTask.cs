using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Restup.Webserver.Http;
using Restup.Webserver.Rest;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace AlarmService
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        HttpServer _server;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<Controller.AsyncGpioController>();

            var serverConfig = new HttpServerConfiguration()
                .ListenOnPort(18080)
                .RegisterRoute("api", restRouteHandler)
                .EnableCors();
            var server = new HttpServer(serverConfig);
            _server = server;
            await _server.StartServerAsync();
        }
    }
}
