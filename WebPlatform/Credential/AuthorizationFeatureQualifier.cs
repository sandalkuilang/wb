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
    public class AuthorizationFeatureQualifier
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Qualifier { get; set; } 
        public AuthorizationUIPolicy AuthorizationUIPolicy { get; set; }
    }
}
