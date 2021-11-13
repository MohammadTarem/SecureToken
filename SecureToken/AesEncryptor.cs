using System;
using System.IO;
using System.Security.Cryptography;


namespace Security
{
    public class AesEncryptor : IEncryptor
    {
        private byte[] _key;
        private byte[] _initialVector;
        

        public AesEncryptor(byte[] key, byte[] iv)
        {
            _key = key;
            _initialVector = iv;
            
        }

        public AesEncryptor(string base64Key, string base64IV)
            : this(Convert.FromBase64String(base64Key),
                  Convert.FromBase64String(base64IV))
        { }
       
        public byte[] Encrypt(byte[] plainBytes)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = _key;
                aes.IV = _initialVector;
                return Transform(plainBytes, aes.CreateEncryptor());
            }

        }

        public byte[] Decrypt(byte[] cipherBytes)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _initialVector;
                return Transform(cipherBytes, aes.CreateDecryptor());
            }

        }

        private static byte[] Transform(byte[] data, ICryptoTransform transformer)
        {
            byte[] outData = null;
            if (data != null)
            {
                using (var memory = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memory, transformer, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();
                    }
                    outData = memory.ToArray();
                }

            }
            return outData;
        }
    }
}
