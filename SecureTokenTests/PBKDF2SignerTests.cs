using SecureToken;
using System;
using System.Linq;
using Xunit;
namespace SecureTokenTests
{
    public class PBKDF2SignerTests
    {
        public PBKDF2SignerTests()
        {
        }

        [Fact]
        public void PBKDF2_Must_Generate_128_Bytes_Hash()
        {

            var keys = SecureRandomBytes.Generate(64);
            PBKDF2Signer signer = new PBKDF2Signer( keys, 128, 1000);

            var message = SecureRandomBytes.Generate(32);
            var hash = signer.Hash(message);

            Assert.Equal(128, hash.Length);

        }

        [Fact]
        public void PBKDF2_Must_Generate_Same_Hash()
        {
            var keys = SecureRandomBytes.Generate(64);
            PBKDF2Signer signer = new PBKDF2Signer(keys, 128, 1000);

            var message = SecureRandomBytes.Generate(32);
            var hash1 = signer.Hash(message);
            var hash2 = signer.Hash(message);

            Assert.True(hash1.SequenceEqual(hash2));

        }

        [Fact]
        public void PasswordHasher_Must_Return_Same_Results()
        {
            var password = "MyStr0ngP@ssw0rd";
            var key = Convert.ToBase64String(SecureRandomBytes.Generate(64));
            
            var hash1 = PBKDF2Signer.PasswordHasher(password, key);
            var hash2 = PBKDF2Signer.PasswordHasher(password, key);

            Assert.Equal(hash1, hash2);

        }
    }
}
