using System;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Findo.Framework.Cache
{
    public sealed class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<RedisCacheService> logger;

        public RedisCacheService(IDistributedCache distributedCache,
            ILogger<RedisCacheService> logger
        )
        {
            this.distributedCache = distributedCache;
            this.logger = logger;
        }

        public T GetOrCreate<T>(string key, Func<T> function) where T : class
        {
            try
            {
                string value = distributedCache.GetString(key);
                if (!string.IsNullOrWhiteSpace(value))
                    return JsonConvert.DeserializeObject<T>(value);

                T newObj = function();

                if (newObj == null)
                    return null;

                distributedCache.SetString(key, JsonConvert.SerializeObject(newObj));

                return newObj;
            }
            catch (Exception exception)
            {
                logger.LogError("Redis cache service error.", exception);
                return function();
            }
        }

        public void Remove(string key)
        {
            try
            {
                distributedCache.Remove(key);
            }
            catch (Exception exception)
            {
                logger.LogError("Redis cache service error to remove an entry.", exception);
            }
        }

        public T Get<T>(string key) where T : class
        {
            try
            {
                string value = distributedCache.GetString(key);
                return !string.IsNullOrWhiteSpace(value) ? JsonConvert.DeserializeObject<T>(value) : null;
            }
            catch (Exception exception)
            {
                logger.LogError("Redis cache service error.", exception);
                return null;
            }
        }
    }
}
