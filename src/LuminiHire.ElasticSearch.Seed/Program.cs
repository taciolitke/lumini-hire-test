using LuminiHire.Domain.Repositories;
using LuminiHire.ElasticSearch.Seed.IoC;
using LuminiHire.ElasticSearch.Seed.Seeds;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace LuminiHire.ElasticSearch.Seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServiceProvider serviceProvider = DependencyResolve.Resolve();

            IElasticWriterRepository _cacheService = serviceProvider.GetService<IElasticWriterRepository>();

            Console.WriteLine("Carga Iniciada!");

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            var zipPath = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}sample_data{Path.DirectorySeparatorChar}";
            var zipName = "CollegeScorecard_Raw_Data.zip";

            var seeder = ScoreCardSeed.Create(zipPath, zipName, _cacheService);

            await seeder.Seed();

            stopwatch.Stop();

            Console.WriteLine($"Tempo total da carga: { stopwatch.ElapsedMilliseconds / 1000 } segundos.");
            Console.WriteLine("Carga Finalizada!");
        }
    }
}
