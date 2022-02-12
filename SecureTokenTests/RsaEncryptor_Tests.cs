using System.Linq;
using Xunit;
using SecureToken;
using System.Security.Cryptography;


namespace SecureTokenTests
{
    public class RsaEncryptor_Tests
    {
        public RsaEncryptor_Tests()
        {

        }

        [Fact]
        public void RsaEncryptor_Must_Encrypt_Decrypt()
        {

            RSACryptoServiceProvider r = new RSACryptoServiceProvider();

            RsaEncryptor rsa = new RsaEncryptor(r.ToXmlString(true));
            var message = SecureRandomBytes.Generate(10);

            var cipher = rsa.Encrypt(message);
            var plain = rsa.Decrypt(cipher);

            Assert.True(message.SequenceEqual(plain));

            
        }

        [Fact]
        public void RsaEncryptor_Must_Encrypt_Decrypt_With_Param_Constructor()
        {

            RSACryptoServiceProvider r = new RSACryptoServiceProvider();

            RsaEncryptor rsa = new RsaEncryptor(r.ExportParameters(true));
            var message = SecureRandomBytes.Generate(10);

            var cipher = rsa.Encrypt(message);
            var plain = rsa.Decrypt(cipher);

            Assert.True(message.SequenceEqual(plain));


        }
    }
}
