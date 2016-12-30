using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using Windows.Devices.Gpio;
using System.Threading;

namespace AlarmService.Controller
{
    [RestController(InstanceCreationType.PerCall)]
    public sealed class AsyncGpioController
    {
        MyGpio _myGpio;
        
        public AsyncGpioController()
        {
            _myGpio = MyGpio.Instance;
        }        

        [UriFormat("/on")]
        public IGetResponse on()
        {
            _myGpio.On();
            return new GetResponse(GetResponse.ResponseStatus.OK, "Hallo Welt");
        }

        [UriFormat("/off")]
        public IGetResponse off()
        {
            _myGpio.Off();
            return new GetResponse(GetResponse.ResponseStatus.OK, "Hallo Welt");
        }
    }
}
