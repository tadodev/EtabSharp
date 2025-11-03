namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;

/// <summary>
/// Represents joint drift results.
/// </summary>
public class JointDriftResult
{
    public string Story { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }
    public double DisplacementX { get; set; }
    public double DisplacementY { get; set; }
    public double DriftX { get; set; }
    public double DriftY { get; set; }

    public override string ToString()
    {
        return $"Joint Drift: {Name} @ {Story} - {LoadCase} - DX={DriftX:F6}, DY={DriftY:F6}";
    }
}