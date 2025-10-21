namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents ground displacement (support settlement or base motion) at a point.
/// </summary>
public class PointDisplacement
{
    /// <summary>
    /// Name of the point where displacement is applied
    /// </summary>
    public required string PointName { get; set; }

    /// <summary>
    /// Load pattern name
    /// </summary>
    public required string LoadPattern { get; set; }

    /// <summary>
    /// Load case step
    /// </summary>
    public int LoadCaseStep { get; set; }

    /// <summary>
    /// Displacement in X direction
    /// </summary>
    public double Ux { get; set; }

    /// <summary>
    /// Displacement in Y direction
    /// </summary>
    public double Uy { get; set; }

    /// <summary>
    /// Displacement in Z direction
    /// </summary>
    public double Uz { get; set; }

    /// <summary>
    /// Rotation about X axis
    /// </summary>
    public double Rx { get; set; }

    /// <summary>
    /// Rotation about Y axis
    /// </summary>
    public double Ry { get; set; }

    /// <summary>
    /// Rotation about Z axis
    /// </summary>
    public double Rz { get; set; }

    /// <summary>
    /// Coordinate system for displacement
    /// </summary>
    public string CoordinateSystem { get; set; } = "Local";

    /// <summary>
    /// Converts to array [Ux, Uy, Uz, Rx, Ry, Rz]
    /// </summary>
    public double[] ToArray() => new[] { Ux, Uy, Uz, Rx, Ry, Rz };

    /// <summary>
    /// Creates from array [Ux, Uy, Uz, Rx, Ry, Rz]
    /// </summary>
    public static PointDisplacement FromArray(string pointName, string loadPattern, double[] values, string csys = "Local")
    {
        if (values?.Length != 6)
            throw new ArgumentException("Displacement array must have 6 elements");

        return new PointDisplacement
        {
            PointName = pointName,
            LoadPattern = loadPattern,
            Ux = values[0],
            Uy = values[1],
            Uz = values[2],
            Rx = values[3],
            Ry = values[4],
            Rz = values[5],
            CoordinateSystem = csys
        };
    }

    /// <summary>
    /// Resultant displacement magnitude
    /// </summary>
    public double ResultantDisplacement => Math.Sqrt(Ux * Ux + Uy * Uy + Uz * Uz);

    public override string ToString()
    {
        return $"Displacement at {PointName} [{LoadPattern}]: U=({Ux:F4}, {Uy:F4}, {Uz:F4})";
    }
}