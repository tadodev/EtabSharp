namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;

public class JointDisplacementResults
{
    public int NumberResults { get; set; }
    public List<JointDisplacementResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}