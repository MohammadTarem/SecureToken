using System;
using System.Security.Cryptography;

namespace SecureToken
{
    public class RsaEncryptor : IEncryption
    {
        protected RSACryptoServiceProvider rsaService;
        public RsaEncryptor(RSAParameters rsaParam)
        {

            rsaService = new RSACryptoServiceProvider();
            rsaService.ImportParameters(rsaParam);
            ValidateKeySize();

        }

        public RsaEncryptor(string xml)
        {
            rsaService = new RSACryptoServiceProvider();
            rsaService.FromXmlString(xml);
            ValidateKeySize();

        }

        protected void ValidateKeySize()
        {
            if (rsaService.KeySize < 4096)
            {
                throw new ArgumentException("Encryption key  must be at least 4096 bits.");
            }
        }

        public byte[] Decrypt(byte[] cipherBytes)
        {
            return rsaService.Decrypt(cipherBytes, false);
        }

        public byte[] Encrypt(byte[] plainBytes)
        {
            return rsaService.Encrypt(plainBytes, false);
        }
    }
}
