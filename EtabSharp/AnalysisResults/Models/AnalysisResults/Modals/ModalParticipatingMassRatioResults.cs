namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

public class ModalParticipatingMassRatioResults
{
    public int NumberResults { get; set; }
    public List<ModalParticipatingMassRatioResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}