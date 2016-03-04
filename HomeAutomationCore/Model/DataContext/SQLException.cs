using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationCore.Model.DataContext
{
    public class SQLException : Exception
    {
        public SQLException(string Message, string SQLException)
            : base(string.Format("{0} {1}", Message, SQLException))
        {

        }
    }
}
