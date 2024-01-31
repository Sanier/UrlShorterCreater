using Microsoft.AspNetCore.Mvc;
using UrlShorterCreater.Service;

namespace UrlShorterCreater.Controllers
{
    public class UrlController : Controller
    {
        private readonly TokenService _tokenService;
        public UrlController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShortUrl(string longUrl)
        {
            if (string.IsNullOrEmpty(longUrl))
                return BadRequest();
            var shortUrl = await _tokenService.GenerateShortUrlFor(longUrl);
            return Ok(new { outputUrl = shortUrl });
        }
    }
}
