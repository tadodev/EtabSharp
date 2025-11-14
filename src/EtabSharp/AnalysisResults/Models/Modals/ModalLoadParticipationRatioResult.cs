namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

/// <summary>
/// Represents modal load participation ratios.
/// </summary>
public class ModalLoadParticipationRatioResult
{
    public string LoadCase { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty;
    public string Item { get; set; } = string.Empty;
    public double Static { get; set; }
    public double Dynamic { get; set; }
}