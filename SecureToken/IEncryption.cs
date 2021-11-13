
namespace SecureToken
{
    public interface IEncryption
    {
        byte[] Encrypt(byte[] plainBytes);
        byte[] Decrypt(byte[] cipherBytes);
    }
}
