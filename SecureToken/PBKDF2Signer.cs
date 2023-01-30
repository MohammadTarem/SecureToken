using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace SecureToken
{
    public class PBKDF2Signer : ISigner
    {
        protected byte[] keys;
        protected int outputLength;
        protected int iterations;
        public PBKDF2Signer(byte[] keys, int length, int iterations)
        {
            if (keys == null || keys.Length < 64)
            {
                throw new ArgumentException("Hash key length must be at least 64 bytes.");
            }

            if (length < 128)
            {
                throw new ArgumentException("Length must be at least 128 bytes.");
            }

            this.keys = keys;
            this.outputLength = length;
            this.iterations = iterations < 10000 ? 10000 : iterations;


        }

        public byte[] Hash(byte[] plainBytes)
        {
            return KeyDerivation.Pbkdf2(Convert.ToBase64String(plainBytes),
                                        keys, KeyDerivationPrf.HMACSHA512,
                                        iterations, outputLength);
        }

        public static string PasswordHasher(string password, string base64StringKey)
        {
            return Convert.ToBase64String
                (
                    KeyDerivation.Pbkdf2(password,
                                         Convert.FromBase64String(base64StringKey), 
                                         KeyDerivationPrf.HMACSHA512,
                                         10000, 128)
                );
        }

    
    }
}
