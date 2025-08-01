using Microsoft.AspNetCore.Mvc;
using TPMTwinAPI.Services;
using TPMTwinAPI.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TPMTwinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintCandidatesController : ControllerBase
    {
        private readonly SprintCandidateDbContext _context;
        private readonly AdoQueryService _adoQuery;

        public SprintCandidatesController(SprintCandidateDbContext context, AdoQueryService adoQuery)
        {
            _context = context;
            _adoQuery = adoQuery;
        }

        // GET: api/SprintCandidates/summary
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<object>>> GetBasicItemInfo()
        {
            var items = (await _adoQuery.FetchSprintCandidatesAsync())
                .Select(x => new
                {
                    Id = x.Id,
                    x.Title,
                    x.Status,
                    x.Tags,
                    x.Priority,
                    LastUpdated = x.LastUpdated,
                    x.AIInsights
                })
                .ToList();
            return Ok(items);
        }

        // GET: api/SprintCandidates/details
        [HttpGet("details")]
        public ActionResult<IEnumerable<object>> GetItemDetails()
        {
            var items = _context.SprintCandidates
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Description,
                    x.AcceptanceCriteria,
                    x.LinkedDocs
                })
                .ToList();
            return Ok(items);
        }
        // POST: api/SprintCandidates
        [HttpPost]
        public ActionResult<Models.SprintCandidates> AddItem([FromBody] Models.SprintCandidates sprintCandidate)
        {
            if (sprintCandidate == null)
            {
                return BadRequest();
            }
            _context.SprintCandidates.Add(sprintCandidate);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBasicItemInfo), new { id = sprintCandidate.Id }, sprintCandidate);
        }
                // GET: api/SprintCandidates/search?query=yourstring
        [HttpGet("search")]
        public ActionResult<IEnumerable<Models.SprintCandidates>> SearchItem([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query string cannot be empty.");
            }
            var lowerQuery = query.ToLower();
            var results = _context.SprintCandidates
                .Where(x => x.Title.ToLower().Contains(lowerQuery) || x.Tags.Any(tag => tag.ToLower().Contains(lowerQuery)))
                .ToList();
            return Ok(results);
        }
    }
}
