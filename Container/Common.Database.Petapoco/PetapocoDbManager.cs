namespace Common.Database.Petapoco
{
    using Common.Database;
    using System;
    using System.Collections.Generic;
    using Common.Database.SqlLoader;

    public class PetapocoDbManager : BaseDbManager
    {
        public PetapocoDbManager(List<IFileLoader> sqlLoader, List<ConnectionDescriptor> connectionDescriptor) : base(sqlLoader, connectionDescriptor)
        {
        }

        public override IDataCommand GetDatabase(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                ConnectionDescriptor connection = base.GetConnection(name);
                if (connection != null)
                {
                    return new PetapocoDbContext(base.SqlLoader, connection);
                }
            }
            return null;
        }
    }
}

