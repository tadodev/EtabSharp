namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;

/// <summary>
/// Represents joint reactions.
/// </summary>
public class JointReactionResult
{
    public string ObjectName { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    public double F1 { get; set; }
    public double F2 { get; set; }
    public double F3 { get; set; }
    public double M1 { get; set; }
    public double M2 { get; set; }
    public double M3 { get; set; }

    public override string ToString()
    {
        return $"Joint React: {ElementName} - {LoadCase} - F1={F1:F2}, F2={F2:F2}, F3={F3:F2}";
    }
}