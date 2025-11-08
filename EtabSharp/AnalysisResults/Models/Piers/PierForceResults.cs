namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Piers;

public class PierForceResults
{
    public int NumberResults { get; set; }
    public List<PierForceResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}