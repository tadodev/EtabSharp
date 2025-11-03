namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Joints;

/// <summary>
/// Represents joint displacements.
/// </summary>
public class JointDisplacementResult
{
    public string ObjectName { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    /// <summary>
    /// U1 - Translation in global X direction.
    /// </summary>
    public double U1 { get; set; }

    /// <summary>
    /// U2 - Translation in global Y direction.
    /// </summary>
    public double U2 { get; set; }

    /// <summary>
    /// U3 - Translation in global Z direction.
    /// </summary>
    public double U3 { get; set; }

    /// <summary>
    /// R1 - Rotation about global X axis.
    /// </summary>
    public double R1 { get; set; }

    /// <summary>
    /// R2 - Rotation about global Y axis.
    /// </summary>
    public double R2 { get; set; }

    /// <summary>
    /// R3 - Rotation about global Z axis.
    /// </summary>
    public double R3 { get; set; }

    public override string ToString()
    {
        return $"Joint Displ: {ElementName} - {LoadCase} - U1={U1:F4}, U2={U2:F4}, U3={U3:F4}";
    }
}