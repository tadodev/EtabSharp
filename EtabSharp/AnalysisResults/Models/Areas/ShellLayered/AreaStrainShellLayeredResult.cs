namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas.ShellLayered;

/// <summary>
/// Represents layered shell strains for area elements.
/// </summary>
public class AreaStrainShellLayeredResult
{
    public string ObjectName { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public string Layer { get; set; } = string.Empty;
    public int IntegrationPointNum { get; set; }
    public double IntegrationPointLoc { get; set; }
    public string PointElement { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    public double E11 { get; set; }
    public double E22 { get; set; }
    public double G12 { get; set; }
    public double EMax { get; set; }
    public double EMin { get; set; }
    public double EAngle { get; set; }
    public double EVM { get; set; }
    public double G13Avg { get; set; }
    public double G23Avg { get; set; }
    public double GMaxAvg { get; set; }
    public double GAngleAvg { get; set; }
}