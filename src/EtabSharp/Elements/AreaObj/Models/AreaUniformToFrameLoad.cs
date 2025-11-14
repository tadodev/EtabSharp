namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents uniform load distributed to surrounding frame elements.
/// Used for one-way slabs.
/// </summary>
public class AreaUniformToFrameLoad
{
    /// <summary>
    /// Name of the area
    /// </summary>
    public required string AreaName { get; set; }

    /// <summary>
    /// Load pattern name
    /// </summary>
    public required string LoadPattern { get; set; }

    /// <summary>
    /// Load value (force/area)
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Load direction
    /// </summary>
    public int Direction { get; set; } = 3;

    /// <summary>
    /// Distribution type: how load is distributed to frames
    /// </summary>
    public int DistributionType { get; set; }

    /// <summary>
    /// Coordinate system
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    public override string ToString()
    {
        return $"{AreaName} → Frame [{LoadPattern}]: {Value:F2}";
    }
}