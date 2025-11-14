namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;

public class JointAccelerationResults
{
    public int NumberResults { get; set; }
    public List<JointAccelerationResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}