using SecureToken;
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
    }
}
