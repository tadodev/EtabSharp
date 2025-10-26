namespace EtabSharp.AnalysisResults.Models.AnalysisResultsSetup;

/// <summary>
/// Enumeration for multi-valued combination output options.
/// </summary>
public enum MultiValuedComboOption
{
    /// <summary>
    /// Output multiple values for combination.
    /// </summary>
    Multiple = 1,

    /// <summary>
    /// Output envelopes only.
    /// </summary>
    Envelopes = 2,

    /// <summary>
    /// Output specific values.
    /// </summary>
    Specific = 3
}