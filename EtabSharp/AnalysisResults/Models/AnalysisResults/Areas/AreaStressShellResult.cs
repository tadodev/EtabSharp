namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;

/// <summary>
/// Represents shell stresses for area elements.
/// </summary>
public class AreaStressShellResult
{
    public string ObjectName { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public string PointElement { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    #region Top Surface Stresses

    /// <summary>
    /// S11Top - Normal stress in local 1 direction at top surface.
    /// </summary>
    public double S11Top { get; set; }

    /// <summary>
    /// S22Top - Normal stress in local 2 direction at top surface.
    /// </summary>
    public double S22Top { get; set; }

    /// <summary>
    /// S12Top - Shear stress at top surface.
    /// </summary>
    public double S12Top { get; set; }

    /// <summary>
    /// SMaxTop - Maximum principal stress at top surface.
    /// </summary>
    public double SMaxTop { get; set; }

    /// <summary>
    /// SMinTop - Minimum principal stress at top surface.
    /// </summary>
    public double SMinTop { get; set; }

    /// <summary>
    /// SAngleTop - Angle of maximum principal stress at top (degrees).
    /// </summary>
    public double SAngleTop { get; set; }

    /// <summary>
    /// SVMTop - Von Mises stress at top surface.
    /// </summary>
    public double SVMTop { get; set; }

    #endregion

    #region Bottom Surface Stresses

    /// <summary>
    /// S11Bot - Normal stress in local 1 direction at bottom surface.
    /// </summary>
    public double S11Bot { get; set; }

    /// <summary>
    /// S22Bot - Normal stress in local 2 direction at bottom surface.
    /// </summary>
    public double S22Bot { get; set; }

    /// <summary>
    /// S12Bot - Shear stress at bottom surface.
    /// </summary>
    public double S12Bot { get; set; }

    /// <summary>
    /// SMaxBot - Maximum principal stress at bottom surface.
    /// </summary>
    public double SMaxBot { get; set; }

    /// <summary>
    /// SMinBot - Minimum principal stress at bottom surface.
    /// </summary>
    public double SMinBot { get; set; }

    /// <summary>
    /// SAngleBot - Angle of maximum principal stress at bottom (degrees).
    /// </summary>
    public double SAngleBot { get; set; }

    /// <summary>
    /// SVMBot - Von Mises stress at bottom surface.
    /// </summary>
    public double SVMBot { get; set; }

    #endregion

    #region Average Shear Stresses

    /// <summary>
    /// S13Avg - Average shear stress in 1-3 plane.
    /// </summary>
    public double S13Avg { get; set; }

    /// <summary>
    /// S23Avg - Average shear stress in 2-3 plane.
    /// </summary>
    public double S23Avg { get; set; }

    /// <summary>
    /// SMaxAvg - Maximum average shear stress.
    /// </summary>
    public double SMaxAvg { get; set; }

    /// <summary>
    /// SAngleAvg - Angle of maximum average shear stress (degrees).
    /// </summary>
    public double SAngleAvg { get; set; }

    #endregion

    public override string ToString()
    {
        return $"Area Stress: {ElementName} - {LoadCase} - Top VM={SVMTop:F2}, Bot VM={SVMBot:F2}";
    }
}