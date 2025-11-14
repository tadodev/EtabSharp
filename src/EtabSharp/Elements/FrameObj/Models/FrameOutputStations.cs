namespace EtabSharp.Elements.FrameObj.Models;

/// <summary>
/// Represents output station configuration for a frame.
/// </summary>
public class FrameOutputStations
{
    /// <summary>
    /// Station type: 0=MinNumStations, 1=MaxSpacing
    /// </summary>
    public int StationType { get; set; }

    /// <summary>
    /// Maximum segment size between stations
    /// </summary>
    public double MaxSegmentSize { get; set; }

    /// <summary>
    /// Minimum number of stations
    /// </summary>
    public int MinStations { get; set; } = 5;

    /// <summary>
    /// Suppress output at element ends
    /// </summary>
    public bool NoOutputAtElementEnds { get; set; }

    /// <summary>
    /// Suppress output at point loads
    /// </summary>
    public bool NoOutputAtPointLoads { get; set; }

    /// <summary>
    /// Creates default station configuration (minimum 5 stations)
    /// </summary>
    public static FrameOutputStations Default() => new()
    {
        StationType = 0,
        MinStations = 5
    };

    public override string ToString()
    {
        return StationType == 0
            ? $"Min {MinStations} stations"
            : $"Max spacing {MaxSegmentSize:F2}";
    }
}