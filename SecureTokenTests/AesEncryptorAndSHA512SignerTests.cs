using System.Linq;
using Xunit;
using SecureToken;

namespace SecureTokenTests
{
    public class AesEncryptorAndSHA512SignerTests
    {

        [Fact]
        public void Encryption_And_Decryption_Must_Work()
        {
            var cert = Helpers.Generator.GetNewCertificate();
            var options = GetTokenOptions();

            var token = Token.CreateToken(cert, options);
            var result = Token.CreateCertificate(token, options);


            Assert.Equal(cert.Identifier, result.Identifier);
            Assert.Equal(cert.Issuer, result.Issuer);
            Assert.Equal(cert.ValidFrom, result.ValidFrom);
            Assert.Equal(cert.ExpiresAt, result.ExpiresAt);
            Assert.True(cert.Claims.SequenceEqual( result.Claims.ToArray()));

        }


        private static SecureTokenOptions GetTokenOptions()
        {
            var key = SecureRandomBytes.Generate(32);
            var iv  = SecureRandomBytes.Generate(16);
            var hashKey = SecureRandomBytes.Generate(64);

            return new SecureTokenOptions
            {
                Encryptor = new AesEncryptor(key, iv),
                Signer = new SHA512Signer(hashKey)
            };
        }
        
    }
}
