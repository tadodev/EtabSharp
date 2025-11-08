namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

/// <summary>
/// Represents modal participating mass ratios.
/// </summary>
public class ModalParticipatingMassRatioResult
{
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }
    public double Period { get; set; }

    public double UX { get; set; }
    public double UY { get; set; }
    public double UZ { get; set; }
    public double SumUX { get; set; }
    public double SumUY { get; set; }
    public double SumUZ { get; set; }

    public double RX { get; set; }
    public double RY { get; set; }
    public double RZ { get; set; }
    public double SumRX { get; set; }
    public double SumRY { get; set; }
    public double SumRZ { get; set; }
}