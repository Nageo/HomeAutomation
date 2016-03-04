using Devkoes.Restup.WebServer.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Controller.Web
{
    public class Response
    {
        public Exception Exception { get; set; }

        internal GetResponse GetAsResponse()
        {
            return new GetResponse(GetResponse.ResponseStatus.OK, this);
        }
    }
}
