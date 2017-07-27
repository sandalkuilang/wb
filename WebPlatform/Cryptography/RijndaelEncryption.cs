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
using Crypto;

namespace WebPlatform.Cryptography
{
    /// <summary>
    /// A class to decrypt/encrypt data using symetric algorithm   
    /// </summary>
    public class RijndaelEncryption : SymmetricEncryption
    {
        private Crypto.IEncryptionAgent agent;

        /// <summary>
        /// Create instance by reading key ("PublicKey") in appSettings section in configuration
        /// </summary>
        public RijndaelEncryption()
        {
            agent = new Crypter(new Base64WebConfigurationKey());
        }

        public RijndaelEncryption(string key, string iv)
            : base(key, iv)
        {
            System.Security.Cryptography.RijndaelManaged rijnManaged = new System.Security.Cryptography.RijndaelManaged();
            
            /* used to generate key and IV */
            /*
            GenerateKeyIV gen = new GenerateKeyIV(rijnManaged);
            string s = Convert.ToBase64String(gen.GenerateKey());
            string sa = Convert.ToBase64String(gen.GenerateIV());

            rijnManaged.IV = Convert.FromBase64String(sa);
            rijnManaged.Key = Convert.FromBase64String(s);
            agent = new Crypto.SymmetricEncryption(rijnManaged);
            string enc = Convert.ToBase64String(agent.Encrypt(System.Text.ASCIIEncoding.ASCII.GetBytes("sa123")));
            */

            try
            {
                rijnManaged.Key = Convert.FromBase64String(key);
                rijnManaged.IV = Convert.FromBase64String(iv);
                agent = new Crypto.SymmetricEncryption(rijnManaged);
            }
            catch(System.Security.Cryptography.CryptographicException)
            {
                rijnManaged.Dispose();
                throw new System.Security.Cryptography.CryptographicException("Invalid key or iv.");
            }           
        }
         
        public override string Encrypt(string value)
        { 
            byte[] values = System.Text.Encoding.ASCII.GetBytes(value); 
            return Convert.ToBase64String(agent.Encrypt(values));
        }

        public override string Decrypt(string value)
        {
            byte[] values = Convert.FromBase64String(value);
            return System.Text.Encoding.ASCII.GetString(agent.Decrypt(values));
        }
    }
}