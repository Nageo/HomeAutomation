using Devkoes.Restup.WebServer.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationCore.Controller.Home
{
    internal class Response
    {
        public Exception Exception { get; set; }

        public ulong CallsToAPI { get; set; }

        internal GetResponse GetAsResponse()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, Newtonsoft.Json.JsonConvert.SerializeObject(this));
        }
    }
}
