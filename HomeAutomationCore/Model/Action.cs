using HomeAutomationCore.Model.DataContext;
using SQLite.Net.Async;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationCore.Model
{
    [Table("Action")]
    public class Action : TableBase
    {
        public static AsyncTableQuery<Action> Table { get { return Context.Instance.Connection.Table<Action>(); } }
    }
}
