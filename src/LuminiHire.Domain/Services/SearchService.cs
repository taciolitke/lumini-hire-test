using LuminiHire.Domain.Entities;
using LuminiHire.Domain.Repositories;
using LuminiHire.Domain.Services.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuminiHire.Domain.Services
{
    public class SearchService: ISearchService
    {
        private readonly IElasticReaderRepository _elasticReaderRepository;

        public SearchService(IElasticReaderRepository elasticReaderRepository)
        {
            _elasticReaderRepository = elasticReaderRepository;
        }

        public async Task<IEnumerable<ScoreCard>> Get(long id, string query, int skip, int take)
        {
            return await _elasticReaderRepository.Get(id, query, skip, take);
        }
    }
}
