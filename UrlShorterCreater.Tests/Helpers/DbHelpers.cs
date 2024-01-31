using Microsoft.EntityFrameworkCore;
using UrlShorterCreater.Database;

namespace UrlShorterCreater.Tests.Helpers
{
    public static class DbHelpers
    {
        public static AppDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName);

            return new AppDbContext(options.Options);
        }
    }
}
