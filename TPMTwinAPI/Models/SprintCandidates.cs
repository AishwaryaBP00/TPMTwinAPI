using System.ComponentModel.DataAnnotations;

namespace TPMTwinAPI.Models
{
    public class SprintCandidates
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string[] Tags { get; set; } = Array.Empty<string>();
        public string Priority { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
        public string[] AIInsights { get; set; } = Array.Empty<string>();
        public string Description { get; set; } = string.Empty;
        public string AcceptanceCriteria { get; set; } = string.Empty;
        public string[] LinkedDocs { get; set; } = Array.Empty<string>();
        public string Type { get; set; } = string.Empty;
        public string ParentItemId { get; set; } = string.Empty;
    }
}
