using Microsoft.Extensions.Options;
using NorthWindApp.BLL.Infrastructure.ConfigurationOptions;
using NorthWindApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
