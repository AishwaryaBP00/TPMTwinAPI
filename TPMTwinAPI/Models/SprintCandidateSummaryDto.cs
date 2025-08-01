namespace TPMTwinAPI.Models
{
    public class SprintCandidateSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string[] Tags { get; set; } = Array.Empty<string>();
        public string Priority { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
        public string[] AIInsights { get; set; } = Array.Empty<string>();
    }
}
