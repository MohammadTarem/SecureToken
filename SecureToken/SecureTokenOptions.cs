namespace SecureToken
{
    public class SecureTokenOptions
    {
        public IEncryption Encryptor { get; set; }
        public ISigner Signer { get; set; }

    }
}
