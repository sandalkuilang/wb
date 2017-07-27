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

namespace WebPlatform.Credential
{
    public class AuthorizationFunction : KeyValueData
    {
        public IList<AuthorizationFeatureQualifier> Features { get; set; }

        public AuthorizationFunction()
        {
            Features = new List<AuthorizationFeatureQualifier>();
        }
    }
}
