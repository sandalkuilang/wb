using Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPlatform.Cryptography
{
    public class ArrayWebConfigurationKey : IKeySym
    {
        public byte[] GetKey()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["PublicKey"];
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("App setting \'PublicKey\' cannot be null.");
            else
            {
                string[] KeySym = value.Split(',');
                byte[] publicKey = new byte[KeySym.Length];
                for (int i = 0; i < KeySym.Length; i++)
                {
                    publicKey[i] = Convert.ToByte(KeySym[i]);
                }
                return publicKey;
            }
        }
    }
}
