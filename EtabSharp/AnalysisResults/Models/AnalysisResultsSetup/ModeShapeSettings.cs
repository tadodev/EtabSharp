namespace EtabSharp.AnalysisResults.Models.AnalysisResultsSetup;

/// <summary>
/// Represents mode shape range settings.
/// </summary>
public class ModeShapeSettings
{
    /// <summary>
    /// Gets or sets the starting mode shape number.
    /// </summary>
    public int ModeStart { get; set; } = 1;

    /// <summary>
    /// Gets or sets the ending mode shape number.
    /// </summary>
    public int ModeEnd { get; set; } = 12;

    /// <summary>
    /// Gets or sets whether all mode shapes should be included.
    /// </summary>
    public bool AllModes { get; set; } = false;

    public ModeShapeSettings()
    {
    }

    public ModeShapeSettings(int modeStart, int modeEnd, bool allModes = false)
    {
        ModeStart = modeStart;
        ModeEnd = modeEnd;
        AllModes = allModes;
    }

    /// <summary>
    /// Creates settings for all mode shapes.
    /// </summary>
    public static ModeShapeSettings AllModeShapes() => new ModeShapeSettings(1, 1, true);

    /// <summary>
    /// Creates settings for a single mode shape.
    /// </summary>
    public static ModeShapeSettings SingleMode(int modeNumber) => new ModeShapeSettings(modeNumber, modeNumber, false);

    /// <summary>
    /// Creates settings for a range of mode shapes.
    /// </summary>
    public static ModeShapeSettings ModeRange(int startMode, int endMode) => new ModeShapeSettings(startMode, endMode, false);

    /// <summary>
    /// Creates settings for first N mode shapes.
    /// </summary>
    public static ModeShapeSettings FirstNModes(int count) => new ModeShapeSettings(1, count, false);

    public override string ToString()
    {
        return AllModes ? "All Mode Shapes" : $"Mode Shapes {ModeStart} to {ModeEnd}";
    }
}