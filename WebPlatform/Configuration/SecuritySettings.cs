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

namespace WebPlatform.Configuration
{
    public class SecuritySettings : ConfigurationSection
    {

        [ConfigurationProperty("key")]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("iv")]
        public string IV
        {
            get
            {
                return (string)this["iv"];
            }
            set
            {
                this["iv"] = value;
            }
        }

        [ConfigurationProperty("publicKey")]
        public string PublicKey
        {
            get
            {
                return (string)this["publicKey"];
            }
            set
            {
                this["publicKey"] = value;
            }
        }
         
        [ConfigurationProperty("privateKey")]
        public string PrivateKey
        {
            get
            {
                return (string)this["privateKey"];
            }
            set
            {
                this["privateKey"] = value;
            }
        }


    }
}