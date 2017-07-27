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
    public class Role : KeyValueData
    {
        public IList<AuthorizationFunction> Functions { get; set; }

        public Role()
        {
            Functions = new List<AuthorizationFunction>();
        }
    }
}
