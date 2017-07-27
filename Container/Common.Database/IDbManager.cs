namespace Common.Database
{
    using Common.Database.SqlLoader;
    using System;
    using System.Collections.Generic;

    public interface IDbManager
    {
        void AddConnection(string name, bool isDefault, Common.Database.ConnectionDescriptor connectionDescriptor);
        void AddSqlLoader(IFileLoader sqlLoader);
        IDataCommand GetDatabase(string name);
        void RemoveConnection(string name);
        void RemoveSqlLoader(string name); 
        List<Common.Database.ConnectionDescriptor> ConnectionDescriptor { get; set; } 
        List<IFileLoader> SqlLoader { get; set; }
    }
}

