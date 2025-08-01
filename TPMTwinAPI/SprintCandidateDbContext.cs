using Microsoft.EntityFrameworkCore;
using TPMTwinAPI.Models;

namespace TPMTwinAPI
{
    public class SprintCandidateDbContext : DbContext
    {
        public SprintCandidateDbContext(DbContextOptions<SprintCandidateDbContext> options) : base(options)
        {
        }

        public DbSet<SprintCandidates> SprintCandidates { get; set; }
    }
}
