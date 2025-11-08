namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Frames;

/// <summary>
/// Contain a number of frame joint force results.
/// </summary>
public class FrameJointForceResults
{
    public int NumberResults { get; set; }
    public List<FrameJointForceResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}