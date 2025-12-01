namespace EtabSharp.Design.Concrete.Models;

/// <summary>
/// Represents concrete joint design results for a single frame element.
/// </summary>
public class ConcreteJointDesignResult
{
    /// <summary>
    /// Frame element name.
    /// </summary>
    public string FrameName { get; set; } = string.Empty;

    #region Joint Shear Ratios

    /// <summary>
    /// Load combination for joint shear ratio (major axis).
    /// </summary>
    public string LCJSRatioMajor { get; set; } = string.Empty;

    /// <summary>
    /// Joint shear ratio (major axis).
    /// </summary>
    public double JSRatioMajor { get; set; }

    /// <summary>
    /// Load combination for joint shear ratio (minor axis).
    /// </summary>
    public string LCJSRatioMinor { get; set; } = string.Empty;

    /// <summary>
    /// Joint shear ratio (minor axis).
    /// </summary>
    public double JSRatioMinor { get; set; }

    #endregion

    #region Beam-Column Capacity Ratios

    /// <summary>
    /// Load combination for beam-column capacity ratio (major axis).
    /// </summary>
    public string LCBCCRatioMajor { get; set; } = string.Empty;

    /// <summary>
    /// Beam-column capacity ratio (major axis).
    /// </summary>
    public double BCCRatioMajor { get; set; }

    /// <summary>
    /// Load combination for beam-column capacity ratio (minor axis).
    /// </summary>
    public string LCBCCRatioMinor { get; set; } = string.Empty;

    /// <summary>
    /// Beam-column capacity ratio (minor axis).
    /// </summary>
    public double BCCRatioMinor { get; set; }

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
    /// Overall controlling ratio (maximum of all ratios).
    /// </summary>
    public double ControllingRatio => Math.Max(
        Math.Max(JSRatioMajor, JSRatioMinor),
        Math.Max(BCCRatioMajor, BCCRatioMinor)
    );

    /// <summary>
    /// Indicates if design passed (no errors and ratios <= 1.0).
    /// </summary>
    public bool Passes => string.IsNullOrWhiteSpace(ErrorSummary) && ControllingRatio <= 1.0;

    #endregion

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{FrameName}: JS Maj={JSRatioMajor:F3}, JS Min={JSRatioMinor:F3}, Status={(Passes ? "Pass" : "Fail")}";
    }
}