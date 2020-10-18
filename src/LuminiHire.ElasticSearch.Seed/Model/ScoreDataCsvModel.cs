
using CsvHelper.Configuration.Attributes;
using LuminiHire.Domain.Entities;

namespace LuminiHire.ElasticSearch.Seed.Model
{
    public class ScoreDataCsvModel
    {

        [Name("UNITID")]
        public long UnitId { get; set; }

        [Name("INSTNM")]
        public string Instituition { get; set; }

        [Name("CITY")]
        public string City { get; set; }

        [Name("ZIP")]
        public string Zip { get; set; }

        public ScoreCard ToScoreCard() => new ScoreCard()
        {
            UnitId = UnitId,
            Instituition = Instituition,
            City = City,
            Zip = Zip,
        };
    }
}
