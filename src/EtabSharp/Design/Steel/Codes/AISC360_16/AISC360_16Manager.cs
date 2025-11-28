using EtabSharp.Design.Steel.Codes.AISC360_16.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Design.Steel.Codes;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Design.Steel.Codes.AISC360_16;

/// <summary>
/// Implementation of AISC 360-16 specific steel design operations.
/// Implements IAISC360_16 interface - code-specific overwrites and preferences only.
/// </summary>
public class AISC360_16Manager: IAISC360_16
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;
    private readonly cDStAISC360_16 _aisc360_16;

    public AISC360_16Manager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Get the AISC 360-16 design interface
        _aisc360_16 = _sapModel.DesignSteel.AISC360_16;

        _logger.LogDebug("AISC360_16Manager initialized");
    }

    #region Overwrites - Code Specific

    public SteelOverwrite GetOverwrite(string frameName, SteelOverwriteItem item)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            double value = 0;
            bool progDet = false;

            int ret = _aisc360_16.GetOverwrite(frameName, (int)item, ref value, ref progDet);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetOverwrite",
                    $"Failed to get overwrite {item} for frame '{frameName}'. Return code: {ret}");
            }

            var overwrite = new SteelOverwrite
            {
                FrameName = frameName,
                Item = item,
                Value = value,
                IsProgramDetermined = progDet
            };

            _logger.LogDebug("Retrieved overwrite {Item} for frame '{Frame}': Value={Value}, ProgDet={ProgDet}",
                item, frameName, value, progDet);

            return overwrite;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error getting overwrite {item} for frame '{frameName}': {ex.Message}", ex);
        }
    }

    public SteelOverwriteResults GetAllOverwrites(string frameName)
    {
        var results = new SteelOverwriteResults();

        try
        {
            if (string.IsNullOrWhiteSpace(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            // Loop through all overwrite items (2-51)
            for (int itemNum = 2; itemNum <= 51; itemNum++)
            {
                var item = (SteelOverwriteItem)itemNum;
                try
                {
                    var overwrite = GetOverwrite(frameName, item);
                    results.Overwrites.Add(overwrite);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to get overwrite {Item} for frame '{Frame}': {Error}",
                        item, frameName, ex.Message);
                }
            }

            results.IsSuccess = true;
            results.ReturnCode = 0;

            _logger.LogDebug("Retrieved {Count} overwrites for frame '{Frame}'", results.Count, frameName);

            return results;
        }
        catch (Exception ex) when (!(ex is ArgumentException))
        {
            results.IsSuccess = false;
            results.ErrorMessage = $"Unexpected error getting all overwrites for frame '{frameName}': {ex.Message}";
            throw new EtabsException(results.ErrorMessage, ex);
        }
    }

    public int SetOverwrite(string name, SteelOverwriteItem item, double value, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            int ret = _aisc360_16.SetOverwrite(name, (int)item, value, itemType);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetOverwrite",
                    $"Failed to set overwrite {item} for '{name}'. Return code: {ret}");
            }

            _logger.LogDebug("Set overwrite {Item} for '{Name}' ({ItemType}): Value={Value}",
                item, name, itemType, value);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException) && !(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting overwrite {item} for '{name}': {ex.Message}", ex);
        }
    }

    public int SetMultipleOverwrites(string name, Dictionary<SteelOverwriteItem, double> overwrites, eItemType itemType = eItemType.Objects)
    {
        int successCount = 0;

        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            if (overwrites == null || !overwrites.Any())
                throw new ArgumentException("Overwrites dictionary cannot be null or empty", nameof(overwrites));

            foreach (var overwrite in overwrites)
            {
                try
                {
                    int ret = SetOverwrite(name, overwrite.Key, overwrite.Value, itemType);
                    if (ret == 0)
                        successCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to set overwrite {Item} for '{Name}': {Error}",
                        overwrite.Key, name, ex.Message);
                }
            }

            _logger.LogDebug("Set {Success}/{Total} overwrites for '{Name}'", successCount, overwrites.Count, name);

            return successCount;
        }
        catch (Exception ex) when (!(ex is ArgumentException))
        {
            throw new EtabsException($"Unexpected error setting multiple overwrites for '{name}': {ex.Message}", ex);
        }
    }

    public int ResetOverwrite(string name, SteelOverwriteItem item, eItemType itemType = eItemType.Objects)
    {
        // Reset by setting value to 0 (program default)
        return SetOverwrite(name, item, 0, itemType);
    }

    #endregion

    #region Preferences - Code Specific

    public SteelPreference GetPreference(SteelPreferenceItem item)
    {
        try
        {
            double value = 0;

            int ret = _aisc360_16.GetPreference((int)item, ref value);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetPreference",
                    $"Failed to get preference {item}. Return code: {ret}");
            }

            var preference = new SteelPreference
            {
                Item = item,
                Value = value
            };

            _logger.LogDebug("Retrieved preference {Item}: Value={Value}", item, value);

            return preference;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting preference {item}: {ex.Message}", ex);
        }
    }

    public List<SteelPreference> GetAllPreferences()
    {
        var preferences = new List<SteelPreference>();

        try
        {
            // Loop through all preference items (2-45)
            for (int itemNum = 2; itemNum <= 45; itemNum++)
            {
                var item = (SteelPreferenceItem)itemNum;
                try
                {
                    var preference = GetPreference(item);
                    preferences.Add(preference);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to get preference {Item}: {Error}", item, ex.Message);
                }
            }

            _logger.LogDebug("Retrieved {Count} preferences", preferences.Count);

            return preferences;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting all preferences: {ex.Message}", ex);
        }
    }

    public int SetPreference(SteelPreferenceItem item, double value)
    {
        try
        {
            int ret = _aisc360_16.SetPreference((int)item, value);

            if (ret != 0)
            {
                throw new EtabsException(ret, "SetPreference",
                    $"Failed to set preference {item}. Return code: {ret}");
            }

            _logger.LogDebug("Set preference {Item}: Value={Value}", item, value);
            _logger.LogWarning("Preference {Item} was set programmatically. Consider setting preferences in ETABS UI instead.", item);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting preference {item}: {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods - Code Specific

    public int SetFramingType(string frameName, int framingType)
    {
        if (framingType < 1 || framingType > 8)
            throw new ArgumentException("Framing type must be between 1 and 8", nameof(framingType));

        return SetOverwrite(frameName, SteelOverwriteItem.FramingType, framingType);
    }

    public int SetEffectiveLengthFactors(string frameName, double k1Major, double k1Minor, double k2Major = 0, double k2Minor = 0)
    {
        var overwrites = new Dictionary<SteelOverwriteItem, double>
        {
            { SteelOverwriteItem.EffectiveLengthFactorK1Major, k1Major },
            { SteelOverwriteItem.EffectiveLengthFactorK1Minor, k1Minor }
        };

        if (k2Major > 0)
            overwrites.Add(SteelOverwriteItem.EffectiveLengthFactorK2Major, k2Major);

        if (k2Minor > 0)
            overwrites.Add(SteelOverwriteItem.EffectiveLengthFactorK2Minor, k2Minor);

        return SetMultipleOverwrites(frameName, overwrites);
    }

    public int SetUnbracedLengthRatios(string frameName, double ratioMajor, double ratioMinor, double ratioLTB = 0)
    {
        var overwrites = new Dictionary<SteelOverwriteItem, double>
        {
            { SteelOverwriteItem.UnbracedLengthRatioMajor, ratioMajor },
            { SteelOverwriteItem.UnbracedLengthRatioMinor, ratioMinor }
        };

        if (ratioLTB > 0)
            overwrites.Add(SteelOverwriteItem.UnbracedLengthRatioLTB, ratioLTB);

        return SetMultipleOverwrites(frameName, overwrites);
    }

    public int SetMomentCoefficients(string frameName, double cmMajor, double cmMinor, double cb = 0)
    {
        var overwrites = new Dictionary<SteelOverwriteItem, double>
        {
            { SteelOverwriteItem.MomentCoefficientCmMajor, cmMajor },
            { SteelOverwriteItem.MomentCoefficientCmMinor, cmMinor }
        };

        if (cb > 0)
            overwrites.Add(SteelOverwriteItem.BendingCoefficientCb, cb);

        return SetMultipleOverwrites(frameName, overwrites);
    }

    public int SetDeflectionLimits(string frameName, double dlLimit = 360, double llLimit = 360, double totalLimit = 240)
    {
        var overwrites = new Dictionary<SteelOverwriteItem, double>
        {
            { SteelOverwriteItem.ConsiderDeflection, 2 }, // 2 = Yes
            { SteelOverwriteItem.DeflectionCheckType, 1 }  // 1 = Ratio
        };

        if (dlLimit > 0)
            overwrites.Add(SteelOverwriteItem.DLDeflectionLimitRatio, dlLimit);

        if (llLimit > 0)
            overwrites.Add(SteelOverwriteItem.LLDeflectionLimitRatio, llLimit);

        if (totalLimit > 0)
            overwrites.Add(SteelOverwriteItem.TotalDeflectionLimitRatio, totalLimit);

        return SetMultipleOverwrites(frameName, overwrites);
    }

    #endregion
}