namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

/// <summary>
/// Represents modal participation factors.
/// </summary>
public class ModalParticipationFactorResult
{
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }
    public double Period { get; set; }

    public double UX { get; set; }
    public double UY { get; set; }
    public double UZ { get; set; }
    public double RX { get; set; }
    public double RY { get; set; }
    public double RZ { get; set; }

    public double ModalMass { get; set; }
    public double ModalStiffness { get; set; }

    public override string ToString()
    {
        return $"Mode {StepNum}: UX={UX:F4}, UY={UY:F4}, UZ={UZ:F4}";
    }
}