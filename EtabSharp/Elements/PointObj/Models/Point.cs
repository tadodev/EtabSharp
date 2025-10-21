namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents a point object (joint/node) in ETABS with its coordinates and properties.
/// </summary>
public class Point
{
    /// <summary>
    /// Unique name of the point object in ETABS
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// X coordinate in current length units
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Y coordinate in current length units
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Z coordinate (elevation) in current length units
    /// </summary>
    public double Z { get; set; }

    /// <summary>
    /// Coordinate system used for this point
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// Label assigned to this point
    /// </summary>
    public string Label { get; set; } = "";

    /// <summary>
    /// Story level where this point exists
    /// </summary>
    public string Story { get; set; } = "";

    /// <summary>
    /// Whether this is a special point (can exist without connections)
    /// </summary>
    public bool IsSpecial { get; set; }

    /// <summary>
    /// GUID identifier for the point
    /// </summary>
    public string GUID { get; set; } = "";

    /// <summary>
    /// Restraint conditions assigned to this point
    /// </summary>
    public PointRestraint? Restraint { get; set; }

    /// <summary>
    /// Spring properties assigned to this point
    /// </summary>
    public PointSpring? Spring { get; set; }

    /// <summary>
    /// Mass assigned to this point
    /// </summary>
    public PointMass? Mass { get; set; }

    /// <summary>
    /// Diaphragm assignment
    /// </summary>
    public string DiaphragmName { get; set; } = "";

    /// <summary>
    /// List of groups this point belongs to
    /// </summary>
    public List<string> Groups { get; set; } = new();

    /// <summary>
    /// Gets the distance from origin
    /// </summary>
    public double DistanceFromOrigin => Math.Sqrt(X * X + Y * Y + Z * Z);

    /// <summary>
    /// Calculates distance to another point
    /// </summary>
    public double DistanceTo(Point other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        double dz = Z - other.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    public override string ToString()
    {
        return $"Point {Name}: ({X:F3}, {Y:F3}, {Z:F3})";
    }
}