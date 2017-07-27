namespace Common.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;

    public interface IDataCommand
    {
        void Close();
        int Execute(string sql, params object[] args);
        T ExecuteScalar<T>(string sql, params object[] args);
        void Open();
        IEnumerable<T> AsEnumerable<T>(string sql);
        IEnumerable<T> AsEnumerable<T>(string sql, dynamic args);
        List<T> Query<T>(string sql, dynamic args);
        List<T> Query<T>(string sql, params object[] args);
        List<T> Query<T>(long page, long pageSize, string sql, params object[] args); 
        int CommandTimeout { get; set; } 
        IDbTransaction Transaction { get; set; }
    }
}

