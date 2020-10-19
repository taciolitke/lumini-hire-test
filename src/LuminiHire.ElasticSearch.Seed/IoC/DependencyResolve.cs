using LuminiHire.Domain.Repositories;
using LuminiHire.Infra.Repositories.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.IO;

namespace LuminiHire.ElasticSearch.Seed.IoC
{
    public static class DependencyResolve
    {
        public static ServiceProvider Resolve()
        {
            var serviceProvider = new ServiceCollection()
                    .ConfigureServices()
                    .AddConfigurationBuilder();

            return serviceProvider
                .BuildServiceProvider();
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
        {
            var configuration = ConfigurationBuilder();

            var settings = new ConnectionSettings(new Uri(configuration.GetSection("ElasticSearch:Uri").Value))
                                        .DefaultIndex(configuration.GetSection("ElasticSearch:DefaultIndex").Value);

            serviceCollection
                .AddSingleton<IElasticWriterRepository>(x => new ElasticRepository(settings));

            return serviceCollection;
        }

        private static IServiceCollection AddConfigurationBuilder(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton(ConfigurationBuilder());
        }

        private static IConfigurationRoot ConfigurationBuilder()
        {
            return new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();
        }
    }
}
