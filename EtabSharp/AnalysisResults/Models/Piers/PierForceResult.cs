namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Piers;

/// <summary>
/// Represents pier force results.
/// </summary>
public class PierForceResult
{
    public string StoryName { get; set; } = string.Empty;
    public string PierName { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public double P { get; set; }
    public double V2 { get; set; }
    public double V3 { get; set; }
    public double T { get; set; }
    public double M2 { get; set; }
    public double M3 { get; set; }

    public override string ToString()
    {
        return $"Pier: {PierName} @ {StoryName} - {LoadCase} - P={P:F2}, V2={V2:F2}";
    }
}