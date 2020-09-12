using Microsoft.Extensions.Options;
using NorthWindApp.BLL.Infrastructure.ConfigurationOptions;

namespace NorthWindApp.BLL.Services
{
    public class CacheImageService : GenericCacheService<byte[]>
    {
        public CacheImageService(IOptions<CacheOptions> options)
            :base(options)
        {
        }
    }
}
