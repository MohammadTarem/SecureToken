namespace Security
{
    public interface ISigner
    {
        byte[] Hash(byte[] plainBytes);
    }
}
