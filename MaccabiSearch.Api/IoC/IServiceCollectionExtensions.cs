﻿using MaccabiSearch.Application.Models;
using MaccabiSearch.Application.Services;
using MaccabiSearch.Application.Services.Implementations;
using MaccabiSearch.Common.Models;
using MaccabiSearch.Common.Services;
using MaccabiSearch.Common.Services.Implementations;
using MaccabiSearch.Domain.Models;
using MaccabiSearch.Domain.Services;
using MaccabiSearch.Domain.Services.Implementations;
using MaccabiSearch.Domain.Services.Implementations.EntityFramework;
using MaccabiSearch.Infrastructure.Models;
using MaccabiSearch.Infrastructure.Models.GoogleSearchEngine;
using MaccabiSearch.Infrastructure.Services;
using MaccabiSearch.Infrastructure.Services.Implementations;
using MaccabiSearch.Infrastructure.Services.Implementations.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Text.Json;

namespace MaccabiSearch.Api.IoC
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCommonSingletonServices(configuration)
                .AddCommonScopedServices(configuration)
                .AddCommonTransientServices(configuration);

            services
                .AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration.GetConnectionString("RedisCache");
                });

            services
                .AddDbContext<MaccabiSearchDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("MaccabiSearchDB"));
                });

            services
                .AddHttpClient<ISearchEngineApiClient, GoogleSimpleSearchApiClient>(client =>
                {
                    var url = configuration.GetValue<string>("GoogleSearchEngine:Endpoint:Search:BaseUrl");
                    client.BaseAddress = new Uri(url!);
                });

            return services;
        }

        private static IServiceCollection AddCommonSingletonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

            var searchDefaultPageSize = configuration.GetValue<int>("Search:PageSize");
            var searchDefaultPageNumber = configuration.GetValue<int>("Search:PageNumber");
            var googleSearchEngineId = configuration.GetValue<string>("GoogleSearchEngine:Id");
            var googleSearchEngineApiKey = configuration.GetValue<string>("GoogleSearchEngine:ApiKey");
            var googleSearchEngineSearchEndpointTemplate = string.Format(
                configuration.GetValue<string>("GoogleSearchEngine:Endpoint:Search:Template")!,
                googleSearchEngineApiKey,
                googleSearchEngineId
            );

            services
                .AddSingleton<IGoogleSimpleSearchApiClientOptions>(new GoogleSimpleSearchApiClientOptions
                {
                    ApiKey = googleSearchEngineApiKey,
                    SearchEngineId = googleSearchEngineId,
                    DefaultPageSize = searchDefaultPageSize,
                    DefaultPageNumber = searchDefaultPageNumber,
                    SearchEndpointTemplate = googleSearchEngineSearchEndpointTemplate
                });

            services
                .AddSingleton<IDictionary<ServiceResultStatus, int>>(new Dictionary<ServiceResultStatus, int>
                {
                    { ServiceResultStatus.Succeeded, StatusCodes.Status200OK},
                    { ServiceResultStatus.NotFound, StatusCodes.Status404NotFound },
                    { ServiceResultStatus.Forbidden, StatusCodes.Status403Forbidden },
                    { ServiceResultStatus.InvalidInput, StatusCodes.Status400BadRequest },
                    { ServiceResultStatus.Failed, StatusCodes.Status422UnprocessableEntity },
                    { ServiceResultStatus.AlreadyExisting, StatusCodes.Status400BadRequest },
                });

            services
                .AddSingleton<ICacheService, RedisCacheService>();

            services
                .AddSingleton<IGoogleSearchResponseResolver, GoogleSearchDefaultFailedResponseResolver>();

            services
                .AddKeyedSingleton<IGoogleSearchResponseResolver, GoogleSearchOkResponseResolver>(HttpStatusCode.OK);

            services
                .AddSingleton<IModelMapper<SearchResult, SearchResultPgEntity>, SearchResultPgEntityMapper>();

            services
                .AddSingleton<IModelMapper<GoogleSearchEngineResponse, IEnumerable<SearchResult>>, GoogleSearchEngineResponseSearchResultMapper>();

            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        private static IServiceCollection AddCommonScopedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IRequestMetadata>(serviceProvider =>
                {
                    var trackingId = default(StringValues);
                    var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

                    accessor?.HttpContext?.Request.Headers.TryGetValue("Tracking-Id", out trackingId);

                    return new RequestMetadata(trackingId.ToString());
                });

            services
                .AddScoped<ISearchService, SearchResultsManager>();

            services
                .AddScoped<IRepository<SearchResultPgEntity>, SearchResultRepository>();
            return services;
        }

        private static IServiceCollection AddCommonTransientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IServiceResolver, BuiltInServiceResolver>();
            return services;
        }
    }
}
