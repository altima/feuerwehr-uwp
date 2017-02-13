using AlarmDisplay.Events;
using Prism.Events;
using System;
using Windows.UI.Xaml;

namespace AlarmDisplay.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AlarmDisplay.Services.IService" />
    public interface IAlarmTimerService : IService { }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="AlarmDisplay.Services.IAlarmTimerService" />
    public class AlarmTimerService : IAlarmTimerService
    {
        IEventAggregator _eventAggregator;
        DispatcherTimer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmTimerService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public AlarmTimerService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AlarmEvents.On>().Unsubscribe(OnAlarm);
            _eventAggregator.GetEvent<AlarmEvents.On>().Subscribe(OnAlarm);
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
        public void Start()
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
#if !DEBUG
                _timer.Interval = TimeSpan.FromHours(1);
#else
                _timer.Interval = TimeSpan.FromMinutes(2);
#endif
                _timer.Tick += OnTick;
            }
        }

        /// <summary>
        /// Called when [tick].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void OnTick(object sender, object e)
        {
            Stop();
            _eventAggregator.GetEvent<AlarmEvents.Off>().Publish();            
        }

        /// <summary>
        /// Called when [alarm].
        /// </summary>
        private async void OnAlarm()
        {
            //await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, ()=>
            //{
                if (_timer.IsEnabled) _timer.Stop();
                _timer.Start();
            //});            
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Tick -= OnTick;
                if(_timer.IsEnabled) _timer.Stop();
                _timer = null;
            }
        }
    }
}
