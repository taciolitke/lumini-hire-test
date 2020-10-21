using System.Net;
using System.Threading.Tasks;
using LuminiHire.Domain.Entities;
using LuminiHire.Domain.Services.Interfaces;
using LuminiHire.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LuminiHire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ScoreCard), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromQuery]ScoreCardFilter scoreCardFilter)
        {
            var result = await _searchService.Get(scoreCardFilter.UnitId, scoreCardFilter.Query, scoreCardFilter.Skip, scoreCardFilter.Take);

            return Ok(result);
        }
    }
}