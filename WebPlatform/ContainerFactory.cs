using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Container;
using Common.Database;
using Common.Database.SqlLoader;
using WebPlatform.Configuration;
using WebPlatform.Cryptography;
using WebPlatform.Menu;
using WebPlatform.Credential;
using System.Web;

namespace WebPlatform
{
    public class ContainerFactory
    {
        private HttpContext context;

        public ContainerFactory(HttpContext context)
        {
            this.context = context;
        }

        public void Register(IContainer container)
        {
            if (container != null)
            {
                InitializeConfiguration(container);
                InitializeDatabase(container);
                InitializeUser(container);
                InitializeMenu(container);
            }
        }
         
        public void Unregister(IContainer container)
        {
            if (container != null)
            {
                container.Unregister<User>();
                container.Unregister<PageSettings>();
                container.Unregister<MenuExtensions>();
                container.Unregister<IUserProvider>();
                container.Unregister<IMenuProvider<MenuCollection>>();
                container.Unregister<PlatformExtensions>();
                container.Unregister<ApplicationSettings>();
                container.Unregister<IDbManager>();
            } 
        }
          
        private void InitializeConfiguration(IContainer container)
        {
            ApplicationSettings settings = new ApplicationSettings();
            container.Register<ApplicationSettings>().ImplementedBy(settings);
        }

        private void InitializeDatabase(IContainer container)
        {
            IEncryptionAgent encryption;
            string passwd;

            IDbManager dbManager = new Common.Database.Petapoco.PetapocoDbManager(null, null);
            IFileLoader resourceloader = new ResourceSqlLoader("resource-loader", "SqlFiles", typeof(DatabaseManager).Assembly);
            IFileLoader fileloader = new FileSqlLoader("file-loader", AppDomain.CurrentDomain.BaseDirectory + "SqlFiles");
            dbManager.AddSqlLoader(resourceloader);
            dbManager.AddSqlLoader(fileloader); 

            ConnectionStringCollection connections = ApplicationSettings.Instance.Connection.Items;
            foreach (ConnectionStringElement connection in connections)
            {
                encryption = new RijndaelEncryption(connection.Key, connection.IV);
                passwd = encryption.Decrypt(connection.Password);

                dbManager.ConnectionDescriptor.Add(new ConnectionDescriptor()
                {
                    ConnectionString = string.Format(connection.ConnectionString, connection.UserId, passwd),
                    IsDefault = connection.IsDefault,
                    Name = connection.Name,
                    ProviderName = connection.ProviderName
                });
            }

            container.Register<IDbManager>().ImplementedBy(dbManager);
        }

        private void InitializeUser(IContainer container)
        {
            IDbManager dbManager = container.Resolve<IDbManager>();
            string defaultDbName = dbManager.ConnectionDescriptor.Where(x => x.IsDefault).Select(c => c.Name).SingleOrDefault();
            if (!string.IsNullOrEmpty(defaultDbName))
                container.Register<IUserProvider>().ImplementedBy(new DatabaseUserProvider(dbManager));
        }

        private void InitializeMenu(IContainer container)
        {
            IDbManager dbManager = container.Resolve<IDbManager>();
            string defaultDbName = dbManager.ConnectionDescriptor.Where(x => x.IsDefault).Select(c => c.Name).SingleOrDefault();
            if (!string.IsNullOrEmpty(defaultDbName))
            {
                IMenuProvider<MenuCollection> menuProvider = new DatabaseMenuProvider(dbManager); 
                container.Register<IMenuProvider<MenuCollection>>().ImplementedBy(menuProvider);
            }
        }
         
        private void InitializeCryptography(IContainer container)
        {
            SymmetricEncryption sym = new RijndaelEncryption(ApplicationSettings.Instance.SecuritySettings.Key, ApplicationSettings.Instance.SecuritySettings.IV);
            container.Register<Cryptography.IEncryptionAgent>().ImplementedBy(sym);

            container.Register<Cryptography.IEncryptionFileAgent>().ImplementedBy<PGPEncryption>();
        }
    }
}
