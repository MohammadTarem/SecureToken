using System;
using System.Security.Cryptography;
using System.Text;

namespace SecureToken
{
    public class SHA512Signer : ISigner
    {
        private readonly byte[] _hashKey;

        public SHA512Signer(byte[] hashKey)
        {
            if (hashKey.Length != 64)
            {
                throw new ArgumentException("Hash key length must be 64 bytes.");
            }
            _hashKey = hashKey;
        }

        public SHA512Signer(string base64HashKey) : this(Convert.FromBase64String(base64HashKey))
        {

        }

        public byte[] Hash(byte[] plainBytes)
        {
            using HMACSHA512 hash = new HMACSHA512(_hashKey);
            return hash.ComputeHash(plainBytes);

        }

        public static string PasswordHasher(string password, string base64StringKey)
        {
            if(String.IsNullOrEmpty(password))
            {
                return String.Empty;
            }

            var signer = new SHA512Signer(base64StringKey);
            return Convert
                   .ToBase64String
                   (
                       signer.Hash(UnicodeEncoding.UTF8.GetBytes(password))
                   );

        }
    }
}
