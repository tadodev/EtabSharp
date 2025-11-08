namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

public class ModeShapeResults
{
    public int NumberResults { get; set; }
    public List<ModeShapeResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}