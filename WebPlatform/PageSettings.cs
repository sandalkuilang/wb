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
using System.Web.Mvc;

namespace WebPlatform
{
    public class PageSettings
    {
        private const string ControllerKey = "Controller";
        private const string IndexFunctionKey = "Index";

        public string ScreenId { get; set; }
        public string Title { get; set; }
        public string IndexPage { get; set; }

        public PageSettings(Type controllerType)
        {
            IndexPage = IndexFunctionKey;
            string name = controllerType.Name;
            this.ScreenId = name.Substring(0, name.IndexOf(ControllerKey));
            StringBuilder title = new StringBuilder();
            for(int i = 0; i <= ScreenId.Length - 1; i++)
            {
                if (((int)ScreenId[i]) >= 65 && ((int)ScreenId[i]) <= 90 && i > 1)
                {
                    title.Append(" ");
                    title.Append(ScreenId[i]);
                }
                else
                {
                    title.Append(ScreenId[i]);
                } 
            }
            this.Title = name.Substring(0, name.IndexOf(ControllerKey));
        }

        public PageSettings(Type controllerType, string title)
        {
            IndexPage = IndexFunctionKey;
            string name = controllerType.Name;
            this.ScreenId = name.Substring(0, name.IndexOf(ControllerKey));
            this.Title = title;
        }

        //public void SetRequest(ViewDataDictionary viewData)
        //{

        //}


    }
}
