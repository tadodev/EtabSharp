namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Bucklings;

/// <summary>
/// Represents buckling factor results.
/// </summary>
public class BucklingFactorResult
{
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }
    public double Factor { get; set; }

    public override string ToString()
    {
        return $"Buckling Mode {StepNum}: Factor={Factor:F4}";
    }
}