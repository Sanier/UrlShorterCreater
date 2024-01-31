using Moq;
using UrlShorterCreater.Interface;
using UrlShorterCreater.Service;

namespace UrlShorterCreater.Tests.Helpers
{
    public static class TokenServiceHelpers
    {
        public static TokenService GetService()
        {
            var encryptGiver = new Mock<IEncryptValueGiver>();
            encryptGiver.Setup(giver => giver.GetValueAsync(0)).Returns(Task.FromResult<long>(100));

            return new TokenService(encryptGiver.Object, DbHelpers.GetDbContext(Guid.NewGuid().ToString()));
        }
    }
}
