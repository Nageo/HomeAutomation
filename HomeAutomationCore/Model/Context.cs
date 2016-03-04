using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Async;
using SQLite.Net;
using HomeAutomationCore.Model.DataContext;

namespace HomeAutomationCore.Model
{
    /// <summary>
    /// Context for all tables
    /// </summary>
    public class Context : DataContext.DataContext
    {
        #region SINGLETON
        private static Context _Instance;
        public static Context Instance
        {
            get
            {
                if (_Instance == null)
                {
                    throw new InvalidOperationException("The Context needs to be async initialized first. Use await Initzialize()");
                }

                return _Instance;
            }
        }
        #endregion

        public override int Version
        {
            get { return 0; }
        }
        public override Task<int> LastVersion()
        {
            return DataContext.Versioning.DBVersionsContext.Instance.Get(DatabaseVersionKey);
        }
        public override string DatabaseVersionKey
        {
            get { return Folder.Name + DatabaseFile; }
        }
        public override string DatabaseFile
        {
            get { return "homeautomation.db"; }
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override async Task Reset()
        {
            try
            {
                await connection.DropTableAsync<Room>();
                await connection.DropTableAsync<Category>();
                await connection.DropTableAsync<Action>();

                await DataContext.Versioning.DBVersionsContext.Instance.Set(DatabaseVersionKey, Version);
            }
            catch (SQLiteException ex)
            {
                throw new SQLException("Reset failed.", ex.Message);
            }
        }

        protected override async Task UpgradeTables(SQLiteAsyncConnection c)
        {
            try
            {
                await Task.Delay(100);
            }
            catch (SQLiteException ex)
            {
                throw new SQLException("UpgradeTables failed.", ex.Message);
            }

        }
        protected override async Task CreateTables(SQLiteAsyncConnection c)
        {
            try
            {
                await connection.CreateTableAsync<Room>();
                await connection.CreateTableAsync<Category>();
                await connection.CreateTableAsync<Action>();

                await DataContext.Versioning.DBVersionsContext.Instance.Set(DatabaseVersionKey, Version);
            }
            catch (SQLiteException ex)
            {
                throw new SQLException("CreateTables failed.", ex.Message);
            }
        }

        public static async Task Initialize()
        {
            if (_Instance == null)
            {
                // init Database for database versioning first
                await DataContext.Versioning.DBVersionsContext.Instance.Init(Windows.Storage.ApplicationData.Current.LocalFolder);

                _Instance = new Context();
                await _Instance.Init(Windows.Storage.ApplicationData.Current.LocalFolder);
            }
        }
    }
}
