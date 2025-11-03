namespace EtabSharp.AnalysisResults.Models.AnalysisResults.SectionCuts;

/// <summary>
/// Represents section cut design forces.
/// </summary>
public class SectionCutDesignResult
{
    public string SectionCut { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    public double P { get; set; }
    public double V2 { get; set; }
    public double V3 { get; set; }
    public double T { get; set; }
    public double M2 { get; set; }
    public double M3 { get; set; }
}