namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas;

/// <summary>
/// Represents shell strains for area elements.
/// </summary>
public class AreaStrainShellResult
{
    public string ObjectName { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public string PointElement { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    #region Top Surface Strains

    public double E11Top { get; set; }
    public double E22Top { get; set; }
    public double G12Top { get; set; }
    public double EMaxTop { get; set; }
    public double EMinTop { get; set; }
    public double EAngleTop { get; set; }
    public double EVMTop { get; set; }

    #endregion

    #region Bottom Surface Strains

    public double E11Bot { get; set; }
    public double E22Bot { get; set; }
    public double G12Bot { get; set; }
    public double EMaxBot { get; set; }
    public double EMinBot { get; set; }
    public double EAngleBot { get; set; }
    public double EVMBot { get; set; }

    #endregion

    #region Average Shear Strains

    public double G13Avg { get; set; }
    public double G23Avg { get; set; }
    public double GMaxAvg { get; set; }
    public double GAngleAvg { get; set; }

    #endregion
}