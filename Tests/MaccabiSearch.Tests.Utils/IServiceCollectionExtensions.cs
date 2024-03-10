using MaccabiSearc.Tests.Utils;
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

namespace MaccabiSearch.Tests.Utils
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTestsServices(this IServiceCollection services)
        {
            services
                .AddSingleton<IMockService, MockService>();

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
            return services;
        }
    }
}
