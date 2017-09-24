using Microsoft.Extensions.Caching.Memory;
using WC.Blog.Infrastructure.Cache.Configuration;

namespace WC.Blog.Infrastructure.Cache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _cache;
        private readonly ICacheConfiguration _config;

        public MemoryCacheProvider(IMemoryCache memoryCache, ICacheConfiguration config)
        {
            _cache = memoryCache;
            _config = config;
        }
        
        public bool TryGetItem<T>(string id, out T value)
        {
            if (_cache.TryGetValue(id, out value))
            {
                return true;
            }
            return false;
        }

        public T SetItem<T>(string key, T value)
        {
            return _cache.Set(key, value, _config.ExpiryTime);
        }
    }
}
