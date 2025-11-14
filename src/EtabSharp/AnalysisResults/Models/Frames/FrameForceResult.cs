namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Frames;

/// <summary>
/// Represents frame element forces at a station.
/// </summary>
public class FrameForceResult
{
    public string ObjectName { get; set; } = string.Empty;
    public double ObjectStation { get; set; }
    public string ElementName { get; set; } = string.Empty;
    public double ElementStation { get; set; }
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    /// <summary>
    /// P - Axial force.
    /// </summary>
    public double P { get; set; }

    /// <summary>
    /// V2 - Shear force in local 2 direction.
    /// </summary>
    public double V2 { get; set; }

    /// <summary>
    /// V3 - Shear force in local 3 direction.
    /// </summary>
    public double V3 { get; set; }

    /// <summary>
    /// T - Torsional moment.
    /// </summary>
    public double T { get; set; }

    /// <summary>
    /// M2 - Bending moment about local 2 axis.
    /// </summary>
    public double M2 { get; set; }

    /// <summary>
    /// M3 - Bending moment about local 3 axis.
    /// </summary>
    public double M3 { get; set; }

    public override string ToString()
    {
        return $"Frame Force: {ElementName} @ {ElementStation:F3} - {LoadCase} - P={P:F2}, V2={V2:F2}, M3={M3:F2}";
    }
}

