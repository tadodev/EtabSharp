namespace EtabSharp.AnalysisResults.Models.AnalysisResults.PanelZones;

/// <summary>
/// Represents panel zone force results.
/// </summary>
public class PanelZoneForceResult
{
    public string ElementName { get; set; } = string.Empty;
    public string PointElement { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    public double P { get; set; }
    public double V2 { get; set; }
    public double V3 { get; set; }
    public double T { get; set; }
    public double M2 { get; set; }
    public double M3 { get; set; }
}