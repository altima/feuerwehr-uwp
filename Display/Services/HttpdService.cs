using Display.RestControllers;
using Prism.Events;
using Restup.Webserver.Http;
using Restup.Webserver.Rest;
using System.Collections.Generic;

namespace Display.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Display.Services.IService" />
    public interface IHttpdService : IService { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Display.Services.IHttpdService" />
    public class HttpdService : IHttpdService
    {
        IEventAggregator _eventAggregator;
        IDnssdService _dnssdService;
        HttpServer _server;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpdService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="dnssdService">The DNSSD service.</param>
        public HttpdService(IEventAggregator eventAggregator, IDnssdService dnssdService)
        {
            _eventAggregator = eventAggregator;
            _dnssdService = dnssdService;
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart()
        {
            Stop();
            Start();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public async void Start()
        {
            if (_server == null)
            {
                var handler = new RestRouteHandler();
                handler.RegisterController<AlarmController>(_eventAggregator);

                var config = new HttpServerConfiguration()
                    .ListenOnPort(Constants.HttpdPort)
                    .RegisterRoute("/alarm", handler)
                    .EnableCors();

                _server = new HttpServer(config);
                await _server.StartServerAsync();
            }

            Dictionary<string, string> txt = new Dictionary<string, string>()
            {
                { "AlarmOn", "/alarm/on" },
                { "AlarmOff", "/alarm/off" }
            };

            await _dnssdService.Announce(Constants.HttpdDnssdName, Constants.HttpdPort, null);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (_server != null)
            {
                _server.StopServer();
                _server = null;
            }
        }
    }
}
