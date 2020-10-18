using LuminiHire.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuminiHire.Domain.Repositories
{
    public interface IElasticReaderRepository
    {
        Task<IReadOnlyCollection<ScoreCard>> Get(string search, int skip, int take);
    }
}
