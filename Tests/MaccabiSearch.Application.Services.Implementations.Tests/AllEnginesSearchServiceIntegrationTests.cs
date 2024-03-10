using MaccabiSearc.Tests.Utils;
using MaccabiSearch.Infrastructure.Models;
using MaccabiSearch.Infrastructure.Services;
using MaccabiSearch.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace MaccabiSearch.Application.Services.Implementations.Tests
{
    [TestClass]
    public class AllEnginesSearchServiceIntegrationTests
    {
        private readonly IServiceCollection services;

        public AllEnginesSearchServiceIntegrationTests()
        {
            services = new ServiceCollection().AddTestsServices();
        }

        [TestMethod]
        public async Task Search_ReturnsResults_Successfully()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Constants.GoogleSearchEngineBaseUrl);

                var serviceProvider = services.BuildServiceProvider();
                var clients = serviceProvider.GetServices<ISearchEngineApiClient>();

                var target = new AllEnginesSearchService(clients);
                var actual = await target.Search(new SearchDto { Query = "Test Search" });
                Assert.IsNotNull(actual);
            }
        }
    }
}