namespace EtabSharp.Properties.Materials.Models;

/// <summary>
/// Stress-strain curve point
/// </summary>
public class StressStrainPoint
{
    /// <summary>
    /// Point ID
    /// </summary>
    public int PointID { get; set; }

    /// <summary>
    /// Strain value
    /// </summary>
    public double Strain { get; set; }

    /// <summary>
    /// Stress value
    /// </summary>
    public double Stress { get; set; }
}