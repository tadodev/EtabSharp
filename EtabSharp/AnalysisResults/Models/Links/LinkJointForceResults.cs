namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Links;

public class LinkJointForceResults
{
    public int NumberResults { get; set; }
    public List<LinkJointForceResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}