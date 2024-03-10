using Microsoft.Extensions.DependencyInjection;

namespace MaccabiSearch.Common.Services.Implementations
{
    /// <summary>
    /// Implementation resolving services registered within the <see cref="IServiceCollection"/>
    /// </summary>
    public class BuiltInServiceResolver : IServiceResolver
    {
        private readonly IServiceProvider serviceProvider;

        public BuiltInServiceResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        /// Resolves service of type <typeparamref name="TService"/> from the DI container.
        /// </summary>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        /// <returns>A service object of type <typeparamref name="TService"/> or null if there is no such service.</returns>
        public TService? Resolve<TService>()
        {
            var service = serviceProvider.GetService<TService>();
            return service;
        }

        /// <summary>
        /// Resolves service of type <typeparamref name="TService"/> from the DI container by a key.
        /// </summary>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        /// <param name="key">An object that specifies the key of service object to get.</param>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        public TService? Resolve<TService>(object? key)
        {
            var service = serviceProvider.GetKeyedService<TService>(key);
            if (service == null)
            {
                service = serviceProvider.GetService<TService>();
            }

            return service;
        }
    }
}
