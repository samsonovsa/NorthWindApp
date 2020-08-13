﻿using Microsoft.Extensions.Options;
using NorthWindApp.BLL.Infrastructure.ConfigurationOptions;
using NorthWindApp.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.BLL.Services
{
    public class CacheImageService : ICacheImageService
    {
        CacheOptions _options;
        Dictionary<string, CachedImage> _cache;
        private readonly string _filePath;

        public CacheImageService(IOptions<CacheOptions> options)
        {
            _options = options.Value;
            _cache = new Dictionary<string, CachedImage>();

            _filePath = Path.Combine(Directory.GetCurrentDirectory(), _options.Path);
            Directory.CreateDirectory(_filePath);
        }

        public async Task<byte[]> GetImageAsync(string key)
        {
            var cachedImage = new CachedImage();
            if (_cache.TryGetValue(key, out cachedImage))
            {
                if (cachedImage.TimeSetCache.AddSeconds(_options.CacheExpirationTimeInSec) < DateTime.Now)
                    return null;

                if (File.Exists(cachedImage.Path))
                {
                    return await Task.FromResult<byte[]>(File.ReadAllBytes(cachedImage.Path));
                }
                else
                    return null;
            }
            else
                return null;
        }

        public async Task SetImageAsync(string key, byte[] image)
        {
            var cachedImage = new CachedImage()
            {
                Image = image,
                TimeSetCache = DateTime.Now,
                Path = Path.Combine(_filePath, Guid.NewGuid().ToString()+".png")
            };

            await CacheRemoveAsync(key);
            await CacheAddAsync(key, cachedImage);
        }

        private async Task CacheAddAsync(string key, CachedImage cachedImage)
        {
            if (cachedImage.Image.Length == 0)
                return;

            if (_cache.Keys.Count >= _options.MaxCountCachigImage)
                if (!(await TryClearCachAsync()))
                    return;

            await Task.Run(() =>
            {
                try
                {
                    File.WriteAllBytes(cachedImage.Path, cachedImage.Image);
                    _cache.Add(key, cachedImage);
                }
                catch (Exception ex)
                {
                    throw new  IOException("Error file writing",ex);
                }
            });
        }

        private async Task<bool> TryClearCachAsync()
        {
            var oldCache = _cache.Values.Where(
                x => x.TimeSetCache.AddSeconds(_options.CacheExpirationTimeInSec) > DateTime.Now);

            if (oldCache == null)
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
                var cachedImage = new CachedImage();

                await Task.Run(() =>
                {
                    try
                    {
                        if (_cache.TryGetValue(key, out cachedImage))
                        {
                            if (File.Exists(cachedImage.Path))
                            {
                                File.Delete(cachedImage.Path);
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

        public async Task ClearAsync()
        {
            await ClearCacheByKeysAsync(_cache.Keys);
        }


        private async Task ClearCacheByKeysAsync(IEnumerable<string> keys)
        {
            if (keys.Count() == 0)
                return;

            var tasks = new List<Task>();

            foreach (var key in keys)
            {
                tasks.Add(CacheRemoveAsync(key));
            }

            await Task.WhenAll(tasks.ToArray());
        }
    }

    class CachedImage
    {
        public byte[] Image { get; set; }

        public DateTime TimeSetCache { get; set; }

        public string Path { get; set; }
    }
}
