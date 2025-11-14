namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Spandrels;

public class SpandrelForceResults
{
    public int NumberResults { get; set; }
    public List<SpandrelForceResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}