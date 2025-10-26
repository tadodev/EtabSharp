namespace EtabSharp.AnalysisResults.Models.AnalysisResultsSetup;

/// <summary>
/// Summary of analysis results setup configuration.
/// </summary>
public class AnalysisResultsSetupSummary
{
    /// <summary>
    /// Number of cases selected for output.
    /// </summary>
    public int SelectedCasesCount { get; set; }

    /// <summary>
    /// Number of combinations selected for output.
    /// </summary>
    public int SelectedCombosCount { get; set; }

    /// <summary>
    /// Base reaction location settings.
    /// </summary>
    public BaseReactionLocation? BaseReactionLocation { get; set; }

    /// <summary>
    /// Buckling mode settings.
    /// </summary>
    public BucklingModeSettings? BucklingModes { get; set; }

    /// <summary>
    /// Mode shape settings.
    /// </summary>
    public ModeShapeSettings? ModeShapes { get; set; }

    /// <summary>
    /// Direct history output option.
    /// </summary>
    public int DirectHistoryOption { get; set; }

    /// <summary>
    /// Modal history output option.
    /// </summary>
    public int ModalHistoryOption { get; set; }

    /// <summary>
    /// Multi-step static output option.
    /// </summary>
    public int MultiStepStaticOption { get; set; }

    /// <summary>
    /// Nonlinear static output option.
    /// </summary>
    public int NonlinearStaticOption { get; set; }

    /// <summary>
    /// Multi-valued combo output option.
    /// </summary>
    public int MultiValuedComboOption { get; set; }

    public override string ToString()
    {
        return $"Analysis Results Setup: {SelectedCasesCount} cases, {SelectedCombosCount} combos selected | " +
               $"Base Reaction: ({BaseReactionLocation?.GlobalX:F2}, {BaseReactionLocation?.GlobalY:F2}, {BaseReactionLocation?.GlobalZ:F2})";
    }
}