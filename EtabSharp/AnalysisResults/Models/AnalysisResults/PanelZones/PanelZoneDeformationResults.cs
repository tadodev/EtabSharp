namespace EtabSharp.AnalysisResults.Models.AnalysisResults.PanelZones;

public class PanelZoneDeformationResults
{
    public int NumberResults { get; set; }
    public List<PanelZoneDeformationResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}