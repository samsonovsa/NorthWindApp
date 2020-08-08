
namespace NorthWindApp.BLL.Infrastructure.ConfigurationOptions
{
    public class CacheOptions
    {
        public const string CacheImage = nameof(CacheImage);

        public string Path { get; set; }

        public int MaxCountCachigImage { get; set; }

        public int CacheExpirationTimeInSec { get; set; }
    }
}
