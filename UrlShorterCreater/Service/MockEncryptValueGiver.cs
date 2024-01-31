using UrlShorterCreater.Interface;

namespace UrlShorterCreater.Service
{
    public class MockEncryptValueGiver : IEncryptValueGiver
    {
        private int _max = 1000;
        public Task<long> GetValueAsync(int current)
        {
            if (current > _max)
                current = 0;

            return Task.FromResult<long>(current++);
        }
    }
}
