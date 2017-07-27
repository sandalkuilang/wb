/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WebPlatform.Configuration
{ 
    public class ConnectionStringSettings : ConfigurationSection
    {
        [ConfigurationProperty("connectionString")]
        [ConfigurationCollection(typeof(ConnectionStringCollection))]
        public ConnectionStringCollection Items
        {
            get
            {
                return ((ConnectionStringCollection)(base["connectionString"]));
            }
            set
            {
                base["connectionString"] = value;
            }
        } 
    }
}