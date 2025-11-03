namespace EtabSharp.AnalysisResults.Models.AnalysisResults.BaseReactions;

/// <summary>
/// Represents base reaction with centroid information.
/// </summary>
public class BaseReactionWithCentroidResult
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

    // Centroid coordinates for FX
    public double XCentroidForFX { get; set; }
    public double YCentroidForFX { get; set; }
    public double ZCentroidForFX { get; set; }

    // Centroid coordinates for FY
    public double XCentroidForFY { get; set; }
    public double YCentroidForFY { get; set; }
    public double ZCentroidForFY { get; set; }

    // Centroid coordinates for FZ
    public double XCentroidForFZ { get; set; }
    public double YCentroidForFZ { get; set; }
    public double ZCentroidForFZ { get; set; }
}