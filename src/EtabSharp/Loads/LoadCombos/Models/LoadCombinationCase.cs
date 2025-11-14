namespace EtabSharp.Loads.LoadCombos.Models;

/// <summary>
/// Represents a single case within a load combination.
/// </summary>
public class LoadCombinationCase
{
    /// <summary>
    /// Type of case (LoadCase or LoadCombo)
    /// </summary>
    public ETABSv1.eCNameType CaseType { get; set; }

    /// <summary>
    /// Name of the load case or combination
    /// </summary>
    public required string CaseName { get; set; }

    /// <summary>
    /// Scale factor applied to this case
    /// </summary>
    public double ScaleFactor { get; set; } = 1.0;

    /// <summary>
    /// Mode number (for modal cases, 0 if not applicable)
    /// </summary>
    public int ModeNumber { get; set; }

    /// <summary>
    /// Gets whether this is a load case (vs load combo)
    /// </summary>
    public bool IsLoadCase => CaseType == ETABSv1.eCNameType.LoadCase;

    /// <summary>
    /// Gets whether this is a load combo
    /// </summary>
    public bool IsLoadCombo => CaseType == ETABSv1.eCNameType.LoadCombo;

    /// <summary>
    /// Gets whether this case uses a specific mode
    /// </summary>
    public bool HasModeNumber => ModeNumber > 0;

    /// <summary>
    /// Creates a load case component
    /// </summary>
    public static LoadCombinationCase FromLoadCase(string caseName, double scaleFactor = 1.0, int modeNumber = 0)
    {
        return new LoadCombinationCase
        {
            CaseType = ETABSv1.eCNameType.LoadCase,
            CaseName = caseName,
            ScaleFactor = scaleFactor,
            ModeNumber = modeNumber
        };
    }

    /// <summary>
    /// Creates a load combo component
    /// </summary>
    public static LoadCombinationCase FromLoadCombo(string comboName, double scaleFactor = 1.0)
    {
        return new LoadCombinationCase
        {
            CaseType = ETABSv1.eCNameType.LoadCombo,
            CaseName = comboName,
            ScaleFactor = scaleFactor,
            ModeNumber = 0
        };
    }

    public override string ToString()
    {
        string typeStr = IsLoadCase ? "LoadCase" : "LoadCombo";
        string modeStr = HasModeNumber ? $" Mode {ModeNumber}" : "";
        return $"{ScaleFactor:F2} × {CaseName} ({typeStr}){modeStr}";
    }
}