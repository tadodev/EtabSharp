namespace EtabSharp.AnalysisResults.Models.AnalysisResults.GeneralizedDisplacements;

public class GeneralizedDisplacementResults
{
    public int NumberResults { get; set; }
    public List<GeneralizedDisplacementResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}