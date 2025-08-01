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
                var existingCandidates = _context.SprintCandidates.ToDictionary(x => x.Id);
                foreach (var candidate in candidates)
                {
                    if (existingCandidates.TryGetValue(candidate.Id, out var existing))
                    {
                        // Update all properties
                        existing.Title = candidate.Title;
                        existing.Status = candidate.Status;
                        existing.Tags = candidate.Tags;
                        existing.Priority = candidate.Priority;
                        existing.LastUpdated = candidate.LastUpdated;
                        existing.AIInsights = candidate.AIInsights;
                        existing.Description = candidate.Description;
                        existing.AcceptanceCriteria = candidate.AcceptanceCriteria;
                        existing.LinkedDocs = candidate.LinkedDocs;
                        existing.Type = candidate.Type;
                        existing.ParentItemId = candidate.ParentItemId;
                    }
                    else
                    {
                        _context.SprintCandidates.Add(candidate);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
