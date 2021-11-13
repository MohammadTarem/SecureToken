using System;
using System.Text;
namespace SecureToken
{
    public class SecureTokenOptions
    {
        public IEncryption Encryptor { get; set; }
        public ISigner Signer { get; set; }

    }
}
