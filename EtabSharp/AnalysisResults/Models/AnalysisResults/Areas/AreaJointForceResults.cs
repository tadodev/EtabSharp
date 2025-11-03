namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;

public class AreaJointForceResults
{
    public int NumberResults { get; set; }
    public List<AreaJointForceResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}