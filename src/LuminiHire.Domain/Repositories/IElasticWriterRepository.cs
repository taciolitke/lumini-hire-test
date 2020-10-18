
using LuminiHire.Domain.Entities;
using System.Threading.Tasks;

namespace LuminiHire.Domain.Repositories
{
    public interface IElasticWriterRepository
    {
        Task<bool> Set(ScoreCard entity);
    }
}
