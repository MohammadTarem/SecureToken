using System;
using Xunit;
using SecureToken;
using System.Linq;
using SecureTokenTests.Helpers;

namespace SecureTokenTests
{
    public class SHA_SignerTests
    {
        public SHA_SignerTests()
        {

        }

        [Fact]
        public void Sha256_Must_Generate_32_Bytes_Hash()
        {
            SHA256Signer signer = new SHA256Signer(SecureRandomBytes.Generate(32));
            var message = SecureRandomBytes.Generate(32);
            var hash = signer.Hash(message);

            Assert.Equal(32, hash.Length);
            Assert.False(hash.SequenceEqual(message));

        }

        [Fact]
        public void Sha384_Must_Generate_48_Bytes_Hash()
        {
            SHA384Signer signer = new SHA384Signer(SecureRandomBytes.Generate(48));
            var message = SecureRandomBytes.Generate(48);
            var hash = signer.Hash(message);

            Assert.Equal(48, hash.Length);
            Assert.False(hash.SequenceEqual(message));

        }

        [Fact]
        public void Sha512_Must_Generate_64_Bytes_Hash()
        {
            SHA512Signer signer = new SHA512Signer(SecureRandomBytes.Generate(64));
            var message = SecureRandomBytes.Generate(64);
            var hash = signer.Hash(message);

            Assert.Equal(64, hash.Length);
            Assert.False(hash.SequenceEqual(message));

        }

        [Fact]
        public void PasswordHasher_Must_Return_Same_Results()
        {
            var key = Convert.ToBase64String(SecureRandomBytes.Generate(64));
            var password = "MyStr0ngP@ssw0rd";

            var hash1 = SHA512Signer.PasswordHasher(password, key);
            var hash2 = SHA512Signer.PasswordHasher(password, key);

            Assert.Equal(hash1, hash2);

        }
    }
}
