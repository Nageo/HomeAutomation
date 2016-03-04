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
    [Table("Room")]
    public class Room : TableBase
    {
        public static AsyncTableQuery<Room> Table { get { return Context.Instance.Connection.Table<Room>(); } }
    }
}
