using Microsoft.Extensions.Options;
using NorthWindApp.BLL.Infrastructure.ConfigurationOptions;
using NorthWindApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace NorthWindApp.BLL.Services
{
    public class GenericCacheService<TEntity> : IGenericCacheService<TEntity>
    {
        private readonly CacheOptions _options;
        private readonly Dictionary<string, CachedEntity<TEntity>> _cache;
        private readonly string _filePath;

        public GenericCacheService(IOptions<CacheOptions> options)
        {
            _options = options.Value;
            _cache = new Dictionary<string, CachedEntity<TEntity>>();

            _filePath = Path.Combine(Directory.GetCurrentDirectory(), _options.Path);
            Directory.CreateDirectory(_filePath);
        }

        public async Task<TEntity> GetEntityAsync(string key)
        {
            var cachedEntity = new CachedEntity<TEntity>();
            if (_cache.TryGetValue(key, out cachedEntity))
            {
                if (cachedEntity.TimeSetCache.AddSeconds(_options.CacheExpirationTimeInSec) < DateTime.Now)
                    return default;

                if (File.Exists(cachedEntity.Path))
                {
                    return await Task.Run(()=>
                    {
                        File.ReadAllBytes(cachedEntity.Path);
                        BinaryFormatter formatter = new BinaryFormatter();
                        TEntity entity;

                        using (FileStream fs = new FileStream(cachedEntity.Path, FileMode.OpenOrCreate))
                        {
                            entity = (TEntity)formatter.Deserialize(fs);
                        }

                        return entity;
                    });
                }
                else
                    return default;
            }
            else
                return default;
        }

        public async Task SetEntityAsync(string key, TEntity entity)
        {
            var cachedEntity = new CachedEntity<TEntity>()
            {
                Entity = entity,
                TimeSetCache = DateTime.Now,
                Path = Path.Combine(_filePath, Guid.NewGuid().ToString()+".png")
            };

            await CacheRemoveAsync(key);
            await CacheAddAsync(key, cachedEntity);
        }

        private async Task CacheAddAsync(string key, CachedEntity<TEntity> cachedEntity)
        {
            if (cachedEntity.Entity == null)
                return;

            if (_cache.Keys.Count >= _options.MaxCountCachigImage)
                if (!(await TryClearCacheAsync()))
                    return;

            await Task.Run(() =>
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(cachedEntity.Path, FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, cachedEntity.Entity);
                    }
                    _cache.Add(key, cachedEntity);
                }
                catch (Exception ex)
                {
                    throw new  IOException("Error file writing",ex);
                }
            });
        }

        public async Task ClearAsync()
        {
            await ClearCacheByKeysAsync(_cache.Keys);
        }

        private async Task<bool> TryClearCacheAsync()
        {
            var oldCache = _cache.Values.Where(
                x => x.TimeSetCache.AddSeconds(_options.CacheExpirationTimeInSec) > DateTime.Now);

            if (!oldCache.Any())
                return false;

            var keys = new List<string>();

            foreach (var cache in oldCache)
            {
                var cacheKey = _cache.FirstOrDefault(x => x.Value == cache).Key;
                keys.Add(cacheKey);
            }

            await ClearCacheByKeysAsync(keys);

            return true;
        }

        private async Task CacheRemoveAsync(string key)
        {
            if (_cache.ContainsKey(key))
            {
                var cachedEntity = new CachedEntity<TEntity>();

                await Task.Run(() =>
                {
                    try
                    {
                        if (_cache.TryGetValue(key, out cachedEntity))
                        {
                            if (File.Exists(cachedEntity.Path))
                            {
                                File.Delete(cachedEntity.Path);
                            }
                            _cache.Remove(key);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IOException("Error cache file deleting", ex);
                    }
                });            
            }
        }

        private async Task ClearCacheByKeysAsync(IEnumerable<string> keys)
        {
            if (!keys.Any())
                return;

            var tasks = new List<Task>();

            foreach (var key in keys)
            {
                tasks.Add(CacheRemoveAsync(key));
            }

            await Task.WhenAll(tasks.ToArray());
        }
    }
}
