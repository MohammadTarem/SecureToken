using System;
using System.Security.Cryptography;

namespace SecureToken
{
    public class SHA384Signer : ISigner
    {
        private readonly byte[] _hashKey;

        public SHA384Signer(byte[] hashKey)
        {
            if (hashKey.Length != 48)
            {
                throw new ArgumentException("Hash key length must be 48 bytes.");
            }
            _hashKey = hashKey;
        }

        public SHA384Signer(string base64HashKey) : this(Convert.FromBase64String(base64HashKey))
        {

        }

        public byte[] Hash(byte[] plainBytes)
        {
            using HMACSHA384 hash = new HMACSHA384(_hashKey);
            return hash.ComputeHash(plainBytes);
        }
    }
}
