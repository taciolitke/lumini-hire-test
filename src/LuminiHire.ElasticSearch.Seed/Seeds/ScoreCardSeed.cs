using CsvHelper;
using CsvHelper.Configuration.Attributes;
using LuminiHire.Domain.Repositories;
using LuminiHire.ElasticSearch.Seed.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace LuminiHire.ElasticSearch.Seed.Seeds
{
    public class ScoreCardSeed
    {
        public string ZipFilePath { get; protected set; }

        public string ZipFileName { get; protected set; }

        public IElasticWriterRepository Repository { get; protected set; }

        public string ZipFullPath { get => $"{ZipFilePath}{Path.DirectorySeparatorChar}{ZipFileName}"; }

        public static ScoreCardSeed Create(string zipPath, string zipFileName, IElasticWriterRepository repository) => new ScoreCardSeed()
        {
            ZipFilePath = zipPath,
            ZipFileName = zipFileName,
            Repository = repository
        };

        public async Task Seed()
        {
            await ProcessFileList();
        }

        private async Task ProcessFileList()
        {
            using (ZipArchive archive = ZipFile.OpenRead(ZipFullPath))
            {
                var files = archive.Entries.Where(x => x.Name.ToLower().EndsWith("csv"));
                var tasks = files.Select(x => SeedFile(x));

                await Task.WhenAll(tasks);
            }
        }

        private async Task SeedFile(ZipArchiveEntry zipArchiveEntry)
        {
            using (var stream = zipArchiveEntry.Open())
            {
                StreamReader reader = new StreamReader(stream);
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ScoreDataCsvModel>();
                    await SendRecords(records, zipArchiveEntry.Name);
                }
            }
        }

        private async Task<bool> SendRecords(IEnumerable<ScoreDataCsvModel> records, string filename)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            var recordsParsed = records.Select(x => x.ToScoreCard());

            var status = await Repository.Set(recordsParsed);

            stopwatch.Stop();

            var statusLabel = status ? "sucesso": "falha";

            Console.WriteLine($"Arquivo \"{filename}\" enviado com {statusLabel} em { stopwatch.ElapsedMilliseconds / 1000 } segundos.");

            return status;
        }
    }

}
