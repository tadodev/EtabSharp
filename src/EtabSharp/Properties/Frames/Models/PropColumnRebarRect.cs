namespace EtabSharp.Properties.Frames.Models;

/// <summary>
/// Reinforcement data for rectangular columns.
/// </summary>
public record PropColumnRebarRect
{
    /// <summary>
    /// Longitudinal rebar material name
    /// </summary>
    public required string MatPropLong { get; set; }

    /// <summary>
    /// Confinement (tie/stirrup) rebar material name
    /// </summary>
    public required string MatPropConfine { get; set; }

    /// <summary>
    /// Rebar configuration pattern: 1=Rectangular, 2=Circular
    /// </summary>
    public int Pattern = 1; // 1=Rectangular, 2=Circular

    /// <summary>
    /// Confinement type: 1=Ties, 2=Spiral
    /// </summary>
    public int ConfineType = 1;

    /// <summary>
    /// Clear cover to reinforcement
    /// </summary>
    public double Cover { get; set; } = 2; // in or as per units

    /// <summary>
    /// Number of bars along local 3-axis (depth direction)
    /// </summary>
    public int NumberOfBars3Dir { get; set; }

    /// <summary>
    /// Number of bars along local 2-axis (width direction)
    /// </summary>
    public int NumberOfBars2Dir { get; set; }

    /// <summary>
    /// Size of longitudinal bars (e.g., "#8", "25M", "H16")
    /// </summary>
    public string BarSize { get; set; } = "#8";

    /// <summary>
    /// Size of tie/stirrup bars
    /// </summary>
    public string TieSize { get; set; } = "#4";

    /// <summary>
    /// Spacing of ties/stirrups
    /// </summary>
    public double TieSpacing { get; set; } = 12; // in or as per units

    /// <summary>
    /// Number of tie legs parallel to local 2-axis
    /// </summary>
    public int TieLegs2Dir { get; set; } = 2;

    /// <summary>
    /// Number of tie legs parallel to local 3-axis
    /// </summary>
    public int TieLegs3Dir { get; set; } = 2;

    /// <summary>
    /// Whether this column is to be designed
    /// </summary>
    public bool ToBeDesigned { get; set; } = false;

    /// <summary>
    /// Creates rectangular column rebar with typical values
    /// </summary>
    public static PropColumnRebarRect Create(
        string longitudinalRebar,
        string confinementRebar,
        int barsIn3Direction,
        int barsIn2Direction,
        double cover = 2.0,
        string barSize = "#8",
        string tieSize = "#4",
        double tieSpacing = 12.0)
    {
        return new PropColumnRebarRect
        {
            MatPropLong = longitudinalRebar,
            MatPropConfine = confinementRebar,
            NumberOfBars3Dir = barsIn3Direction,
            NumberOfBars2Dir = barsIn2Direction,
            Cover = cover,
            BarSize = barSize,
            TieSize = tieSize,
            TieSpacing = tieSpacing
        };
    }

    /// <summary>
    /// Creates typical rectangular column rebar (4x4 pattern with #8 bars)
    /// </summary>
    public static PropColumnRebarRect Typical(
        string longitudinalRebar,
        string confinementRebar)
    {
        return new PropColumnRebarRect
        {
            MatPropLong = longitudinalRebar,
            MatPropConfine = confinementRebar,
            NumberOfBars3Dir = 4,
            NumberOfBars2Dir = 4,
            Cover = 2.0,
            BarSize = "#8",
            TieSize = "#4",
            TieSpacing = 12.0
        };
    }
}