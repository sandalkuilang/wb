using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;

namespace WebPlatform.Configuration
{
    public class SocialMediaSettings : ConfigurationSection
    {
        [ConfigurationProperty("socialMedia")]
        [ConfigurationCollection(typeof(SocialMediaCollection))]
        public SocialMediaCollection Items
        {
            get
            {
                return ((SocialMediaCollection)(base["socialMedia"]));
            }
            set
            {
                base["socialMedia"] = value;
            }
        }
    }
}
