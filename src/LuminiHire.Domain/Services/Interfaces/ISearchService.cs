using LuminiHire.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuminiHire.Domain.Services.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<ScoreCard>> Get(long id, string query, int skip, int take);
    }
}
