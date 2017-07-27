using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WebPlatform.Configuration
{
    [ConfigurationCollection(typeof(ConnectionStringElement), AddItemName = "add")]
    public class SocialMediaCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SocialMediaElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as SocialMediaElement;
            if (l_configElement != null)
                return l_configElement.Url;
            else
                return null;
        }
        public SocialMediaElement this[int index]
        {
            get
            {
                return BaseGet(index) as SocialMediaElement;
            }
        }

        public new IEnumerator<SocialMediaElement> GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}
