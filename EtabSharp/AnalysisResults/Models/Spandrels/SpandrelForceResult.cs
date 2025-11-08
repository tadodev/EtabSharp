namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Spandrels;

/// <summary>
/// Represents spandrel force results.
/// </summary>
public class SpandrelForceResult
{
    public string StoryName { get; set; } = string.Empty;
    public string SpandrelName { get; set; } = string.Empty;
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
        return $"Spandrel: {SpandrelName} @ {StoryName} - {LoadCase}";
    }
}