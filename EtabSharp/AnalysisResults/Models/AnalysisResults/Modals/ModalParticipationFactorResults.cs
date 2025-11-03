namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

public class ModalParticipationFactorResults
{
    public int NumberResults { get; set; }
    public List<ModalParticipationFactorResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}