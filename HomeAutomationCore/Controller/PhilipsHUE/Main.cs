using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Windows.Devices.Gpio;

namespace HomeAutomationCore.Controller.PhilipsHUE
{
    public struct TestResponse {
        public int Calls;
    }

    [RestController(InstanceCreationType.Singleton)]
    internal class Main
    {
        int calls = 1;

        [UriFormat("/test")]
        public GetResponse Test()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, new TestResponse() { Calls = calls++ });
        }
    }
}
