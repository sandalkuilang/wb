﻿/*
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

namespace WebPlatform.Configuration
{
    [ConfigurationCollection(typeof(ConnectionStringElement), AddItemName = "add")]
    public class ConnectionStringCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionStringElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as ConnectionStringElement;
            if (l_configElement != null)
                return l_configElement.ConnectionString;
            else
                return null;
        }

        public ConnectionStringElement this[int index]
        {
            get
            {
                return BaseGet(index) as ConnectionStringElement;
            }
        }

        public new IEnumerator<ConnectionStringElement> GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}