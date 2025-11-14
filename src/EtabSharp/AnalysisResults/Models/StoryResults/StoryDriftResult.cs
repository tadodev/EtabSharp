namespace EtabSharp.AnalysisResults.Models.AnalysisResults.StoryResults;

/// <summary>
/// Represents story drift results.
/// </summary>
public class StoryDriftResult
{
    public string Story { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }
    public string Direction { get; set; } = string.Empty;
    public double Drift { get; set; }
    public string Label { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public override string ToString()
    {
        return $"Story Drift: {Story} - {LoadCase} - {Direction} - Drift={Drift:F6}";
    }
}