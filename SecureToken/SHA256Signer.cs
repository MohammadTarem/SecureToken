using System;
using System.Security.Cryptography;

namespace SecureToken
{
    public class SHA256Signer : ISigner
    {
        private readonly byte[] _hashKey;

        public SHA256Signer(byte[] hashKey)
        {
            if (hashKey.Length != 32)
            {
                throw new ArgumentException("Hash key length must be 32 bytes.");
            }
            _hashKey = hashKey;
        }

        public SHA256Signer(string base64HashKey) : this(Convert.FromBase64String(base64HashKey))
        {

        }

        public byte[] Hash(byte[] plainBytes)
        {
            using (HMACSHA256 hash = new HMACSHA256(_hashKey))
            {
                return hash.ComputeHash(plainBytes);
            }

        }
    }
}
