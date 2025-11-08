namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;

/// <summary>
/// Represents shell internal forces for a single area element result point.
/// </summary>
public class AreaForceShellResult
{
    /// <summary>
    /// Object name (area object identifier).
    /// </summary>
    public string ObjectName { get; set; } = string.Empty;

    /// <summary>
    /// Element name (unique element identifier).
    /// </summary>
    public string ElementName { get; set; } = string.Empty;

    /// <summary>
    /// Point element identifier (result point on element).
    /// </summary>
    public string PointElement { get; set; } = string.Empty;

    /// <summary>
    /// Load case name.
    /// </summary>
    public string LoadCase { get; set; } = string.Empty;

    /// <summary>
    /// Step type (e.g., "Step By Step", "Max", "Min").
    /// </summary>
    public string StepType { get; set; } = string.Empty;

    /// <summary>
    /// Step number for time history/nonlinear analysis.
    /// </summary>
    public double StepNum { get; set; }

    #region In-Plane Forces (Force per unit length)

    /// <summary>
    /// F11 - In-plane force in local 1 direction (Force/Length).
    /// </summary>
    public double F11 { get; set; }

    /// <summary>
    /// F22 - In-plane force in local 2 direction (Force/Length).
    /// </summary>
    public double F22 { get; set; }

    /// <summary>
    /// F12 - In-plane shear force (Force/Length).
    /// </summary>
    public double F12 { get; set; }

    /// <summary>
    /// FMax - Maximum in-plane principal force (Force/Length).
    /// </summary>
    public double FMax { get; set; }

    /// <summary>
    /// FMin - Minimum in-plane principal force (Force/Length).
    /// </summary>
    public double FMin { get; set; }

    /// <summary>
    /// FAngle - Angle of maximum principal in-plane force (degrees).
    /// </summary>
    public double FAngle { get; set; }

    /// <summary>
    /// FVM - Von Mises in-plane force (Force/Length).
    /// </summary>
    public double FVM { get; set; }

    #endregion

    #region Bending Moments (Moment per unit length)

    /// <summary>
    /// M11 - Bending moment about local 2 axis (Moment/Length).
    /// </summary>
    public double M11 { get; set; }

    /// <summary>
    /// M22 - Bending moment about local 1 axis (Moment/Length).
    /// </summary>
    public double M22 { get; set; }

    /// <summary>
    /// M12 - Twisting moment (Moment/Length).
    /// </summary>
    public double M12 { get; set; }

    /// <summary>
    /// MMax - Maximum principal moment (Moment/Length).
    /// </summary>
    public double MMax { get; set; }

    /// <summary>
    /// MMin - Minimum principal moment (Moment/Length).
    /// </summary>
    public double MMin { get; set; }

    /// <summary>
    /// MAngle - Angle of maximum principal moment (degrees).
    /// </summary>
    public double MAngle { get; set; }

    #endregion

    #region Out-of-Plane Shear Forces (Force per unit length)

    /// <summary>
    /// V13 - Out-of-plane shear force in local 1-3 plane (Force/Length).
    /// </summary>
    public double V13 { get; set; }

    /// <summary>
    /// V23 - Out-of-plane shear force in local 2-3 plane (Force/Length).
    /// </summary>
    public double V23 { get; set; }

    /// <summary>
    /// VMax - Maximum out-of-plane shear force (Force/Length).
    /// </summary>
    public double VMax { get; set; }

    /// <summary>
    /// VAngle - Angle of maximum out-of-plane shear (degrees).
    /// </summary>
    public double VAngle { get; set; }

    #endregion

    public override string ToString()
    {
        return $"Area Force: {ElementName} - {LoadCase} ({StepType} #{StepNum}) - F11={F11:F2}, F22={F22:F2}, M11={M11:F2}";
    }
}