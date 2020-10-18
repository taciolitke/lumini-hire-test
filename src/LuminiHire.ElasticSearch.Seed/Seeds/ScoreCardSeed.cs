using CsvHelper;
using CsvHelper.Configuration.Attributes;
using LuminiHire.Domain.Repositories;
using LuminiHire.ElasticSearch.Seed.Model;
using System;
using System.Collections;
using System.Collections.Generic;
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
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        await SeedFile(entry);
                    }
                }
            }
        }

        private async Task SeedFile(ZipArchiveEntry zipArchiveEntry)
        {
            using (var stream = zipArchiveEntry.Open())
            {
                StreamReader reader = new StreamReader(stream);
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ScoreDataCsvModel>().ToList();

                    Parallel.ForEach(records, (item) => {
                        Repository.Set(item.ToScoreCard());
                    });
                }
            }
        }
    }

}
