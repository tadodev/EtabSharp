namespace EtabSharp.DatabaseTables.Models;

/// <summary>
/// Output options for display configuration.
/// </summary>
public class OutputOptionsForDisplay
{
    /// <summary>
    /// Use user-defined base reaction location.
    /// </summary>
    public bool IsUserBaseReactionLocation { get; set; }

    /// <summary>
    /// User-defined base reaction X coordinate.
    /// </summary>
    public double UserBaseReactionX { get; set; }

    /// <summary>
    /// User-defined base reaction Y coordinate.
    /// </summary>
    public double UserBaseReactionY { get; set; }

    /// <summary>
    /// User-defined base reaction Z coordinate.
    /// </summary>
    public double UserBaseReactionZ { get; set; }

    /// <summary>
    /// Output all modes.
    /// </summary>
    public bool IsAllModes { get; set; }

    /// <summary>
    /// Starting mode number.
    /// </summary>
    public int StartMode { get; set; } = 1;

    /// <summary>
    /// Ending mode number.
    /// </summary>
    public int EndMode { get; set; } = 12;

    /// <summary>
    /// Output all buckling modes.
    /// </summary>
    public bool IsAllBucklingModes { get; set; }

    /// <summary>
    /// Starting buckling mode number.
    /// </summary>
    public int StartBucklingMode { get; set; } = 1;

    /// <summary>
    /// Ending buckling mode number.
    /// </summary>
    public int EndBucklingMode { get; set; } = 1;

    /// <summary>
    /// Multistep static output option.
    /// </summary>
    public int MultistepStatic { get; set; }

    /// <summary>
    /// Nonlinear static output option.
    /// </summary>
    public int NonlinearStatic { get; set; }

    /// <summary>
    /// Modal history output option.
    /// </summary>
    public int ModalHistory { get; set; }

    /// <summary>
    /// Direct history output option.
    /// </summary>
    public int DirectHistory { get; set; }

    /// <summary>
    /// Combination output option.
    /// </summary>
    public int Combo { get; set; }
}