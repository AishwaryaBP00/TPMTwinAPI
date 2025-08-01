using System.Collections.Generic;
using System.Threading.Tasks;
using TPMTwinAPI.Models;
using TPMTwinAPI.Database;

namespace TPMTwinAPI.Services
{
    public class SprintCandidateService
    {
        private readonly SprintCandidateDbContext _context;
        private readonly AdoQueryService _adoQueryService;

        public SprintCandidateService(SprintCandidateDbContext context, AdoQueryService adoQueryService)
        {
            _context = context;
            _adoQueryService = adoQueryService;
        }

        public async Task FetchAndAddSprintCandidatesAsync()
        {
            List<SprintCandidates> candidates = await _adoQueryService.FetchSprintCandidatesAsync();
            if (candidates != null && candidates.Count > 0)
            {
                var existingIds = _context.SprintCandidates.Select(x => x.Id).ToHashSet();
                var newCandidates = candidates.Where(c => !existingIds.Contains(c.Id)).ToList();
                if (newCandidates.Count > 0)
                {
                    _context.SprintCandidates.AddRange(newCandidates);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
