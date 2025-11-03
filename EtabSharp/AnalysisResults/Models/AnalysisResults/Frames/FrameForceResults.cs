namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Frames;

/// <summary>
/// Contain a number of frameforce results.
/// </summary>
public class FrameForceResults
{
    public int NumberResults { get; set; }
    public List<FrameForceResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}