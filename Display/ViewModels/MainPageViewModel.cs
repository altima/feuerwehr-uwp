using System.Collections.Generic;
using AlarmDisplay.Services;
using Prism.Events;
using Prism.Windows.Navigation;

namespace AlarmDisplay.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AlarmDisplay.ViewModels.CommonPageViewModel" />
    public class MainPageViewModel : CommonPageViewModel
    {
        IEventAggregator _eventAggregator;
        IHttpdService _httpdService;
        IGpioService _gpioService;
        IAlarmTimerService _alarmService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        public MainPageViewModel(IEventAggregator eventAggregator, IHttpdService httpdService, IGpioService gpioService, IAlarmTimerService alarmService)
        {
            _eventAggregator = eventAggregator;
            _httpdService = httpdService;
            _gpioService = gpioService;
            _alarmService = alarmService;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            _gpioService.Init();
            _httpdService.Start();
            _alarmService.Start();
        }
    }
}
