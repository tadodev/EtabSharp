namespace EtabSharp.AnalysisResults.Models.AnalysisResults.PanelZones;

public class PanelZoneForceResults
{
    public int NumberResults { get; set; }
    public List<PanelZoneForceResult> Results { get; set; } = new();
    public bool IsSuccess { get; set; }
    public int ReturnCode { get; set; }
    public string? ErrorMessage { get; set; }
}