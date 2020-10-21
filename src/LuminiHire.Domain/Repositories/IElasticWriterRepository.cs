
using LuminiHire.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuminiHire.Domain.Repositories
{
    public interface IElasticWriterRepository
    {
        Task<bool> Set(ScoreCard entity);

        Task<bool> Set(IEnumerable<ScoreCard> entities);

        Task<bool> IsAlive();
    }
}
