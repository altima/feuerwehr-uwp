using AlarmDisplay.Events;
using AlarmDisplay.Models;
using Prism.Events;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using Windows.UI.Core;

namespace AlarmDisplay.RestControllers
{
    /// <summary>
    /// 
    /// </summary>
    [RestController(InstanceCreationType.Singleton)]
    public class AlarmController
    {
        IEventAggregator _eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmController"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public AlarmController(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Ons this instance.
        /// </summary>
        /// <returns></returns>
        [UriFormat("/on")]
        public IGetResponse On()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => { _eventAggregator.GetEvent<AlarmEvents.On>().Publish(); });
            return new GetResponse(GetResponse.ResponseStatus.OK, new ResponseData(200, "ok"));
        }

        /// <summary>
        /// Offs this instance.
        /// </summary>
        /// <returns></returns>
        [UriFormat("/off")]
        public IGetResponse Off()
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => { _eventAggregator.GetEvent<AlarmEvents.Off>().Publish(); });
            return new GetResponse(GetResponse.ResponseStatus.OK, new ResponseData(200, "ok"));
        }
    }
}
