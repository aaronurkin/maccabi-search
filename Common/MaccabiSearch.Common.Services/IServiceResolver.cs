namespace MaccabiSearch.Common.Services
{
    /// <summary>
    /// Contract of the service resolving services registered within the DI container
    /// </summary>
    public interface IServiceResolver
    {
        /// <summary>
        /// Resolves service of type <typeparamref name="TService"/> from the DI container.
        /// </summary>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        /// <returns>A service object of type <typeparamref name="TService"/> or null if there is no such service.</returns>
        TService? Resolve<TService>();

        /// <summary>
        /// Resolves service of type <typeparamref name="TService"/> from the DI container by a key.
        /// </summary>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        /// <param name="key">An object that specifies the key of service object to get.</param>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        TService? Resolve<TService>(object? key);
    }
}