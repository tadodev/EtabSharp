namespace EtabSharp.AnalysisResults.Models.AnalysisResults.Modals;

/// <summary>
/// Represents modal period and frequency data.
/// </summary>
public class ModalPeriodResult
{
    public string LoadCase { get; set; } = string.Empty;
    public string StepType { get; set; } = string.Empty;
    public double StepNum { get; set; }

    /// <summary>
    /// Modal period (seconds).
    /// </summary>
    public double Period { get; set; }

    /// <summary>
    /// Modal frequency (cycles/sec).
    /// </summary>
    public double Frequency { get; set; }

    /// <summary>
    /// Circular frequency (radians/sec).
    /// </summary>
    public double CircularFrequency { get; set; }

    /// <summary>
    /// Eigenvalue.
    /// </summary>
    public double EigenValue { get; set; }

    public override string ToString()
    {
        return $"Mode {StepNum}: T={Period:F4}s, f={Frequency:F4}Hz";
    }
}