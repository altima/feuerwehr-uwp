using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace AlarmService
{
    public sealed class MyGpio
    {
        private static MyGpio _instance;
        public static MyGpio Instance
        {
            get
            {
                if (_instance == null) _instance = new MyGpio();
                return _instance;
            }
        }

        private GpioController _controller;
        private GpioPin _pin;
        private GpioPinValue _value;

        public MyGpio()
        {
            Init();
        }

        private void Init()
        {
            _controller = GpioController.GetDefault();
            _pin = _controller.OpenPin(5);
            _value = GpioPinValue.High;
            _pin.Write(_value);
            _pin.SetDriveMode(GpioPinDriveMode.Output);
            
            _value = GpioPinValue.Low;
            _pin.Write(_value);
        }
        
        public void On()
        {
            if (_controller != null)
            {
                _value = GpioPinValue.High;
                _pin.Write(_value);
            }
        }

        public void Off()
        {
            if(_controller != null)
            {
                _value = GpioPinValue.Low;
                _pin.Write(_value);
            }
        }
    }
}
