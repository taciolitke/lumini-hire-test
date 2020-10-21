using LuminiHire.Domain.Repositories;
using LuminiHire.Domain.Services;
using LuminiHire.Domain.Services.Interfaces;
using LuminiHire.Infra.Repositories.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace LuminiHire.IoC
{
    public static class DependencyResolver
    {

        public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection
                .ConfigureServices()
                .ConfigureRepositories(configuration);
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<ISearchService, SearchService>();

            return serviceCollection;
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration.GetSection("ElasticSearch:Uri").Value))
                                        .DefaultIndex(configuration.GetSection("ElasticSearch:DefaultIndex").Value);

            serviceCollection
                .AddSingleton<IElasticReaderRepository>(x => new ElasticRepository(settings));

            return serviceCollection;
        }
    }
}
