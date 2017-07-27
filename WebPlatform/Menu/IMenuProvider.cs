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
using System.Web.Mvc;

namespace WebPlatform.Menu
{
    public interface IMenuProvider<T>
    { 
        void Build();
        void BuildOnce();
        T GetHierarchyMenuData();
        User User { get; set; } 
    }
}
