namespace EtabSharp.AnalysisResults.Models.AnalysisResults.SectionCuts;

/// <summary>
/// Represents section cut analysis forces.
/// </summary>
public class SectionCutAnalysisResult
{
    public string SectionCut { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    public double F1 { get; set; }
    public double F2 { get; set; }
    public double F3 { get; set; }
    public double M1 { get; set; }
    public double M2 { get; set; }
    public double M3 { get; set; }

    public override string ToString()
    {
        return $"Section Cut: {SectionCut} - {LoadCase} - F3={F3:F2}";
    }
}