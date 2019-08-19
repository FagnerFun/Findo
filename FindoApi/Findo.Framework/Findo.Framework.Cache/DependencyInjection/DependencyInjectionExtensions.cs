using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Findo.Framework.Cache.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Adds null caching services to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add services to.</param>
        /// <returns><see cref="IServiceCollection"/> so that additionalcalls can be chained.</returns>
        public static IServiceCollection AddNullCache(this IServiceCollection services) =>
            services.AddSingleton<ICacheService, NullCacheService>();

        /// <summary>
        /// Adds in-memory caching services to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add services to.</param>
        /// <returns><see cref="IServiceCollection"/> so that additionalcalls can be chained.</returns>
        public static IServiceCollection AddInMemoryCache(this IServiceCollection services) =>
            services.AddSingleton<ICacheService, InMemoryCacheService>();

        /// <summary>
        /// Adds Redis distributed caching services to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="setupAction"></param>
        /// <returns><see cref="IServiceCollection"/> so that additionalcalls can be chained.</returns>
        public static IServiceCollection AddRedisCache(this IServiceCollection services, Action<RedisCacheOptions> setupAction) =>
            services.AddDistributedRedisCache(setupAction)
                    .AddSingleton<ICacheService, RedisCacheService>();
    }
}
