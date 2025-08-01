using Microsoft.AspNetCore.Mvc;

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

        // GET api/<SPrintCandidatesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SPrintCandidatesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SPrintCandidatesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SPrintCandidatesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
