namespace TPMTwinAPI.Models
{
    public class SprintCandidateDetailsDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AcceptanceCriteria { get; set; } = string.Empty;
        public string[] LinkedDocs { get; set; } = Array.Empty<string>();
    }
}
