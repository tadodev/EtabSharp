namespace EtabSharp.Elements.FrameObj.Models;

public class FrameDistributedLoad
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
    /// Direction: 1=Gravity, 4=Local-1, 5=Local-2, 6=Local-3, 7=Projected
    /// </summary>
    public int Direction { get; set; } = 6; // Local-3 (typical gravity load)

    /// <summary>
    /// Relative distance to start of load (0 to 1)
    /// </summary>
    public double RelativeDistance1 { get; set; } = 0;

    /// <summary>
    /// Relative distance to end of load (0 to 1)
    /// </summary>
    public double RelativeDistance2 { get; set; } = 1;

    /// <summary>
    /// Load value at start (force/length or moment/length)
    /// </summary>
    public double Value1 { get; set; }

    /// <summary>
    /// Load value at end (for trapezoidal loads)
    /// </summary>
    public double Value2 { get; set; }

    /// <summary>
    /// Coordinate system for load
    /// </summary>
    public string CoordinateSystem { get; set; } = "Global";

    /// <summary>
    /// Whether distances are relative (0-1) or absolute
    /// </summary>
    public bool IsRelativeDistance { get; set; } = true;

    /// <summary>
    /// Creates uniform gravity load
    /// </summary>
    public static FrameDistributedLoad UniformGravity(string frameName, string pattern, double value)
    {
        return new FrameDistributedLoad
        {
            FrameName = frameName,
            LoadPattern = pattern,
            LoadType = 1,
            Direction = 6, // Local-3
            Value1 = value,
            Value2 = value
        };
    }

    /// <summary>
    /// Whether this is a uniform load (Value1 = Value2)
    /// </summary>
    public bool IsUniform => Math.Abs(Value1 - Value2) < 1e-6;

    public override string ToString()
    {
        string loadDesc = IsUniform
            ? $"Uniform: {Value1:F2}"
            : $"Trapezoidal: {Value1:F2} → {Value2:F2}";
        return $"{FrameName} [{LoadPattern}]: {loadDesc}";
    }
}