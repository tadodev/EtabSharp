namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Links;

public class LinkForceResults
{
    public int NumberResults { get; set; }
    public List<LinkForceResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}