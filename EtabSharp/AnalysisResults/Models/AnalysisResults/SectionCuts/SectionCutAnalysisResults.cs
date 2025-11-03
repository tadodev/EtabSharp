namespace EtabSharp.AnalysisResults.Models.AnalysisResults.SectionCuts;

public class SectionCutAnalysisResults
{
    public int NumberResults { get; set; }
    public List<SectionCutAnalysisResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}