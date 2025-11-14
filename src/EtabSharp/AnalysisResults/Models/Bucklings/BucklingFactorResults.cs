namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Bucklings;

public class BucklingFactorResults
{
    public int NumberResults { get; set; }
    public List<BucklingFactorResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}