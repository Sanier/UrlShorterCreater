using Moq;
using NUnit.Framework;
using UrlShorterCreater.Interface;
using UrlShorterCreater.Models;
using UrlShorterCreater.Service;
using UrlShorterCreater.Tests.Helpers;

namespace UrlShorterCreater.Tests
{
    [TestFixture]
    public class TokenServiceTests
    {
        [Test]
        public async Task GenerateShortUrlFor_All_Good_Returns_Token()
        {
            // arrange
            var valueGiver = new Mock<IEncryptValueGiver>();
            valueGiver.Setup(x => x.GetValueAsync(It.IsAny<int>())).Returns(Task.FromResult<long>(1000));
            var sut = new TokenService(valueGiver.Object,
                DbHelpers.GetDbContext(nameof(GenerateShortUrlFor_All_Good_Returns_Token)));

            // act
            var result = await sut.GenerateShortUrlFor("https://longurl.com");
            var actual = string.IsNullOrEmpty(result);
            
            // assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public async Task GenerateShortUrlFor_ValueGiver_Throws_Returns_Empty_String()
        {
            // arrange
            var valueGiver = new Mock<IEncryptValueGiver>();
            valueGiver.Setup(x => x.GetValueAsync(It.IsAny<int>())).Throws<Exception>();
            var sut = new TokenService(valueGiver.Object,
                DbHelpers.GetDbContext(nameof(GenerateShortUrlFor_ValueGiver_Throws_Returns_Empty_String)));

            // act
            var result = await sut.GenerateShortUrlFor("https://longurl.com");
            var actual = string.IsNullOrEmpty(result);

            // assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public async Task GenerateShortUrlFor_Record_Already_Exists_Returns_Record()
        {
            // arrange
            var valueGiver = new Mock<IEncryptValueGiver>();
            valueGiver.Setup(giver => giver.GetValueAsync(It.IsAny<int>())).Returns(Task.FromResult<long>(100));
            var dbContext = DbHelpers.GetDbContext(nameof(GenerateShortUrlFor_Record_Already_Exists_Returns_Record));
            var sut = new TokenService(valueGiver.Object, dbContext);

            const string longUrl = "https://long_long_url.com";
            const string shortUrl = "myToken";
            await dbContext.Links.AddAsync(new LinkModel()
            {
                LongUrl = longUrl,
                ShortUrl = shortUrl
            });
            await dbContext.SaveChangesAsync();

            // act
            var actual = await sut.GenerateShortUrlFor(longUrl);

            // assert
            Assert.AreEqual(actual, shortUrl);
        }
    }
}
