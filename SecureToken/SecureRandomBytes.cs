using System.Security.Cryptography;

namespace SecureToken
{
    public static class SecureRandomBytes
    {
        public static byte[] Generate(int count)
        {
            byte[] data = new byte[count];
            using (var r = RandomNumberGenerator.Create())
            {
                r.GetBytes(data, 0, count);
            }
            return data;
        }

    }
}
