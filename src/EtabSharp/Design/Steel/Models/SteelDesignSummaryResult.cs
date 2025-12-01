using ETABSv1;

namespace EtabSharp.Design.Steel.Models;

/// <summary>
/// Represents a steel design summary result for a single frame element.
/// </summary>
public class SteelDesignSummaryResult
{
    /// <summary>
    /// Frame element name.
    /// </summary>
    public string FrameName { get; set; } = string.Empty;

    /// <summary>
    /// Frame type/orientation (Column, Beam, Brace, etc.).
    /// </summary>
    public eFrameDesignOrientation FrameType { get; set; }

    /// <summary>
    /// Design section name.
    /// </summary>
    public string DesignSection { get; set; } = string.Empty;

    /// <summary>
    /// Design status (e.g., "Pass", "Fail", "No Check").
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Controlling P-M-M (axial-moment) load combination name.
    /// </summary>
    public string PMMCombo { get; set; } = string.Empty;

    /// <summary>
    /// Maximum P-M-M interaction ratio.
    /// </summary>
    public double PMMRatio { get; set; }

    /// <summary>
    /// Axial force ratio (P only).
    /// </summary>
    public double PRatio { get; set; }

    /// <summary>
    /// Major axis moment ratio.
    /// </summary>
    public double MMajorRatio { get; set; }

    /// <summary>
    /// Minor axis moment ratio.
    /// </summary>
    public double MMinorRatio { get; set; }

    /// <summary>
    /// Controlling major axis shear combination name.
    /// </summary>
    public string VMajorCombo { get; set; } = string.Empty;

    /// <summary>
    /// Major axis shear ratio.
    /// </summary>
    public double VMajorRatio { get; set; }

    /// <summary>
    /// Controlling minor axis shear combination name.
    /// </summary>
    public string VMinorCombo { get; set; } = string.Empty;

    /// <summary>
    /// Minor axis shear ratio.
    /// </summary>
    public double VMinorRatio { get; set; }

    /// <summary>
    /// Overall controlling ratio (maximum of all ratios).
    /// </summary>
    public double ControllingRatio => Math.Max(PMMRatio, Math.Max(VMajorRatio, VMinorRatio));

    /// <summary>
    /// Indicates if design passes (ratio <= 1.0 and status is "Pass").
    /// </summary>
    public bool Passes => Status.Equals("Pass", StringComparison.OrdinalIgnoreCase) && ControllingRatio <= 1.0;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{FrameName} ({FrameType}): {Status} - Ratio={ControllingRatio:F3} - Section={DesignSection}";
    }
}