using MaccabiSearch.Common.Models;
using MaccabiSearch.Domain.Models;
using MaccabiSearch.Infrastructure.Models.GoogleSearchEngine;
using MaccabiSearch.Infrastructure.Services.Implementations.Mappers;
using NSubstitute;
using System.Text.Json;

namespace MaccabiSearch.Infrastructure.Services.Implementations.Tests
{
    [TestClass]
    public class GoogleSearchOkResponseResolverUnitTests
    {
        private readonly string googleSearchEngineResponse;
        private const int googleSearchEngineResponseItemsCount = 5;
        private readonly IEnumerable<GoogleSearchEngineResponseItem> googleSearchEngineResponseItems;

        public GoogleSearchOkResponseResolverUnitTests()
        {
            this.googleSearchEngineResponseItems = Enumerable
                .Range(1, googleSearchEngineResponseItemsCount)
                .Select(index => new GoogleSearchEngineResponseItem { Title = $"Test Google Search Engine Response Item #{index}" });

            var googleSearchEngineResponseItemsJson = string.Join(",", googleSearchEngineResponseItems.Select(item => "{\"title\":\"" + item.Title + "\"}"));
            this.googleSearchEngineResponse = "{\"kind\":\"customsearch#search\",\"context\":{\"title\":\"Maccabi Home Assignment\"},\"items\":[" + googleSearchEngineResponseItemsJson + "]}";
        }

        [TestMethod]
        public async Task Search_ReturnsResults_Successfully()
        {
            // Arrange
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var mapper = new GoogleSearchEngineResponseSearchResultMapper();

            var target = new GoogleSearchOkResponseResolver(jsonSerializerOptions, mapper);

            // Act
            var httpResponseMessage = Substitute.For<HttpResponseMessage>();
            httpResponseMessage.Content = new StringContent(googleSearchEngineResponse);

            var actual = await target.Resolve(httpResponseMessage);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType<IServiceResult>(actual);
            Assert.IsInstanceOfType<IEnumerable<SearchResult>>(actual.Data);

            var actualSearchResults = actual.Data as IEnumerable<SearchResult>;

            Assert.IsNotNull(actualSearchResults);
            Assert.IsTrue(actualSearchResults.Any());
            Assert.AreEqual(googleSearchEngineResponseItemsCount, actualSearchResults.Count());
            Assert.IsTrue(actualSearchResults.All(item => googleSearchEngineResponseItems.Any(expected => expected.Title == item.Title)));
        }
    }
}