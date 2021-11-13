using System;
using SecureToken;

namespace SecureTokenTests.Helpers
{
    public class TestEncryptor : IEncryption
    {
        public TestEncryptor()
        {
        }

        public byte[] Decrypt(byte[] cipherBytes)
        {
            return cipherBytes;
        }

        public byte[] Encrypt(byte[] plainBytes)
        {
            return plainBytes;
        }
    }
}
