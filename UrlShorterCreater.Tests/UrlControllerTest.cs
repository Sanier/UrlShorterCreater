using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using UrlShorterCreater.Tests.Helpers;

namespace UrlShorterCreater.Tests
{
    [TestFixture]
    public class UrlControllerTest
    {
        [Test]
        public async Task ShortUrl_WithNullOrEmptyUrl_ReturnsBadRequest()
        {
            var sut = UrlControllerHelpers.GetController();

            var result = await sut.ShortUrl(null);
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task ShortUrl_WithValidUrl_ReturnsOk()
        {
            // Arrange
            var sut = UrlControllerHelpers.GetController();

            // Act
            var result = await sut.ShortUrl("https://example.com") as OkObjectResult;
            var shortUrl = result?.Value;

            // Assert
            Assert.IsNotNull(shortUrl);
        }
    }
}
