using MaccabiSearch.Common.Services;
using MaccabiSearch.Common.Services.Implementations;
using MaccabiSearch.Domain.Models;
using MaccabiSearch.Infrastructure.Models;
using MaccabiSearch.Infrastructure.Models.GoogleSearchEngine;
using MaccabiSearch.Infrastructure.Services.Implementations.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;

namespace MaccabiSearch.Infrastructure.Services.Implementations.Tests
{
    [TestClass]
    public class GoogleSimpleSearchApiClientIntegrationTests
    {
        private readonly IServiceCollection services;

        public GoogleSimpleSearchApiClientIntegrationTests()
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
                .AddSingleton<IModelMapper<GoogleSearchEngineResponse, IEnumerable<SearchResult>>, GoogleSearchEngineResponseSearchResultMapper>();
        }

        [TestMethod]
        public async Task Search_ReturnsResults_Successfully()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Constants.GoogleSearchEngineBaseUrl);

                // Arrange
                var options = new GoogleSimpleSearchApiClientOptions
                {
                    ApiKey = Constants.GoogleSearchEngineApiKey,
                    DefaultPageSize = Constants.DefaultPageSize,
                    DefaultPageNumber = Constants.DefaultPageNumber,
                    SearchEngineId = Constants.GoogleSearchEngineId,
                    SearchEndpointTemplate = string.Format(Constants.GoogleSearchEngineEndpointFormat, Constants.GoogleSearchEngineApiKey, Constants.GoogleSearchEngineId)
                };
                var serviceResolver = new BuiltInServiceResolver(services.BuildServiceProvider());

                var target = new GoogleSimpleSearchApiClient(httpClient, serviceResolver, options);

                // Act
                var searchDto = new SearchDto { Query = "Test Search", PageSize = Constants.DefaultPageSize };
                var actual = await target.Search(searchDto);

                // Assert
                Assert.IsNotNull(actual);
                Assert.IsNotNull(actual.Data);
                Assert.IsTrue(actual.Status == Common.Models.ServiceResultStatus.Succeeded);

                var actualSearchResults = actual.Data as IEnumerable<SearchResult>;

                Assert.IsNotNull(actualSearchResults);
                Assert.AreEqual(Constants.DefaultPageSize, actualSearchResults.Count());
            }
        }
    }
}
