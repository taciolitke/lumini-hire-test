using LuminiHire.Domain.Entities;
using LuminiHire.Domain.Repositories;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuminiHire.Infra.Repositories.Cache
{
    public class ElasticRepository: IElasticReaderRepository, IElasticWriterRepository
    {
        private readonly ElasticClient _elasticClient;
        private readonly IndexName _defaultIndex;

        public ElasticRepository(ConnectionSettings connectionSettings)
        {
            _elasticClient = new ElasticClient(connectionSettings);
            _defaultIndex = _elasticClient.ConnectionSettings.DefaultIndex;
        }

        public async Task<bool> Set(ScoreCard entity)
        {
            var result = await _elasticClient
                .IndexDocumentAsync(entity);

            return result.IsValid;
        }

        public async Task<bool> IsAlive()
        {
            var result = await _elasticClient.PingAsync();

            return result.IsValid;
        }

        public async Task<bool> Set(IEnumerable<ScoreCard> entities)
        {
            var result = await _elasticClient.BulkAsync(b => b
                                                .Index(_defaultIndex)
                                                .IndexMany(entities)
                                            );

            return result.IsValid;
        }

        public async Task<IReadOnlyCollection<ScoreCard>> Get(long id, string query, int skip, int take)
        {
            var filters = new List<Func<QueryContainerDescriptor<ScoreCard>, QueryContainer>>();

            if (id != 0)
            {
                filters.Add(fq => fq.Terms(t => t.Field(f => f.UnitId).Terms(id.ToString())));
            }

            var searchResponse = await _elasticClient.SearchAsync<ScoreCard>(s => s
                                                .Query(q => q
                                                .MultiMatch(mm => mm
                                                    .Fields(f => f
                                                        .Field(ff => ff.City)
                                                        .Field(ff => ff.Zip)
                                                        .Field(ff => ff.Instituition)
                                                    )
                                                    .Type(TextQueryType.PhrasePrefix)
                                                    .Query(query)
                                                    .MaxExpansions(10)
                                                )
                                                && q.Bool(b => b.Filter(filters))
                                            )
                                            .From(skip)
                                            .Size(take));

            return searchResponse.Documents;
        }
    }
}
