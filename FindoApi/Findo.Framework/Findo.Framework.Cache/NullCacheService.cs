using System;

namespace Findo.Framework.Cache
{
    public sealed class NullCacheService : ICacheService
    {
        public T GetOrCreate<T>(string key, Func<T> function) where T : class
        {
            return function();
        }

        public void Remove(string key)
        {

        }

        public T Get<T>(string key) where T : class
        {
            return null;
        }
    }
}
