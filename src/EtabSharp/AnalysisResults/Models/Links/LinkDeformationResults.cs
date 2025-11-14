namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Links;

public class LinkDeformationResults
{
    public int NumberResults { get; set; }
    public List<LinkDeformationResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}