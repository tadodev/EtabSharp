namespace EtabSharp.Design.Concrete.Models;

/// <summary>
/// Represents concrete beam design results for a single frame element location.
/// </summary>
public class ConcreteBeamDesignResult
{
    /// <summary>
    /// Frame element name.
    /// </summary>
    public string FrameName { get; set; } = string.Empty;

    /// <summary>
    /// Location along the frame (typically 0 to 1).
    /// </summary>
    public double Location { get; set; }

    #region Top Reinforcement

    /// <summary>
    /// Top reinforcement controlling load combination.
    /// </summary>
    public string TopCombo { get; set; } = string.Empty;

    /// <summary>
    /// Top reinforcement area.
    /// </summary>
    public double TopArea { get; set; }

    /// <summary>
    /// Top reinforcement area required (from GetSummaryResultsBeam_2).
    /// </summary>
    public double TopAreaRequired { get; set; }

    /// <summary>
    /// Top reinforcement minimum area (from GetSummaryResultsBeam_2).
    /// </summary>
    public double TopAreaMin { get; set; }

    /// <summary>
    /// Top reinforcement area provided (from GetSummaryResultsBeam_2).
    /// </summary>
    public double TopAreaProvided { get; set; }

    #endregion

    #region Bottom Reinforcement

    /// <summary>
    /// Bottom reinforcement controlling load combination.
    /// </summary>
    public string BottomCombo { get; set; } = string.Empty;

    /// <summary>
    /// Bottom reinforcement area.
    /// </summary>
    public double BottomArea { get; set; }

    /// <summary>
    /// Bottom reinforcement area required (from GetSummaryResultsBeam_2).
    /// </summary>
    public double BottomAreaRequired { get; set; }

    /// <summary>
    /// Bottom reinforcement minimum area (from GetSummaryResultsBeam_2).
    /// </summary>
    public double BottomAreaMin { get; set; }

    /// <summary>
    /// Bottom reinforcement area provided (from GetSummaryResultsBeam_2).
    /// </summary>
    public double BottomAreaProvided { get; set; }

    #endregion

    #region Shear Reinforcement

    /// <summary>
    /// Major axis shear controlling load combination.
    /// </summary>
    public string VMajorCombo { get; set; } = string.Empty;

    /// <summary>
    /// Major axis shear reinforcement area.
    /// </summary>
    public double VMajorArea { get; set; }

    /// <summary>
    /// Major axis shear reinforcement area required (from GetSummaryResultsBeam_2).
    /// </summary>
    public double VMajorAreaRequired { get; set; }

    /// <summary>
    /// Major axis shear reinforcement minimum area (from GetSummaryResultsBeam_2).
    /// </summary>
    public double VMajorAreaMin { get; set; }

    /// <summary>
    /// Major axis shear reinforcement area provided (from GetSummaryResultsBeam_2).
    /// </summary>
    public double VMajorAreaProvided { get; set; }

    #endregion

    #region Torsion Reinforcement

    /// <summary>
    /// Torsion longitudinal reinforcement controlling load combination.
    /// </summary>
    public string TLCombo { get; set; } = string.Empty;

    /// <summary>
    /// Torsion longitudinal reinforcement area.
    /// </summary>
    public double TLArea { get; set; }

    /// <summary>
    /// Torsion transverse reinforcement controlling load combination.
    /// </summary>
    public string TTCombo { get; set; } = string.Empty;

    /// <summary>
    /// Torsion transverse reinforcement area.
    /// </summary>
    public double TTArea { get; set; }

    #endregion

    #region Status

    /// <summary>
    /// Error summary string.
    /// </summary>
    public string ErrorSummary { get; set; } = string.Empty;

    /// <summary>
    /// Warning summary string.
    /// </summary>
    public string WarningSummary { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if design passed (no errors).
    /// </summary>
    public bool Passes => string.IsNullOrWhiteSpace(ErrorSummary);

    #endregion

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{FrameName} @ {Location:F3}: Top={TopArea:F2}, Bot={BottomArea:F2}, Status={(Passes ? "Pass" : "Errors")}";
    }
}