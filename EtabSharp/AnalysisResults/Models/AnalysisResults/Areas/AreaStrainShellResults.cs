namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;

public class AreaStrainShellResults
{
    public int NumberResults { get; set; }
    public List<AreaStrainShellResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}