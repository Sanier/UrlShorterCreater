namespace UrlShorterCreater.Models
{
    public class LinkModel
    {
        public long Id { get; set; }
        public required string LongUrl { get; set; }
        public required string ShortUrl { get; set; }
    }
}
