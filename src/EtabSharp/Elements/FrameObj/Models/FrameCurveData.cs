namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents curve data for a curved frame object.
/// </summary>
public class FrameCurveData
{
    /// <summary>
    /// Name of the frame object
    /// </summary>
    public string FrameName { get; set; } = "";

    /// <summary>
    /// Type of curve
    /// </summary>
    public FrameCurveType CurveType { get; set; }

    /// <summary>
    /// Tension value for spline curves (controls curve bending between control points)
    /// Values greater than 1 produce unpredictable results
    /// </summary>
    public double Tension { get; set; }

    /// <summary>
    /// Number of points along the curve
    /// </summary>
    public int NumberOfPoints { get; set; }

    /// <summary>
    /// Global X coordinates of curve points
    /// </summary>
    public double[] GlobalX { get; set; } = Array.Empty<double>();

    /// <summary>
    /// Global Y coordinates of curve points
    /// </summary>
    public double[] GlobalY { get; set; } = Array.Empty<double>();

    /// <summary>
    /// Global Z coordinates of curve points
    /// </summary>
    public double[] GlobalZ { get; set; } = Array.Empty<double>();

    public override string ToString()
    {
        return $"Curved Frame {FrameName}: {CurveType}, {NumberOfPoints} points";
    }
}

/// <summary>
/// Types of frame curves
/// </summary>
public enum FrameCurveType
{
    /// <summary>
    /// Straight frame (not curved)
    /// </summary>
    Straight = 0,

    /// <summary>
    /// /// Circular curve
    /// </summary>
    ular = 1,

    /// <summary>
    /// Multilinear curve (straight segments between points)
    /// </summary>
    Multilinear = 2,

    /// <summary>
    /// Bezier curve
    /// </summary>
    Bezier = 3,

    /// <summary>
    /// Spline curve
    /// </summary>
    Spline = 4
}
