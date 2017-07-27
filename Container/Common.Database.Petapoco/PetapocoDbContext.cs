namespace Common.Database.Petapoco
{
    using Common.Database;
    using Microsoft.CSharp.RuntimeBinder;
    using PetaPoco;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;
    using Common.Database.SqlLoader;

    public class PetapocoDbContext : BaseDbCommand, IDisposable
    {
        private Database db;

        public PetapocoDbContext(List<IFileLoader> sqlLoader, ConnectionDescriptor connection) : base(sqlLoader, connection)
        {
            this.db = new Database(connection.ConnectionString, connection.ProviderName);
            this.db.OpenSharedConnection();
            this.db.EnableAutoSelect = false;
            this.db.EnableNamedParams = true;
            this.db.CommandTimeout = 30; 
        }

        public override void Close()
        {
            if (this.db != null)
            {
                this.db.CloseSharedConnection(); 
            }
        }

        public void Commit()
        {
            this.db.CompleteTransaction();
        }

        public override void Dispose()
        {
            base.Dispose();
            if (this.db != null)
            {
                this.db.Dispose();
            }
        }

        public override int Execute(string sql, params object[] args)
        {
            sql = base.GetSql(sql);
            return this.db.Execute(sql, args);
        }

        public override T ExecuteScalar<T>(string sql, params object[] args)
        {
            sql = base.GetSql(sql);
            return this.db.ExecuteScalar<T>(sql, args);
        }

        public override void Open()
        {
            if (this.db != null)
            {
                this.db.OpenSharedConnection();
            }
        }

        public override IEnumerable<T> AsEnumerable<T>(string sql, dynamic args)
        {
            sql = base.GetSql(sql);
            return this.db.Query<T>(sql, args);
        }

        public override List<T> Query<T>(long page, long pageSize, string sql, params object[] args)
        {
            sql = base.GetSql(sql); 
            return this.db.Fetch<T>(page, pageSize, sql, args);
        }

        public override List<T> Query<T>(string sql, params object[] args)
        {
            sql = base.GetSql(sql);
            
            return this.db.Fetch<T>(sql, args);
        }

        public override List<T> Query<T>(string sql, dynamic args)
        {
            sql = base.GetSql(sql);
            return this.db.Fetch<T>(sql, args);
        }

        public override IEnumerable<T> AsEnumerable<T>(string sql)
        {
            sql = base.GetSql(sql);
            return this.db.Query<T>(sql);
        }

        public void Rollback()
        {
            this.db.AbortTransaction();
        }
         
        public override int CommandTimeout
        {
            get
            {
                return db.CommandTimeout;
            }
            set
            {
                db.CommandTimeout = value;
            }
        } 
        
    }
}

