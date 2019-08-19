using System;
using System.Collections.Concurrent;

namespace Findo.Framework.Cache
{
    public sealed class InMemoryCacheService : ICacheService
    {
        private readonly ConcurrentDictionary<string, object> cacheObjects;

        public InMemoryCacheService()
        {
            cacheObjects = new ConcurrentDictionary<string, object>();
        }

        public T GetOrCreate<T>(string key, Func<T> function) where T : class
        {
            if (cacheObjects.ContainsKey(key))
                return cacheObjects[key] as T;

            T newObj = function();

            if (newObj == null)
                return null;

            cacheObjects.TryAdd(key, newObj);

            return newObj;
        }

        public void Remove(string key)
        {
            if (!cacheObjects.ContainsKey(key))
                return;

            cacheObjects.TryRemove(key, out object _);
        }

        public T Get<T>(string key) where T : class
        {
            if (cacheObjects.ContainsKey(key))
                return cacheObjects[key] as T;

            return null;
        }
    }
}
