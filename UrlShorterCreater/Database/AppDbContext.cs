using Microsoft.EntityFrameworkCore;
using UrlShorterCreater.Models;

namespace UrlShorterCreater.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<LinkModel> Links { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
