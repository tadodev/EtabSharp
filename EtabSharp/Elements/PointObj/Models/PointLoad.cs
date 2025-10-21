namespace EtabSharp.Elements.PointObj.Models;

/// <summary>
/// Represents a concentrated force/moment load at a point.
/// </summary>
public class PointLoad
{
    /// <summary>
    /// Name of the point where load is applied
    /// </summary>
    public required string PointName { get; set; }

    /// <summary>
    /// Load pattern name
    /// </summary>
    public required string LoadPattern { get; set; }

    /// <summary>
    /// Load case step (for staged construction)
    /// </summary>
    public int LoadCaseStep { get; set; }

    /// <summary>
    /// Force in X direction
    /// </summary>
    public double Fx { get; set; }

    /// <summary>
    /// Force in Y direction
    /// </summary>
    public double Fy { get; set; }

    /// <summary>
    /// Force in Z direction
    /// </summary>
    public double Fz { get; set; }

    /// <summary>
    /// Moment about X axis
    /// </summary>
    public double Mx { get; set; }

    /// <summary>
    /// Moment about Y axis
    /// </summary>
    public double My { get; set; }

    /// <summary>
    /// Moment about Z axis
    /// </summary>
    public double Mz { get; set; }

    /// <summary>
    /// Coordinate system for load application
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// Converts to array [Fx, Fy, Fz, Mx, My, Mz]
    /// </summary>
    public double[] ToArray() => new[] { Fx, Fy, Fz, Mx, My, Mz };

    /// <summary>
    /// Creates from array [Fx, Fy, Fz, Mx, My, Mz]
    /// </summary>
    public static PointLoad FromArray(string pointName, string loadPattern, double[] values, string csys = "Global")
    {
        if (values?.Length != 6)
            throw new ArgumentException("Load array must have 6 elements");

        return new PointLoad
        {
            PointName = pointName,
            LoadPattern = loadPattern,
            Fx = values[0],
            Fy = values[1],
            Fz = values[2],
            Mx = values[3],
            My = values[4],
            Mz = values[5],
            CoordinateSystem = csys
        };
    }

    /// <summary>
    /// Resultant force magnitude
    /// </summary>
    public double ResultantForce => Math.Sqrt(Fx * Fx + Fy * Fy + Fz * Fz);

    /// <summary>
    /// Resultant moment magnitude
    /// </summary>
    public double ResultantMoment => Math.Sqrt(Mx * Mx + My * My + Mz * Mz);

    public override string ToString()
    {
        return $"Load at {PointName} [{LoadPattern}]: F=({Fx:F2}, {Fy:F2}, {Fz:F2})";
    }
}