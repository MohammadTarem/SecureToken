using System;
using System.Security.Cryptography;

namespace Security
{
    public class SHA512Signer : ISigner
    {
        private byte[] _hashKey;

        public SHA512Signer(byte[] hashKey)
        {
            _hashKey = hashKey;
        }

        public SHA512Signer(string base64) : this(Convert.FromBase64String(base64))
        {

        }

        public byte[] Hash(byte[] plainBytes)
        {
            using (HMACSHA512 hash = new HMACSHA512(_hashKey))
            {
                return hash.ComputeHash(plainBytes);
            }

        }
    }
}
