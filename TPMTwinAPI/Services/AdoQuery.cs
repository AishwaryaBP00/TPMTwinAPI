using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TPMTwinAPI.Models;

namespace TPMTwinAPI.Services
{
    public class AdoQueryService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private readonly string _pat;

        public AdoQueryService(IConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient();
            _baseUrl = _config["AzureDevOps:BaseUrl"] ?? string.Empty;
            _pat = _config["AzureDevOps:PAT"] ?? string.Empty;
            if (!string.IsNullOrEmpty(_pat))
            {
                var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{_pat}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            }
        }

        public async Task<List<SprintCandidates>> FetchSprintCandidatesAsync()
        {
            // 1. Get work item IDs
            var wiqlUrl = $"{_baseUrl}/_apis/wit/wiql?api-version=7.0";
            var wiqlBody = new
            {
                query = "SELECT [System.Id], [System.Title] FROM WorkItems WHERE [System.WorkItemType] = 'User Story' ORDER BY [System.Id] ASC"
            };
            var wiqlContent = new StringContent(JsonSerializer.Serialize(wiqlBody), Encoding.UTF8, "application/json");
            var wiqlResp = await _httpClient.PostAsync(wiqlUrl, wiqlContent);
            wiqlResp.EnsureSuccessStatusCode();
            var wiqlJson = await wiqlResp.Content.ReadAsStringAsync();
            var wiqlResult = JsonSerializer.Deserialize<WiqlResponse>(wiqlJson);
            var ids = wiqlResult?.workItems?.Select(w => w.id).ToList() ?? new List<int>();
            if (!ids.Any()) return new List<SprintCandidates>();

            // 2. Get work item details
            var batchUrl = $"{_baseUrl}/_apis/wit/workitemsbatch?api-version=7.1";
            var batchBody = new
            {
                ids = ids,
                fields = new[]
                {
                    "System.Id",
                    "System.Title",
                    "System.State",
                    "System.Tags",
                    "Microsoft.VSTS.Common.Priority",
                    "System.WorkItemType"
                }
            };
            var batchContent = new StringContent(JsonSerializer.Serialize(batchBody), Encoding.UTF8, "application/json");
            var batchResp = await _httpClient.PostAsync(batchUrl, batchContent);
            batchResp.EnsureSuccessStatusCode();
            var batchJson = await batchResp.Content.ReadAsStringAsync();
            var batchResult = JsonSerializer.Deserialize<WorkItemsBatchResponse>(batchJson);

            // 3. Map to SprintCandidates
            var candidates = batchResult?.value?.Select(w => new SprintCandidates
            {
                ID = w.id,
                Title = w.fields?.GetValueOrDefault("System.Title")?.ToString() ?? string.Empty,
                Status = w.fields?.GetValueOrDefault("System.State")?.ToString() ?? string.Empty,
                Tags = w.fields?.GetValueOrDefault("System.Tags")?.ToString()?.Split(';', ',').Select(t => t.Trim()).Where(t => !string.IsNullOrEmpty(t)).ToArray() ?? Array.Empty<string>(),
                Priority = w.fields?.GetValueOrDefault("Microsoft.VSTS.Common.Priority")?.ToString() ?? string.Empty,
                LastUpdated = DateTime.UtcNow, // Not available in response, set to now
                AIInsights = Array.Empty<string>(),
                Description = string.Empty,
                AcceptanceCriteria = string.Empty,
                LinkedDocs = Array.Empty<string>()
            }).ToList() ?? new List<SprintCandidates>();

            return candidates;
        }

        // DTOs for deserialization
        private class WiqlResponse
        {
            public List<WiqlWorkItem> workItems { get; set; } = new();
        }
        private class WiqlWorkItem
        {
            public int id { get; set; }
        }
        private class WorkItemsBatchResponse
        {
            public int count { get; set; }
            public List<WorkItemDetail> value { get; set; } = new();
        }
        private class WorkItemDetail
        {
            public int id { get; set; }
            public int rev { get; set; }
            public Dictionary<string, object> fields { get; set; } = new();
        }
    }
}
