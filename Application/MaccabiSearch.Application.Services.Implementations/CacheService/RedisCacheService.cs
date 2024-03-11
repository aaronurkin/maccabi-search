using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MaccabiSearch.Application.Services.Implementations
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache cache;

        public RedisCacheService(IDistributedCache cache)
        {
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// Gets a string from the cache with the specified key.
        /// </summary>
        /// <typeparam name="TResult">The type of data retrieved from cache.</typeparam>
        /// <param name="key">The key to store the data in.</param>
        /// <returns>Retrieved data if exists otherwise null.</returns>
        public virtual async Task<TResult?> Get<TResult>(string key)
        {
            var value = await cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(value))
            {
                return default(TResult);
            }

            var result = JsonSerializer.Deserialize<TResult>(value);
            return result;
        }

        /// <summary>
        /// Sets a string in the cache with the specified key.
        /// </summary>
        /// <typeparam name="TData">The type of data to set.</typeparam>
        /// <param name="key">The key to store the data in.</param>
        /// <param name="data">The data to store in the cache.</param>
        /// <returns>A task that represents the asynchronous set operation.</returns>
        public virtual async Task Set<TData>(string key, TData data)
        {
            var value = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(key, value);
        }

        /// <summary>
        /// Deletes the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public virtual async Task Delete(string key)
        {
            await cache.RemoveAsync(key);
        }
    }
}
