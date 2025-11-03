namespace EtabSharp.AnalysisResults.Models.AnalysisResults.StoryResults;

public class StoryDriftResults
{
    public int NumberResults { get; set; }
    public List<StoryDriftResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}