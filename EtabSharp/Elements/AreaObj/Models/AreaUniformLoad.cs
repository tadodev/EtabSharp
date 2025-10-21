namespace EtabSharp.Elements.AreaObj.Models;

/// <summary>
/// Represents a uniform load on an area (floor load).
/// </summary>
public class AreaUniformLoad
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
    /// Load value (force/area, e.g., kN/m² or psf)
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Direction: 3=Gravity, 4=Local-1, 5=Local-2, 6=Local-3
    /// </summary>
    public int Direction { get; set; } = 3; // Gravity

    /// <summary>
    /// Coordinate system for load
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// Creates gravity load (dead load or live load)
    /// </summary>
    public static AreaUniformLoad Gravity(string areaName, string pattern, double value)
    {
        return new AreaUniformLoad
        {
            AreaName = areaName,
            LoadPattern = pattern,
            Value = value,
            Direction = 3
        };
    }

    /// <summary>
    /// Creates uniform pressure in local 3-direction
    /// </summary>
    public static AreaUniformLoad Pressure(string areaName, string pattern, double value)
    {
        return new AreaUniformLoad
        {
            AreaName = areaName,
            LoadPattern = pattern,
            Value = value,
            Direction = 6, // Local-3
            CoordinateSystem = "Local"
        };
    }

    public override string ToString()
    {
        return $"{AreaName} [{LoadPattern}]: {Value:F2} (Dir {Direction})";
    }
}