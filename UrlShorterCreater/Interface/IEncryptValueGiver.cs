namespace UrlShorterCreater.Interface
{
    public interface IEncryptValueGiver
    {
        Task<long> GetValueAsync(int current);
    }
}
