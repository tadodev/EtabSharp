namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;

/// <summary>
/// Represents joint velocities.
/// </summary>
public class JointVelocityResult
{
    public string ObjectName { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    public double U1 { get; set; }
    public double U2 { get; set; }
    public double U3 { get; set; }
    public double R1 { get; set; }
    public double R2 { get; set; }
    public double R3 { get; set; }
}