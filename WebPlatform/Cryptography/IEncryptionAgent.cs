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

namespace WebPlatform.Cryptography
{
    public interface IEncryptionAgent
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
