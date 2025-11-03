namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

public class ModalPeriodResults
{
    public int NumberResults { get; set; }
    public List<ModalPeriodResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}