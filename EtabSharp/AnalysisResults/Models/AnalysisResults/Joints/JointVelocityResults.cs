namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;

public class JointVelocityResults
{
    public int NumberResults { get; set; }
    public List<JointVelocityResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}