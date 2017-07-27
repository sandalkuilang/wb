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
    public class MenuData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public string Application { get; set; } 
        public int ParentId { get; set; }
        public string Url { get; set; } 
    }
}
