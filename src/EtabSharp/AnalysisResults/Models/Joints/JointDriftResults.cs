namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;

public class JointDriftResults
{
    public int NumberResults { get; set; }
    public List<JointDriftResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}