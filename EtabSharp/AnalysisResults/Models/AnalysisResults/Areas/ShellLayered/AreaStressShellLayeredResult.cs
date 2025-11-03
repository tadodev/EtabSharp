namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Areas.ShellLayered;

/// <summary>
/// Represents layered shell stresses for area elements.
/// </summary>
public class AreaStressShellLayeredResult
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

    public double S11 { get; set; }
    public double S22 { get; set; }
    public double S12 { get; set; }
    public double SMax { get; set; }
    public double SMin { get; set; }
    public double SAngle { get; set; }
    public double SVM { get; set; }
    public double S13Avg { get; set; }
    public double S23Avg { get; set; }
    public double SMaxAvg { get; set; }
    public double SAngleAvg { get; set; }
}