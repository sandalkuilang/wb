namespace Common.Database
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConnectionDescriptor
    {
        public string ConnectionString { get; set; } 
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
         
        public ConnectionDescriptor() : this(string.Empty, string.Empty, string.Empty)
        {
        }

        public ConnectionDescriptor(string name, string connectionString, string providerName)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
            this.IsDefault = true;
        }

        public ConnectionDescriptor(string name, string connectionString, string providerName, bool isDefault)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
        }
         
    }
}

