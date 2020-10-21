using LuminiHire.Domain.Entities;

namespace LuminiHire.Requests
{
    public class ScoreCardFilter : PaginationFilter
    {
        public long UnitId { get; set; }

        public string Query { get; set; }
    }
}
