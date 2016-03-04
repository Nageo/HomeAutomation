using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationHeaded
{
    public class ViewModel
    {
        
        public string HomeUrl
        {
            get;
            set;
        } = $"http://127.0.0.1:{HomeAutomationCore.Server.Port}/";
    }
}
