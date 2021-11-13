using System;
using System.IO;
using System.Security.Cryptography;


namespace SecureToken
{
    public class AesEncryptor : IEncryption
    {
        private byte[] _key;
        private byte[] _initialVector;
        

        public AesEncryptor(byte[] key, byte[] iv)
        {
            if(key.Length != 32)
            {
                throw new ArgumentException("Key must be 32 bytes.");
            }
            if(iv.Length != 16)
            {
                throw new ArgumentException("IV must be 32 bytes.");
            }
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
