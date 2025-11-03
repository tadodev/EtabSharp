namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Frames;

/// <summary>
/// Represents frame joint forces.
/// </summary>
public class FrameJointForceResult
{
    public string ObjectName { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public string PointElement { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    public double F1 { get; set; }
    public double F2 { get; set; }
    public double F3 { get; set; }
    public double M1 { get; set; }
    public double M2 { get; set; }
    public double M3 { get; set; }
}