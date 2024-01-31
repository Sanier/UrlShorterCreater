using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UrlShorterCreater.Controllers;

namespace UrlShorterCreater.Tests.Helpers
{
    public static class UrlControllerHelpers
    {
        public static UrlController GetController()
        {
            var sut = new UrlController(TokenServiceHelpers.GetService());
            var context = new Mock<HttpContext>();
            context.Setup(c => c.Request.Host).Returns(new HostString("localhost", 443));
            sut.ControllerContext = new ControllerContext()
            {
                HttpContext = context.Object
            };

            return sut;
        }
    }
}
