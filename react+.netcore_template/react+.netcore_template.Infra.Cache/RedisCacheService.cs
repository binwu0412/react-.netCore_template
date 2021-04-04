
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using react_.netcore_template.Application.Commons.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace react_.netcore_template.Infra.Cache
{
    public class RedisCacheService : ICacheService
    {
        protected readonly IConnectionMultiplexer _connectionMultiplexer;
        protected readonly IConfiguration _config;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer, IConfiguration config)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _config = config;
        }

        public Task CacheAsync(string cacheKey, object value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var jsonData = JsonConvert.SerializeObject(value);
            return db.StringSetAsync(cacheKey, jsonData);
        }

        public async Task<T?> GetStructCacheAsync<T>(string cacheKey) where T : struct
        {
            var db = _connectionMultiplexer.GetDatabase();
            var jsonData = await db.StringGetAsync(cacheKey);
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch
            {
                return null;
            }
        }

        public async Task<T> GetCachedAsync<T>(string cacheKey) where T : class
        {
            var db = _connectionMultiplexer.GetDatabase();
            var jsonData = await db.StringGetAsync(cacheKey);
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch
            {
                return null;
            }
        }

        public Task RemoveByPatternAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentException("Pattern can not be null or whitespace.");
            }

            var server = _connectionMultiplexer.GetServer(_config.GetConnectionString("RedisConnection"));
            var db = _connectionMultiplexer.GetDatabase();
            var deleteKeyList = new List<Task>();
            foreach (var key in server.Keys(pattern: "pattern"))
            {
                deleteKeyList.Add(db.KeyDeleteAsync(key));
            }
            return Task.WhenAll(deleteKeyList);
        }

        public Task RemoveByKeyAsync(string cacheKey)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return db.KeyDeleteAsync(cacheKey);
        }
    }
}
