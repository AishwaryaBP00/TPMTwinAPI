using Microsoft.AspNetCore.Mvc;
using TPMTwinAPI.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TPMTwinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintCandidatesController : ControllerBase
    {
        private readonly SprintCandidateDbContext _context;

        public SprintCandidatesController(SprintCandidateDbContext context)
        {
            _context = context;
        }

        // GET: api/<SPrintCandidatesController>
        [HttpGet]
        public ActionResult<IEnumerable<Models.SprintCandidates>> Get()
        {
            var items = _context.SprintCandidates.ToList();
            return Ok(items);
        }
    }
}
