using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebPlatform.Cryptography
{
    internal class GenerateKeyIV
    {
        private SymmetricAlgorithm algorithm;
        public GenerateKeyIV(System.Security.Cryptography.SymmetricAlgorithm alg)
        {
            algorithm = alg;
        }

        public byte[] GenerateKey()
        {
            algorithm.GenerateKey();
            return algorithm.Key;
        }
        public byte[] GenerateIV()
        {
            algorithm.GenerateIV();
            return algorithm.IV;
        }
    }
}
