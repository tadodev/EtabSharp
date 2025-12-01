using EtabSharp.Design.Steel.Codes.AISC360_16.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Design.Steel.Codes;

/// <summary>
/// Interface for AISC 360-16 specific steel design operations.
/// Provides methods for getting/setting design overwrites and preferences specific to AISC 360-16.
/// Maps to cDStAISC360_16 interface in ETABS API.
/// </summary>
public interface IAISC360_16
{
    #region Overwrites - Code Specific

    /// <summary>
    /// Gets the value of a steel design overwrite item for a frame.
    /// Wraps cDStAISC360_16.GetOverwrite.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="item">Overwrite item to retrieve</param>
    /// <returns>SteelOverwrite with value and program-determined flag</returns>
    SteelOverwrite GetOverwrite(string frameName, SteelOverwriteItem item);

    /// <summary>
    /// Gets all overwrite items for a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>SteelOverwriteResults containing all overwrites for the frame</returns>
    SteelOverwriteResults GetAllOverwrites(string frameName);

    /// <summary>
    /// Sets the value of a steel design overwrite item for a frame or group.
    /// Wraps cDStAISC360_16.SetOverwrite.
    /// </summary>
    /// <param name="name">Name of frame object, group, or selection</param>
    /// <param name="item">Overwrite item to set</param>
    /// <param name="value">Value to set</param>
    /// <param name="itemType">Type of item (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOverwrite(string name, SteelOverwriteItem item, double value, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets multiple overwrites for a frame or group.
    /// </summary>
    /// <param name="name">Name of frame object, group, or selection</param>
    /// <param name="overwrites">Dictionary of overwrite items and values</param>
    /// <param name="itemType">Type of item (Objects, Group, or SelectedObjects)</param>
    /// <returns>Number of successfully set overwrites</returns>
    int SetMultipleOverwrites(string name, Dictionary<SteelOverwriteItem, double> overwrites, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Resets a specific overwrite to program default.
    /// </summary>
    /// <param name="name">Name of frame object, group, or selection</param>
    /// <param name="item">Overwrite item to reset</param>
    /// <param name="itemType">Type of item (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ResetOverwrite(string name, SteelOverwriteItem item, eItemType itemType = eItemType.Objects);

    #endregion

    #region Preferences - Code Specific

    /// <summary>
    /// Gets the value of a steel design preference item.
    /// Wraps cDStAISC360_16.GetPreference.
    /// Note: Users should typically set preferences in ETABS UI.
    /// </summary>
    /// <param name="item">Preference item to retrieve</param>
    /// <returns>SteelPreference with the current value</returns>
    SteelPreference GetPreference(SteelPreferenceItem item);

    /// <summary>
    /// Gets all preference items.
    /// </summary>
    /// <returns>List of all current preference settings</returns>
    List<SteelPreference> GetAllPreferences();

    /// <summary>
    /// Sets the value of a steel design preference item.
    /// Wraps cDStAISC360_16.SetPreference.
    /// Warning: Typically preferences should be set in ETABS UI, not programmatically.
    /// </summary>
    /// <param name="item">Preference item to set</param>
    /// <param name="value">Value to set</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetPreference(SteelPreferenceItem item, double value);

    #endregion

    #region Convenience Methods - Code Specific

    /// <summary>
    /// Sets framing type overwrite for a frame.
    /// </summary>
    /// <param name="frameName">Name of frame</param>
    /// <param name="framingType">Framing type (1=SMF, 2=IMF, 3=OMF, 4=SCBF, 5=OCBF, 6=OCBFI, 7=EBF, 8=BRBF)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetFramingType(string frameName, int framingType);

    /// <summary>
    /// Sets effective length factors K for a frame.
    /// </summary>
    /// <param name="frameName">Name of frame</param>
    /// <param name="k1Major">K1 for major axis</param>
    /// <param name="k1Minor">K1 for minor axis</param>
    /// <param name="k2Major">K2 for major axis (optional)</param>
    /// <param name="k2Minor">K2 for minor axis (optional)</param>
    /// <returns>Number of successfully set K factors</returns>
    int SetEffectiveLengthFactors(string frameName, double k1Major, double k1Minor, double k2Major = 0, double k2Minor = 0);

    /// <summary>
    /// Sets unbraced length ratios for a frame.
    /// </summary>
    /// <param name="frameName">Name of frame</param>
    /// <param name="ratioMajor">Ratio for major axis</param>
    /// <param name="ratioMinor">Ratio for minor axis</param>
    /// <param name="ratioLTB">Ratio for lateral-torsional buckling (optional)</param>
    /// <returns>Number of successfully set ratios</returns>
    int SetUnbracedLengthRatios(string frameName, double ratioMajor, double ratioMinor, double ratioLTB = 0);

    /// <summary>
    /// Sets moment coefficients for a frame.
    /// </summary>
    /// <param name="frameName">Name of frame</param>
    /// <param name="cmMajor">Cm for major axis</param>
    /// <param name="cmMinor">Cm for minor axis</param>
    /// <param name="cb">Cb for lateral-torsional buckling (optional)</param>
    /// <returns>Number of successfully set coefficients</returns>
    int SetMomentCoefficients(string frameName, double cmMajor, double cmMinor, double cb = 0);

    /// <summary>
    /// Enables deflection checking for a frame with ratio limits.
    /// </summary>
    /// <param name="frameName">Name of frame</param>
    /// <param name="dlLimit">DL limit (L/Value), 0 for no check</param>
    /// <param name="llLimit">LL limit (L/Value), 0 for no check</param>
    /// <param name="totalLimit">Total limit (L/Value), 0 for no check</param>
    /// <returns>Number of successfully set parameters</returns>
    int SetDeflectionLimits(string frameName, double dlLimit = 360, double llLimit = 360, double totalLimit = 240);

    #endregion
}