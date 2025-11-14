namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;

/// <summary>
/// Represents joint forces for area elements.
/// </summary>
public class AreaJointForceResult
{
    public string ObjectName { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public string PointElement { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    /// <summary>
    /// Force in local 1 direction.
    /// </summary>
    public double F1 { get; set; }

    /// <summary>
    /// Force in local 2 direction.
    /// </summary>
    public double F2 { get; set; }

    /// <summary>
    /// Force in local 3 direction.
    /// </summary>
    public double F3 { get; set; }

    /// <summary>
    /// Moment about local 1 axis.
    /// </summary>
    public double M1 { get; set; }

    /// <summary>
    /// Moment about local 2 axis.
    /// </summary>
    public double M2 { get; set; }

    /// <summary>
    /// Moment about local 3 axis.
    /// </summary>
    public double M3 { get; set; }

    public override string ToString()
    {
        return $"Area Joint Force: {ElementName} - {LoadCase} - F1={F1:F2}, F2={F2:F2}, F3={F3:F2}";
    }
}