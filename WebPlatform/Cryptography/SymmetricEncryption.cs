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

namespace WebPlatform.Cryptography
{
    public abstract class SymmetricEncryption : IEncryptionAgent
    {
        public string Key { get; set; }
        public string IV { get; set; }

        public SymmetricEncryption()
        {

        }

        public SymmetricEncryption(string key, string iv)
        {
            this.Key = key;
            this.IV = iv;
        }

        public abstract string Encrypt(string value);

        public abstract string Decrypt(string value);
    }
}