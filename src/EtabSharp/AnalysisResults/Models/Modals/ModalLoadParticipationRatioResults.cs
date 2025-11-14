namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

public class ModalLoadParticipationRatioResults
{
    public int NumberResults { get; set; }
    public List<ModalLoadParticipationRatioResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}