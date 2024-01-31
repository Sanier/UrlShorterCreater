using HashidsNet;
using Microsoft.EntityFrameworkCore;
using UrlShorterCreater.Database;
using UrlShorterCreater.Interface;
using UrlShorterCreater.Models;

namespace UrlShorterCreater.Service
{
    public class TokenService
    {
        private readonly IEncryptValueGiver _valueGiver;
        private readonly AppDbContext _dbContext;

        public TokenService(IEncryptValueGiver valueGiver, AppDbContext dbContext)
        {
            _valueGiver = valueGiver;
            _dbContext = dbContext;
        }

        public async Task<string> GenerateShortUrlFor(string longUrl)
        {
            var hashids = new Hashids("this is my salt");

            try
            {
                var candidate = await _dbContext.Links.FirstOrDefaultAsync(x => x.LongUrl == longUrl);
                if (candidate != null)
                    return candidate.ShortUrl;

                var id = await _valueGiver.GetValueAsync(_dbContext.Links.Count());
                var token = hashids.EncodeLong(id);
                var shortUrl = $"https://shortUrl/{token}";

                await _dbContext.Links.AddAsync(new LinkModel()
                {
                    LongUrl = longUrl,
                    ShortUrl = shortUrl
                });

                await _dbContext.SaveChangesAsync();
                return shortUrl;
            }
            catch (Exception)
            {
                //There should be handling of various exceptions here, but there won’t be any.
            }

            return string.Empty;
        }
    }
}
