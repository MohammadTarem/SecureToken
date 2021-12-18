using System;
using Xunit;
using SecureToken;
using System.Linq;
using SecureTokenTests.Helpers;

namespace SecureTokenTests
{
    public class SHA256_384SignerTests
    {
        public SHA256_384SignerTests()
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
    }
}
