using System;
using System.Collections.Generic;
using System.Text;

namespace Findo.Framework.Cache
{
    /// <summary>
    /// Represents a cache service interface 
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets or create a new cache entry into cache service
        /// </summary>
        /// <typeparam name="T">Type of object to be retrieved or stored on cache system</typeparam>
        /// <param name="key">Unique key used to identify the cache object</param>
        /// <param name="function">Function used to perform action on object creation</param>
        /// <returns>An instance of object stored into cache service</returns>
        T GetOrCreate<T>(string key, Func<T> function) where T : class;

        /// <summary>
        /// Removes an object stored into cache service from cache
        /// </summary>
        /// <param name="key">Unique key used to identify the cache object</param>
        void Remove(string key);

        /// <summary>
        /// Gets an object instance stored into cache service
        /// </summary>
        /// <typeparam name="T">Type of object to be retrieved or stored on cache system</typeparam>
        /// <param name="key">Unique key used to identify the cache object</param>
        /// <returns>An instance of object stored into cache service</returns>
        T Get<T>(string key) where T : class;
    }
}
