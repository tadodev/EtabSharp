using EtabSharp.Design.Concrete.Codes.ACI318_14.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Design.Concrete.Codes;


/// <summary>
/// Interface for ACI 318-14 specific concrete design operations.
/// Provides methods for getting/setting design overwrites and preferences specific to ACI 318-14.
/// Maps to cDCoACI318_14 interface in ETABS API.
/// </summary>
public interface IACI318_14
{
    #region Overwrites - Code Specific

    /// <summary>
    /// Gets the value of a concrete design overwrite item for a frame.
    /// Wraps cDCoACI318_14.GetOverwrite.
    /// </summary>
    ConcreteOverwrite GetOverwrite(string frameName, ConcreteOverwriteItem item);

    /// <summary>
    /// Gets all overwrite items for a frame.
    /// </summary>
    ConcreteOverwriteResults GetAllOverwrites(string frameName);

    /// <summary>
    /// Sets the value of a concrete design overwrite item for a frame or group.
    /// Wraps cDCoACI318_14.SetOverwrite.
    /// </summary>
    int SetOverwrite(string name, ConcreteOverwriteItem item, double value, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets multiple overwrites for a frame or group.
    /// </summary>
    int SetMultipleOverwrites(string name, Dictionary<ConcreteOverwriteItem, double> overwrites, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Resets a specific overwrite to program default.
    /// </summary>
    int ResetOverwrite(string name, ConcreteOverwriteItem item, eItemType itemType = eItemType.Objects);

    #endregion

    #region Preferences - Code Specific

    /// <summary>
    /// Gets the value of a concrete design preference item.
    /// Wraps cDCoACI318_14.GetPreference.
    /// Note: Users should typically set preferences in ETABS UI.
    /// </summary>
    ConcretePreference GetPreference(ConcretePreferenceItem item);

    /// <summary>
    /// Gets all preference items.
    /// </summary>
    List<ConcretePreference> GetAllPreferences();

    /// <summary>
    /// Sets the value of a concrete design preference item.
    /// Wraps cDCoACI318_14.SetPreference.
    /// Warning: Typically preferences should be set in ETABS UI, not programmatically.
    /// </summary>
    int SetPreference(ConcretePreferenceItem item, double value);

    #endregion

    #region Convenience Methods - Code Specific

    /// <summary>
    /// Sets framing type overwrite for a frame.
    /// </summary>
    /// <param name="frameName">Name of frame</param>
    /// <param name="framingType">Framing type (0=Program Default, 1=Sway Special, 2=Sway Intermediate, 3=Sway Ordinary, 4=Non-sway)</param>
    int SetFramingType(string frameName, int framingType);

    /// <summary>
    /// Sets effective length factors K for a frame.
    /// </summary>
    int SetEffectiveLengthFactors(string frameName, double kMajor, double kMinor);

    /// <summary>
    /// Sets unbraced length ratios for a frame.
    /// </summary>
    int SetUnbracedLengthRatios(string frameName, double ratioMajor, double ratioMinor);

    /// <summary>
    /// Sets moment coefficients for a frame.
    /// </summary>
    int SetMomentCoefficients(string frameName, double cmMajor, double cmMinor);

    /// <summary>
    /// Sets nonsway moment factors Db for a frame.
    /// </summary>
    int SetNonswayMomentFactors(string frameName, double dbMajor, double dbMinor);

    /// <summary>
    /// Sets sway moment factors Ds for a frame.
    /// </summary>
    int SetSwayMomentFactors(string frameName, double dsMajor, double dsMinor);

    #endregion
}