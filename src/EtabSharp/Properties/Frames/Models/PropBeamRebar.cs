namespace EtabSharp.Properties.Frames.Models;

/// <summary>
/// Reinforcement data for rectangular beams.
/// </summary>
public record PropBeamRebar
{
    /// <summary>
    /// Longitudinal rebar material name
    /// </summary>
    public required string MatPropLong { get; set; }

    /// <summary>
    /// Confinement (stirrup) rebar material name
    /// </summary>
    public required string MatPropConfine { get; set; }

    /// <summary>
    /// Top cover to reinforcement
    /// </summary>
    public double CoverTop { get; set; } = 2; // in or as per units

    /// <summary>
    /// Bottom cover to reinforcement
    /// </summary>
    public double CoverBottom { get; set; } = 2; // in or as per units

    /// <summary>
    /// Top left corner bar area
    /// </summary>
    public double TopLeftArea { get; set; } = 0.0;

    /// <summary>
    /// Top right corner bar area
    /// </summary>
    public double TopRightArea { get; set; } = 0.0;

    /// <summary>
    /// Bottom left corner bar area
    /// </summary>
    public double BottomLeftArea { get; set; } = 0.0;

    /// <summary>
    /// Bottom right corner bar area
    /// </summary>
    public double BottomRightArea { get; set; } = 0.0;

    /// <summary>
    /// Creates beam rebar with specified bar areas
    /// </summary>
    public static PropBeamRebar Create(
        string longitudinalRebar,
        string confinementRebar,
        double topLeftArea,
        double topRightArea,
        double bottomLeftArea,
        double bottomRightArea,
        double coverTop = 2.0,
        double coverBottom = 2.0)
    {
        return new PropBeamRebar
        {
            MatPropLong = longitudinalRebar,
            MatPropConfine = confinementRebar,
            TopLeftArea = topLeftArea,
            TopRightArea = topRightArea,
            BottomLeftArea = bottomLeftArea,
            BottomRightArea = bottomRightArea,
            CoverTop = coverTop,
            CoverBottom = coverBottom
        };
    }

    /// <summary>
    /// Creates symmetric beam rebar (same top and bottom)
    /// </summary>
    public static PropBeamRebar Symmetric(
        string longitudinalRebar,
        string confinementRebar,
        double cornerBarArea,
        double coverTop = 2.0,
        double coverBottom = 2.0)
    {
        return new PropBeamRebar
        {
            MatPropLong = longitudinalRebar,
            MatPropConfine = confinementRebar,
            TopLeftArea = cornerBarArea,
            TopRightArea = cornerBarArea,
            BottomLeftArea = cornerBarArea,
            BottomRightArea = cornerBarArea,
            CoverTop = coverTop,
            CoverBottom = coverBottom
        };
    }
}