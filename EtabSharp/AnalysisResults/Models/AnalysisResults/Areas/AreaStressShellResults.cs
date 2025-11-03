namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;

public class AreaStressShellResults
{
    public int NumberResults { get; set; }
    public List<AreaStressShellResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}