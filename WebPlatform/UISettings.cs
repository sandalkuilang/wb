using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPlatform
{
    public class UISettings : BaseRazorHtmlExtensions
    { 
         
        public string Theme
        {
            get;
            set;
        }


        public UISettings()
        {

        }
         
        protected override StringBuilder OnExecute()
        {
            StringBuilder output = new StringBuilder();
            //output.Append(string.Format("<link rel=\"stylesheet\" href=\"~/~/Content/kendo/styles/{1}\" />", Theme));
            output.Append(Theme);
            return output;
        }

    }

    public class Theme
    {
        public const string Default = "kendo.default.min.css";
        public const string Black = "kendo.black.min.css";
        public const string Blue = "kendo.blueopal.min.css";
        public const string Flat = "kendo.fat.min.css";
        public const string Moonlight = "kendo.moonlight.min.css";
        public const string Metro = "kendo.metro.min.css";
        public const string Metroblack = "kendo.metroblack.min.css";
        public const string Silver = "kendo.silver.min.css";
        public const string Uniform = "kendo.uniform.min.css"; 
    }
}
