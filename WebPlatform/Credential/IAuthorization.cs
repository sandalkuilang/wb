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
    public interface IAuthorization
    {
        bool IsAuthorize(string key, string qualifier);
    }
}
