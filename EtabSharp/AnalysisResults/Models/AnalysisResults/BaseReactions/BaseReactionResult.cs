namespace EtabSharp.AnalysisResults.Models.AnalysisResults.BaseReactions;

/// <summary>
/// Represents base reaction results.
/// </summary>
public class BaseReactionResult
{
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    public double FX { get; set; }
    public double FY { get; set; }
    public double FZ { get; set; }
    public double MX { get; set; }
    public double MY { get; set; }
    public double MZ { get; set; }

    public override string ToString()
    {
        return $"Base React: {LoadCase} - FX={FX:F2}, FY={FY:F2}, FZ={FZ:F2}";
    }
}