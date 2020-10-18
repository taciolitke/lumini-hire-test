﻿using LuminiHire.Domain.Entities;
using LuminiHire.Domain.Repositories;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuminiHire.Infra.Repositories.Cache
{
    public class ElasticRepository: IElasticReaderRepository, IElasticWriterRepository
    {
        private readonly ElasticClient _elasticClient;

        public ElasticRepository(ConnectionSettings connectionSettings)
        {
            _elasticClient = new ElasticClient(connectionSettings);
        }

        public async Task<bool> Set(ScoreCard entity)
        {
            var result = await _elasticClient.IndexDocumentAsync(entity);

            return result.IsValid;
        }

        public async Task<IReadOnlyCollection<ScoreCard>> Get(string search, int skip, int take)
        {
            var searchResponse = await _elasticClient.SearchAsync<ScoreCard>(s => s
                                            .From(skip)
                                            .Size(take)
                                            .Query(q => q
                                                 .Match(m => m
                                                    .Query(search)
                                                 )));

            return searchResponse.Documents;
        }
    }
}