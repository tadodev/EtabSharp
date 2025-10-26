namespace EtabSharp.AnalysisResults.Models.AnalysisResultsSetup;

/// <summary>
/// Enumeration for output step options in time history and nonlinear analyses.
/// </summary>
public enum OutputStepOption
{
    /// <summary>
    /// Output all steps.
    /// </summary>
    AllSteps = 1,

    /// <summary>
    /// Output last step only.
    /// </summary>
    LastStep = 2,

    /// <summary>
    /// Output at specified intervals.
    /// </summary>
    SpecifiedSteps = 3,

    /// <summary>
    /// Output envelopes (min/max).
    /// </summary>
    Envelopes = 4
}