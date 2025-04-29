namespace DomainLayer.Helpers
{
    public interface IEncryption
    {
        byte[] GenerateHash(string password, byte[] saltBytes);
        byte[] GenerateSalt(int size);
    }
}