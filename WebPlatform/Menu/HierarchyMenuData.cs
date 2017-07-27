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
using System.Web.UI;

namespace WebPlatform.Menu
{
    public class HierarchyMenuData : IHierarchyData
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string Module { get; set; } 
        public string Url { get; set; } 
        protected HierarchicalEnumerable children = new HierarchicalEnumerable();
        protected HierarchyMenuData parent = null;

        public HierarchyMenuData()
        {
            parent = this;
        }

        public bool Contains(string id)
        {
            HierarchyMenuData menus = this;
            bool found = false;
            Contains(id, menus, ref found);
            return found;
        }

        private void Contains(string id, HierarchyMenuData menus, ref bool found)
        {
            if (menus.Name.Equals(id))
            {
                found = true;
                return;
            }

            foreach (HierarchyMenuData item in menus.children)
            {
                Contains(id, item, ref found);
            }
        }

        public void AddChildren(HierarchyMenuData menu)
        {
            if (!children.Contains(menu))
            {
                children.Add(menu);
            }
        }

        public IHierarchicalEnumerable GetChildren()
        {
            return children;
        }

        public HierarchicalEnumerable Children
        {
            get { return children; }
            set { children = value; }
        }

        public HierarchyMenuData GetChildren(string id)
        {
            if (this.children.Count > 0)
            {
                foreach (HierarchyMenuData menu in this.children)
                { 
                    if (menu.Id.Equals(id))
                    {
                        return menu;
                    }
                }
            }
            return null;
        }

        public void RemoveChildren(string id)
        {
            if (this.children.Count > 0)
            {
                MenuData item = null;
                foreach (MenuData menu2 in this.children)
                {
                    if (menu2.Id.Equals(id))
                    {
                        item = menu2;
                    }
                }
                if (item != null)
                {
                    this.children.Remove(item);
                }
            }
        }

        public void RemoveChildren(MenuData menu)
        {
            if (this.children.Contains(menu))
            {
                this.children.Remove(menu);
            }
        }

        public HierarchyMenuData GetChildrenByName(string name)
        {
            if (this.children.Count > 0)
            {
                foreach (HierarchyMenuData menu in this.children)
                {
                    if (menu.Name.Equals(name))
                    {
                        return menu;
                    }
                }
            }
            return null;
        }
         
        public HierarchyMenuData Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public IHierarchyData GetParent()
        {
            return parent;
        }

        public bool HasChildren
        {
            get
            {
                if (null == children || 0 == children.Count)
                {
                    return false;
                }
                return true;
            }
        }

        public object Item { get { return ((IHierarchyData)this); } }

        public string Path
        {
            get { return Id.ToString(); }
        }

        public string Type
        {
            get { return "MenuData"; }
        }
    }
}

