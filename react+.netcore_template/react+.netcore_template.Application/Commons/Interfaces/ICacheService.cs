using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Interfaces
{
    public interface ICacheService
    {
        Task CacheAsync(string cacheKey, object value);
        Task<T?> GetStructCacheAsync<T>(string cacheKey) where T : struct;

        Task<T> GetCachedAsync<T>(string cacheKey) where T : class;

        Task RemoveByPatternAsync(string pattern);

        Task RemoveByKeyAsync(string cacheKey);
    }
}
