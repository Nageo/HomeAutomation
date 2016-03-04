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
    [Table("Category")]
    public class Category : TableBase
    {
        public static AsyncTableQuery<Category> Table { get { return Context.Instance.Connection.Table<Category>(); } }
    }
}
