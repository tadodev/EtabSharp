namespace EtabSharp.Analyzes.Models;

/// <summary>
/// Geometry modification parameters.
/// </summary>
public class GeometryModification
{
    /// <summary>
    /// Load case name to use for modification
    /// </summary>
    public required string CaseName { get; set; }

    /// <summary>
    /// Scale factor for displacements
    /// </summary>
    public double ScaleFactor { get; set; } = 1.0;

    /// <summary>
    /// Stage number (-1 for all stages)
    /// </summary>
    public int Stage { get; set; } = -1;

    /// <summary>
    /// Restore original geometry
    /// </summary>
    public bool RestoreOriginal { get; set; }

    public override string ToString()
    {
        if (RestoreOriginal)
            return "Restore Original Geometry";
        return $"Modify from {CaseName}: SF={ScaleFactor:F2}, Stage={Stage}";
    }
}