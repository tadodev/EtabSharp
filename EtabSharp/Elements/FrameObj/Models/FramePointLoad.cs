namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents a point load on a frame.
/// </summary>
public class FramePointLoad
{
    /// <summary>
    /// Name of the frame
    /// </summary>
    public required string FrameName { get; set; }

    /// <summary>
    /// Load pattern name
    /// </summary>
    public required string LoadPattern { get; set; }

    /// <summary>
    /// Load type: 1=Force, 2=Moment
    /// </summary>
    public int LoadType { get; set; } = 1;

    /// <summary>
    /// Direction: 4=Local-1, 5=Local-2, 6=Local-3
    /// </summary>
    public int Direction { get; set; } = 6;

    /// <summary>
    /// Relative distance along frame (0 to 1, where 0=I-end, 1=J-end)
    /// </summary>
    public double RelativeDistance { get; set; } = 0.5;

    /// <summary>
    /// Load value (force or moment)
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Coordinate system
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// Whether distance is relative (0-1) or absolute
    /// </summary>
    public bool IsRelativeDistance { get; set; } = true;

    /// <summary>
    /// Creates concentrated load at mid-span
    /// </summary>
    public static FramePointLoad MidSpan(string frameName, string pattern, double value)
    {
        return new FramePointLoad
        {
            FrameName = frameName,
            LoadPattern = pattern,
            Value = value,
            RelativeDistance = 0.5
        };
    }

    public override string ToString()
    {
        return $"{FrameName} [{LoadPattern}]: {Value:F2} at {RelativeDistance:P0}";
    }
}