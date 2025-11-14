namespace EtabSharp.AnalysisResultsSetup.Models;

/// <summary>
/// Represents buckling mode range settings.
/// </summary>
public class BucklingModeSettings
{
    /// <summary>
    /// Gets or sets the starting buckling mode number.
    /// </summary>
    public int ModeStart { get; set; } = 1;

    /// <summary>
    /// Gets or sets the ending buckling mode number.
    /// </summary>
    public int ModeEnd { get; set; } = 1;

    /// <summary>
    /// Gets or sets whether all buckling modes should be included.
    /// </summary>
    public bool AllModes { get; set; } = false;

    public BucklingModeSettings()
    {
    }

    public BucklingModeSettings(int modeStart, int modeEnd, bool allModes = false)
    {
        ModeStart = modeStart;
        ModeEnd = modeEnd;
        AllModes = allModes;
    }

    /// <summary>
    /// Creates settings for all buckling modes.
    /// </summary>
    public static BucklingModeSettings AllBucklingModes() => new BucklingModeSettings(1, 1, true);

    /// <summary>
    /// Creates settings for a single buckling mode.
    /// </summary>
    public static BucklingModeSettings SingleMode(int modeNumber) => new BucklingModeSettings(modeNumber, modeNumber, false);

    /// <summary>
    /// Creates settings for a range of buckling modes.
    /// </summary>
    public static BucklingModeSettings ModeRange(int startMode, int endMode) => new BucklingModeSettings(startMode, endMode, false);

    public override string ToString()
    {
        return AllModes ? "All Buckling Modes" : $"Buckling Modes {ModeStart} to {ModeEnd}";
    }
}