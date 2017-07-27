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
    public class MenuCollection : List<HierarchyMenuData>, IHierarchicalEnumerable, ICloneable
    {
        private IList<HierarchyMenuData> menu;

        public MenuCollection()
        { 
        }

        public MenuCollection(IList<HierarchyMenuData> menu)
        {
            this.menu = menu;
        }

        public IHierarchyData GetHierarchyData(object enumeratedItem)
        {
            return (HierarchyMenuData)enumeratedItem;
        }
         
        public object Clone()
        {
            return new MenuCollection(this);
        }
    }
}
