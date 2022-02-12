using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace SecureToken
{
    public class PBKDF2Signer : ISigner
    {
        protected byte[] keys;
        protected int outputLength;
        protected int iterations;
        public PBKDF2Signer(byte[]keys, int length, int iterations)
        {
            if(keys == null || keys.Length < 64)
            {
                throw new ArgumentException("Hash key length must be at least 64 bytes.");
            }

            if(length < 128)
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
    }
}
