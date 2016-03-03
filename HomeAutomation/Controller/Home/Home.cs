using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace HomeAutomation.Controller.Home
{
    [RestController(InstanceCreationType.Singleton)]
    internal class Home
    {
        ulong calls = 1;

        [UriFormat("/Home/Send433MHz/{code}/{active}")]
        public GetResponse Send433Mhz(string code, int active)
        {
            Response res = new Response(); res.CallsToAPI = calls++;

            try
            {
                o433Mhz.Send.Send433Mhz(23, code, active);
                //c433Mhz.Send.Send433Mhz(23, code, active);
            }
            catch (Exception ex)
            {
                res.Exception = ex;
                Log.e(ex);
            }

            return res.GetAsResponse();
        }
    }
}
