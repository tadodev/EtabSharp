namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas.ShellLayered;

public class AreaStrainShellLayeredResults
{
    public int NumberResults { get; set; }
    public List<AreaStrainShellLayeredResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}