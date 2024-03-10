using MaccabiSearch.Common.Services;
using MaccabiSearch.Common.Services.Implementations;
using MaccabiSearch.Domain.Models;
using MaccabiSearch.Infrastructure.Models;
using MaccabiSearch.Infrastructure.Models.GoogleSearchEngine;
using MaccabiSearch.Infrastructure.Services;
using MaccabiSearch.Infrastructure.Services.Implementations;
using MaccabiSearch.Infrastructure.Services.Implementations.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;

namespace MaccabiSearch.Application.Services.Implementations.Tests
{
    [TestClass]
    public class AllEnginesSearchServiceIntegrationTests
    {
        private readonly IServiceCollection services;

        public AllEnginesSearchServiceIntegrationTests()
        {
            services = new ServiceCollection();
            services
                .AddSingleton<IGoogleSearchResponseResolver, GoogleSearchDefaultFailedResponseResolver>()
                .AddKeyedSingleton<IGoogleSearchResponseResolver, GoogleSearchOkResponseResolver>(HttpStatusCode.OK);
            services
                .AddSingleton(new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
            services
                .AddSingleton<IGoogleSimpleSearchApiClientOptions>(new GoogleSimpleSearchApiClientOptions
                {
                    ApiKey = Constants.GoogleSearchEngineApiKey,
                    DefaultPageSize = Constants.DefaultPageSize,
                    DefaultPageNumber = Constants.DefaultPageNumber,
                    SearchEngineId = Constants.GoogleSearchEngineId,
                    SearchEndpointTemplate = string.Format(Constants.GoogleSearchEngineEndpointFormat, Constants.GoogleSearchEngineApiKey, Constants.GoogleSearchEngineId)
                });
            services
                .AddSingleton<IModelMapper<GoogleSearchEngineResponse, IEnumerable<SearchResult>>, GoogleSearchEngineResponseSearchResultMapper>();
            services
                .AddTransient<IServiceResolver>(provider => new BuiltInServiceResolver(provider));

            services
                .AddHttpClient<ISearchEngineApiClient, GoogleSimpleSearchApiClient>(client =>
                {
                    client.BaseAddress = new Uri(Constants.GoogleSearchEngineBaseUrl);
                });
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