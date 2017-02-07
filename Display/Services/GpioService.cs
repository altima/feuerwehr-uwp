using Display.Events;
using Prism.Events;
using Windows.Devices.Gpio;

namespace Display.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGpioService
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Init();

        /// <summary>
        /// Ons this instance.
        /// </summary>
        void On();

        /// <summary>
        /// Offs this instance.
        /// </summary>
        void Off();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Display.Services.IGpioService" />
    public class GpioService : IGpioService
    {
        private IEventAggregator _eventAggregator;
        private GpioController _controller;
        private GpioPin _pin;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public GpioService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AlarmEvents.On>().Unsubscribe(On);
            _eventAggregator.GetEvent<AlarmEvents.Off>().Unsubscribe(Off);

            _eventAggregator.GetEvent<AlarmEvents.On>().Subscribe(On);
            _eventAggregator.GetEvent<AlarmEvents.Off>().Subscribe(Off);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Init()
        {
            _controller = GpioController.GetDefault();
            if (!IsControllerAvailable())
            {
                return;
            }
            _pin = _controller.OpenPin(Constants.GpioPort);
            _pin.Write(GpioPinValue.High);
            _pin.SetDriveMode(GpioPinDriveMode.Output);
        }

        /// <summary>
        /// Determines whether [is controller available].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is controller available]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsControllerAvailable()
        {
            return _controller != null;
        }

        /// <summary>
        /// Offs this instance.
        /// </summary>
        public void Off()
        {
            if (IsControllerAvailable())
            {
                _pin.Write(GpioPinValue.High);
            }
        }

        /// <summary>
        /// Ons this instance.
        /// </summary>
        public void On()
        {
            if (IsControllerAvailable())
            {
                _pin.Write(GpioPinValue.Low);
            }
        }
    }
}
