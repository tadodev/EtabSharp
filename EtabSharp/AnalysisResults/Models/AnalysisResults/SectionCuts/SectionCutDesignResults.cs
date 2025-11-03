namespace EtabSharp.AnalysisResults.Models.AnalysisResults.SectionCuts;

public class SectionCutDesignResults
{
    public int NumberResults { get; set; }
    public List<SectionCutDesignResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}