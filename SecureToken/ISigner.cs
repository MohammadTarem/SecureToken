namespace SecureToken
{
    public interface ISigner
    {
        byte[] Hash(byte[] plainBytes);
    }
}
