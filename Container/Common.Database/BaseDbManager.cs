namespace Common.Database
{
    using Common.Database.SqlLoader;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;

    public abstract class BaseDbManager : IDbManager
    { 
        public List<Common.Database.ConnectionDescriptor> ConnectionDescriptor { get; set; }
        public List<IFileLoader> SqlLoader { get; set; }

        public BaseDbManager(List<IFileLoader> sqlLoader, List<Common.Database.ConnectionDescriptor> connectionDescriptor)
        {
            this.SqlLoader = new List<IFileLoader>();
            this.ConnectionDescriptor = new List<Common.Database.ConnectionDescriptor>();
            if (sqlLoader != null)
            {
                this.SqlLoader.AddRange(sqlLoader); 
            }
            if (connectionDescriptor != null)
            {
                this.ConnectionDescriptor.AddRange(connectionDescriptor); 
            }
        }

        public void AddConnection(string name, bool isDefault, Common.Database.ConnectionDescriptor connectionDescriptor)
        {
            if (this.GetConnection(name) == null)
            {
                throw new DuplicateNameException("Connection Descriptor is already exists.");
            }
            Common.Database.ConnectionDescriptor item = new Common.Database.ConnectionDescriptor();
            item.Name = name;
            item.IsDefault = isDefault;
            item.ConnectionString = connectionDescriptor.ConnectionString;
            item.ProviderName = connectionDescriptor.ProviderName;
            this.ConnectionDescriptor.Add(item);
        }

        public void AddSqlLoader(IFileLoader sqlLoader)
        {
            if (this.GetSqlLoader(sqlLoader.GetName()) != null)
            {
                throw new DuplicateNameException("Sql Loader is already exists.");
            }
            this.SqlLoader.Add(sqlLoader);
        }

        protected Common.Database.ConnectionDescriptor GetConnection(string name)
        {
            string str = name.ToLower();
            for (int i = 0; i < this.ConnectionDescriptor.Count; i++)
            {
                if (this.ConnectionDescriptor[i].Name.ToLower() == str)
                {
                    return this.ConnectionDescriptor[i];
                }
            }
            return null;
        }

        public abstract IDataCommand GetDatabase(string name);
        protected IFileLoader GetSqlLoader(string name)
        {
            string str = name.ToLower();
            for (int i = 0; i < this.SqlLoader.Count; i++)
            {
                if (this.SqlLoader[i].GetName().ToLower() == str)
                {
                    return this.SqlLoader[i];
                }
            }
            return null;
        }

        public void RemoveConnection(string name)
        {
            Common.Database.ConnectionDescriptor connection = this.GetConnection(name);
            if (connection != null)
            {
                this.ConnectionDescriptor.Remove(connection);
            }
        }

        public void RemoveSqlLoader(string name)
        {
            IFileLoader sqlLoader = this.GetSqlLoader(name);
            if (sqlLoader != null)
            {
                this.SqlLoader.Remove(sqlLoader);
            }
        }

        protected void SetDefaultConnection(string name)
        {
            Common.Database.ConnectionDescriptor connection = this.GetConnection(name);
            if (connection != null)
            {
                connection.IsDefault = true;
                string str = name.ToLower();
                for (int i = 0; i < this.ConnectionDescriptor.Count; i++)
                {
                    if (this.ConnectionDescriptor[i].Name.ToLower() == str)
                    {
                        this.ConnectionDescriptor[i].IsDefault = false;
                    }
                }
            }
        } 
         
    }
}

