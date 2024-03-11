namespace MaccabiSearch.Application.Services
{
    /// <summary>
    /// Contract of the Cache Service
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Sets a string in the cache with the specified key.
        /// </summary>
        /// <typeparam name="TData">The type of data to set.</typeparam>
        /// <param name="key">The key to store the data in.</param>
        /// <param name="data">The data to store in the cache.</param>
        /// <returns>A task that represents the asynchronous set operation.</returns>
        Task Set<TData>(string key, TData data);

        /// <summary>
        /// Gets a string from the cache with the specified key.
        /// </summary>
        /// <typeparam name="TResult">The type of data retrieved from cache.</typeparam>
        /// <param name="key">The key to store the data in.</param>
        /// <returns>Retrieved data if exists otherwise null.</returns>
        Task<TResult?> Get<TResult>(string key);

        /// <summary>
        /// Deletes the value with the given key.
        /// </summary>
        /// <param name="key">A string identifying the requested value.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task Delete(string key);
    }
}
