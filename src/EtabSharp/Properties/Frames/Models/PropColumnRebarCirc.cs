namespace EtabSharp.Properties.Frames.Models;

/// <summary>
/// Reinforcement data for circular columns.
/// </summary>
public record PropColumnRebarCirc
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
    public int Pattern = 2; // 1=Rectangular, 2=Circular

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
    public int NumberOfBars { get; set; }

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

    #region Static Factory Methods

    /// <summary>
    /// Creates circular column rebar with tie confinement
    /// </summary>
    /// <param name="longitudinalRebar">Longitudinal rebar material name (e.g., "A615Gr60")</param>
    /// <param name="confinementRebar">Confinement rebar material name (e.g., "A615Gr60")</param>
    /// <param name="numberOfBars">Number of bars around the circumference (typically 6-12)</param>
    /// <param name="cover">Clear cover to reinforcement (in current units)</param>
    /// <param name="barSize">Longitudinal bar size (e.g., "#8", "#10")</param>
    /// <param name="tieSize">Tie bar size (e.g., "#3", "#4")</param>
    /// <param name="tieSpacing">Spacing between ties (in current units)</param>
    /// <returns>Configured PropColumnRebarCirc instance</returns>
    public static PropColumnRebarCirc CreateWithTies(
        string longitudinalRebar,
        string confinementRebar,
        int numberOfBars,
        double cover = 2.0,
        string barSize = "#8",
        string tieSize = "#4",
        double tieSpacing = 12.0)
    {
        return new PropColumnRebarCirc
        {
            MatPropLong = longitudinalRebar,
            MatPropConfine = confinementRebar,
            NumberOfBars = numberOfBars,
            Cover = cover,
            BarSize = barSize,
            TieSize = tieSize,
            TieSpacing = tieSpacing,
            Pattern = 2,        // Circular
            ConfineType = 1,    // Ties
            TieLegs2Dir = 2,
            TieLegs3Dir = 2
        };
    }

    /// <summary>
    /// Creates circular column rebar with spiral confinement
    /// </summary>
    /// <param name="longitudinalRebar">Longitudinal rebar material name (e.g., "A615Gr60")</param>
    /// <param name="confinementRebar">Confinement rebar material name (e.g., "A615Gr60")</param>
    /// <param name="numberOfBars">Number of bars around the circumference (typically 6-12)</param>
    /// <param name="cover">Clear cover to reinforcement (in current units)</param>
    /// <param name="barSize">Longitudinal bar size (e.g., "#8", "#10")</param>
    /// <param name="spiralSize">Spiral bar size (e.g., "#3", "#4")</param>
    /// <param name="spiralPitch">Pitch of spiral (vertical spacing, in current units)</param>
    /// <returns>Configured PropColumnRebarCirc instance</returns>
    public static PropColumnRebarCirc CreateWithSpiral(
        string longitudinalRebar,
        string confinementRebar,
        int numberOfBars,
        double cover = 2.0,
        string barSize = "#8",
        string spiralSize = "#4",
        double spiralPitch = 2.0)
    {
        return new PropColumnRebarCirc
        {
            MatPropLong = longitudinalRebar,
            MatPropConfine = confinementRebar,
            NumberOfBars = numberOfBars,
            Cover = cover,
            BarSize = barSize,
            TieSize = spiralSize,
            TieSpacing = spiralPitch,
            Pattern = 2,        // Circular
            ConfineType = 2,    // Spiral
            TieLegs2Dir = 0,    // Not applicable for spiral
            TieLegs3Dir = 0     // Not applicable for spiral
        };
    }

    /// <summary>
    /// Creates a typical circular column with 8 bars and tie confinement
    /// </summary>
    /// <param name="longitudinalRebar">Longitudinal rebar material name</param>
    /// <param name="confinementRebar">Confinement rebar material name</param>
    /// <param name="barSize">Longitudinal bar size (default: "#8")</param>
    /// <returns>Configured PropColumnRebarCirc instance with typical values</returns>
    public static PropColumnRebarCirc Typical(
        string longitudinalRebar,
        string confinementRebar,
        string barSize = "#8")
    {
        return new PropColumnRebarCirc
        {
            MatPropLong = longitudinalRebar,
            MatPropConfine = confinementRebar,
            NumberOfBars = 8,
            Cover = 2.0,
            BarSize = barSize,
            TieSize = "#4",
            TieSpacing = 12.0,
            Pattern = 2,
            ConfineType = 1
        };
    }

    /// <summary>
    /// Creates a typical circular column with spiral confinement
    /// Commonly used for seismic design (better ductility)
    /// </summary>
    /// <param name="longitudinalRebar">Longitudinal rebar material name</param>
    /// <param name="confinementRebar">Confinement rebar material name</param>
    /// <param name="numberOfBars">Number of longitudinal bars (default: 8)</param>
    /// <param name="barSize">Longitudinal bar size (default: "#8")</param>
    /// <returns>Configured PropColumnRebarCirc instance with typical spiral values</returns>
    public static PropColumnRebarCirc TypicalSpiral(
        string longitudinalRebar,
        string confinementRebar,
        int numberOfBars = 8,
        string barSize = "#8")
    {
        return new PropColumnRebarCirc
        {
            MatPropLong = longitudinalRebar,
            MatPropConfine = confinementRebar,
            NumberOfBars = numberOfBars,
            Cover = 2.0,
            BarSize = barSize,
            TieSize = "#4",
            TieSpacing = 2.0,   // Typical spiral pitch
            Pattern = 2,
            ConfineType = 2,    // Spiral
            TieLegs2Dir = 0,
            TieLegs3Dir = 0
        };
    }
    #endregion
}