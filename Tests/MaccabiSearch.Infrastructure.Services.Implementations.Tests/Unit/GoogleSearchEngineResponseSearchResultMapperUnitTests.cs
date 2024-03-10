using MaccabiSearch.Infrastructure.Models.GoogleSearchEngine;
using MaccabiSearch.Infrastructure.Services.Implementations.Mappers;

namespace MaccabiSearch.Infrastructure.Services.Implementations.Tests
{
    [TestClass]
    public class GoogleSearchEngineResponseSearchResultMapperUnitTests
    {
        private const int DefaultPageSize = 5;

        [TestMethod]
        public void Map_MapsSearchResult_Successfully()
        {
            // Arrange
            var gseResponse = new GoogleSearchEngineResponse
            {
                Items = Enumerable.Range(1, DefaultPageSize)
                    .Select(index => new GoogleSearchEngineResponseItem
                    {
                        Title = $"Test Item #{index}"
                    }),
            };
            var target = new GoogleSearchEngineResponseSearchResultMapper();

            // Act
            var actual = target.Map(gseResponse);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(gseResponse.Items.Count(), actual.Count());
            Assert.IsTrue(actual.All(item => gseResponse.Items.Any(expected => expected.Title == item.Title)));
        }
    }
}