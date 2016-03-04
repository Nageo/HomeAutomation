using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationCore.Controller.Web
{
    /// <summary>
    /// Web Controller, for basic web data requests
    /// </summary>
    [RestController(InstanceCreationType.Singleton)]
    internal class Web
    {
        [UriFormat("/Web/GetData")]
        public GetResponse GetData()
        {
            Response res = new Response(); 

            try
            {
                throw new NotImplementedException();
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
