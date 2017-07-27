/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Database;

namespace WebPlatform
{
    public class DatabaseManager : IDbManager
    {  
        public static IDbManager GetInstance
        {
            get
            {
                return SessionPool.Instance.Resolve<IDbManager>();
            }
        }

        public void AddConnection(string name, bool isDefault, ConnectionDescriptor connectionDescriptor)
        {
            GetInstance.AddConnection(name, isDefault, connectionDescriptor);
        }

        public void AddSqlLoader(Common.Database.SqlLoader.IFileLoader sqlLoader)
        {
            GetInstance.AddSqlLoader(sqlLoader);
        }

        public List<ConnectionDescriptor> ConnectionDescriptor
        {
            get
            {
                return GetInstance.ConnectionDescriptor;
            }
            set
            {
                GetInstance.ConnectionDescriptor = value;
            }
        }

        public IDataCommand GetDatabase(string name)
        {
            return GetInstance.GetDatabase(name);
        }

        public void RemoveConnection(string name)
        {
            GetInstance.RemoveConnection(name);
        }

        public void RemoveSqlLoader(string name)
        {
            GetInstance.RemoveSqlLoader(name);
        }

        public List<Common.Database.SqlLoader.IFileLoader> SqlLoader
        {
            get
            {
                return GetInstance.SqlLoader;
            }
            set
            {
                GetInstance.SqlLoader = value;
            }
        }

    }
}