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
using Common.Database;
using WebPlatform.Credential;
using WebPlatform.Menu;
using System.Web;
using System.Web.Mvc;

namespace WebPlatform.Menu
{
    public class DatabaseMenuProvider : IMenuProvider<MenuCollection>
    {
        private MenuCollection menuCollection;
        private IDbManager dbManager;
        private User user; 

        public DatabaseMenuProvider(IDbManager dbManager)
        {
            this.dbManager = dbManager; 
        }

        public void Build() 
        { 
            this.menuCollection = new MenuCollection();
            string defaultDbName = dbManager.ConnectionDescriptor.Where(x => x.IsDefault).Select(c => c.Name).SingleOrDefault();
            IDataCommand db = dbManager.GetDatabase(defaultDbName); 
            List<MenuData> menuList;
            menuList = db.Query<MenuData>("GetMenu", new 
            { 
                Username = user.UserName,
                Application = ApplicationSettings.Instance.Environment.ApplicationId
            });
            db.Close();

            HierarchyMenuData menu;
            foreach (MenuData item in menuList)
            {
                menu = BuildMenu(menuList, item); 
                if (item.ParentId == -1)
                    menuCollection.Add(menu);
            }    
        }
         
        public void BuildOnce()
        {
            if (this.menuCollection == null)
                Build();
        }

        public void Rebuild()
        {
            this.menuCollection = null;
            this.Build();
        }

        private HierarchyMenuData BuildMenu(IEnumerable<MenuData> menus, MenuData current)
        {
            HierarchyMenuData data = new HierarchyMenuData();
            if (current != null)
            {
                data.Id = current.Id;
                data.Name = current.Name;
                data.Description = current.Description;
                data.Module = current.Application;
                data.Url = current.Url;
            } 
            List<MenuData> buffer = menus.Where(i => i.ParentId == current.Id).Select(x => x).ToList();   
            foreach (MenuData item in buffer)
            { 
                if (item.Id != -1)
                {
                    HierarchyMenuData child = BuildMenu(menus, item);
                    child.Parent = data;
                    data.AddChildren(child); 
                }
            }
            return data;
        }

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        public MenuCollection GetHierarchyMenuData()
        {
            return (MenuCollection)menuCollection;
        } 
         
    }
}
