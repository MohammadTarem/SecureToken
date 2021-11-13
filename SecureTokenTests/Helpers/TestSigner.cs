using System;
using SecureToken;
namespace SecureTokenTests.Helpers
{
    public class TestSigner : ISigner
    {
        

        public byte[] Hash(byte[] plainBytes)
        {
            return plainBytes;
        }
    }
}
