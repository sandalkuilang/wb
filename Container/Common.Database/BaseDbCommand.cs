namespace Common.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using Common.Database.SqlLoader;
    using System.Text;

    public abstract class BaseDbCommand : IDataCommand, IDbConnection, IDisposable
    {

        protected const string SQL_NOT_FOUND = "{0} Cannot be found.";

        public IDbConnection Connection {get;set;}
        public List<IFileLoader>  SqlLoader {get;set;}
        public IDbTransaction Transaction {get;set;}

        public BaseDbCommand(List<IFileLoader> sqlLoader, ConnectionDescriptor connectionDescriptor)
        {
            this.SqlLoader = new List<IFileLoader>();
            this.SqlLoader.AddRange(sqlLoader);
            this.Connection = DbProviderFactories.GetFactory(connectionDescriptor.ProviderName).CreateConnection();
            this.Connection.ConnectionString = connectionDescriptor.ConnectionString;
            this.Connection.Open();
        }

        public BaseDbCommand(List<IFileLoader> sqlLoader, IDbConnection connection)
        {
            this.SqlLoader = new List<IFileLoader>();
            this.SqlLoader.AddRange(sqlLoader);
        }

        public IDbTransaction BeginTransaction()
        {
            return this.Connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return this.Connection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            this.Connection.ChangeDatabase(databaseName);
        }

        public abstract void Close();
        public IDbCommand CreateCommand()
        {
            return this.Connection.CreateCommand();
        }

        public virtual void Dispose()
        {
            if (this.Connection != null)
            {
                this.Connection.Dispose();
            }
        }

        public abstract int Execute(string sql, params object[] args);
        public abstract T ExecuteScalar<T>(string sql, params object[] args);
        public string GetSql(string sql)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < this.SqlLoader.Count; i++)
            {
                str.AppendLine(this.SqlLoader[i].Load(sql));
                if (str.Length > 10)
                {
                    break;
                }
            }
            if (str.Length < 10)
            {
                str.AppendLine(sql);
            }
            return str.ToString();
        }

        public abstract void Open();
        public abstract List<T> Query<T>(long page, long pageSize, string sql, params object[] args);
        public abstract List<T> Query<T>(string sql, params object[] args);
        public abstract List<T> Query<T>(string sql, dynamic args);

        public abstract IEnumerable<T> AsEnumerable<T>(string sql, dynamic args);
        public abstract IEnumerable<T> AsEnumerable<T>(string sql);

        public abstract int CommandTimeout { get; set; }
         
        public string ConnectionString
        {
            get
            {
                return this.Connection.ConnectionString;
            }
            set
            {
                this.Connection.ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                return this.Connection.ConnectionTimeout;
            }
        }

        public string Database
        {
            get
            {
                return this.Connection.Database;
            }
        }
         
        public ConnectionState State
        {
            get
            {
                return this.Connection.State;
            }
        } 
    }
}

