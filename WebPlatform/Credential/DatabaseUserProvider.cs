/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPlatform.Credential;
using Krokot.Database;
using System.Web; 

namespace WebPlatform.Credential
{
    public class DatabaseUserProvider : IUserProvider
    {
        private IDbManager dbManager; 
 
        public DatabaseUserProvider(IDbManager dbManager)
        {
            this.dbManager = dbManager; 
        }
          
        public User GetUser(string username)
        {
            string defaultDbName = dbManager.ConnectionDescriptor.Where(x => x.IsDefault).Select(c => c.Name).SingleOrDefault();
            IDataCommand db = dbManager.GetDatabase(defaultDbName);
            List<User> user = db.Query<User>("GetUserByName", new 
            { 
                Username = username,
                Application = ApplicationSettings.Instance.Environment.ApplicationId
            });
            db.Close();

            if (user.Any())
                return user[0];

            return null;
        }

        public User GetUser(string username, string password)
        { 
            return null;
        } 

        public Role GetAuthorization(User user)
        {
            Role role = null;
            string defaultDbName = dbManager.ConnectionDescriptor.Where(x => x.IsDefault).Select(c => c.Name).SingleOrDefault();
            IDataCommand db = dbManager.GetDatabase(defaultDbName);
            IList<Role> roles = db.Query<Role>("GetAuthorizationByUsername", new 
            {
                Username = user.UserName,
                Application = ApplicationSettings.Instance.Environment.ApplicationId 
            });

            if (roles.Any())
            {
                IList<AuthorizationFeatureQualifier> features = new List<AuthorizationFeatureQualifier>(); 
                AuthorizationFunction functionBuffer = new AuthorizationFunction();

                role = roles.SingleOrDefault();
                IList<KeyValueData> functions = db.Query<KeyValueData>("GetAuthorizationFunctionByRoleId", new 
                {
                    RoleId = role.Id,
                    Application = ApplicationSettings.Instance.Environment.ApplicationId  
                });

                foreach (KeyValueData function in functions)
                {
                    functionBuffer = new AuthorizationFunction() 
                    {
                        Id = function.Id,
                        Name = function.Name,
                        Description = function.Description 
                    };  
                    features = db.Query<AuthorizationFeatureQualifier>("GetAuthorizationFeautureByFunctionId", new 
                    { 
                        RoleId = role.Id, 
                        FunctionId = function.Id,
                        Application = ApplicationSettings.Instance.Environment.ApplicationId 
                    });

                    if (features.Any())
                    {
                        foreach (AuthorizationFeatureQualifier feature in features)
                        {
                            functionBuffer.Features.Add(feature);
                        } 
                        role.Functions.Add(functionBuffer);
                    } 
                }
            }
            db.Close();
            return role;
        }
         
    }
}
