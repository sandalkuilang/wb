namespace Common.Database.SqlLoader
{
    using System;

    public interface IFileLoader
    {
        string GetName();
        string Load(string name);
        void SetName(string name);
    }
}

