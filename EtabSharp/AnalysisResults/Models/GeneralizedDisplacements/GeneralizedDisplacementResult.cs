namespace EtabSharp.AnalysisResults.Models.AnalysisResults.GeneralizedDisplacements;

/// <summary>
/// Represents generalized displacement results.
/// </summary>
public class GeneralizedDisplacementResult
{
    public string GeneralizedDisplacement { get; set; } = string.Empty;
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }
    public string DisplacementType { get; set; } = string.Empty;
    public double Value { get; set; }

    public override string ToString()
    {
        return $"Gen Displ: {GeneralizedDisplacement} - {LoadCase} - Type={DisplacementType}, Value={Value:F6}";
    }
}