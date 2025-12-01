using EtabSharp.Design.Concrete.Codes.ACI318_14.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Design.Concrete.Codes;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Design.Concrete.Codes.ACI318_14;

/// <summary>
/// Implementation of ACI 318-14 specific concrete design operations.
/// Implements IACI318_14 interface - code-specific overwrites and preferences only.
/// </summary>
public class ACI318_14Manager : IACI318_14
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;
    private readonly cDCoACI318_14 _aci318_14;

    public ACI318_14Manager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Get the ACI 318-14 design interface
        _aci318_14 = _sapModel.DesignConcrete.ACI318_14;

        _logger.LogDebug("ACI318_14Manager initialized");
    }

    #region Overwrites - Code Specific

    /// <inheritdoc />
    public ConcreteOverwrite GetOverwrite(string frameName, ConcreteOverwriteItem item)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            double value = 0;
            bool progDet = false;

            int ret = _aci318_14.GetOverwrite(frameName, (int)item, ref value, ref progDet);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetOverwrite",
                    $"Failed to get overwrite {item} for frame '{frameName}'. Return code: {ret}");
            }

            var overwrite = new ConcreteOverwrite
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

    /// <inheritdoc />
    public ConcreteOverwriteResults GetAllOverwrites(string frameName)
    {
        var results = new ConcreteOverwriteResults();

        try
        {
            if (string.IsNullOrWhiteSpace(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            // Loop through all overwrite items (1-13)
            for (int itemNum = 1; itemNum <= 13; itemNum++)
            {
                var item = (ConcreteOverwriteItem)itemNum;
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

    /// <inheritdoc />
    public int SetOverwrite(string name, ConcreteOverwriteItem item, double value, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            int ret = _aci318_14.SetOverwrite(name, (int)item, value, itemType);

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

    /// <inheritdoc />
    public int SetMultipleOverwrites(string name, Dictionary<ConcreteOverwriteItem, double> overwrites, eItemType itemType = eItemType.Objects)
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

    /// <inheritdoc />
    public int ResetOverwrite(string name, ConcreteOverwriteItem item, eItemType itemType = eItemType.Objects)
    {
        // Reset by setting value to 0 (program default)
        return SetOverwrite(name, item, 0, itemType);
    }

    #endregion

    #region Preferences - Code Specific

    /// <inheritdoc />
    public ConcretePreference GetPreference(ConcretePreferenceItem item)
    {
        try
        {
            double value = 0;

            int ret = _aci318_14.GetPreference((int)item, ref value);

            if (ret != 0)
            {
                throw new EtabsException(ret, "GetPreference",
                    $"Failed to get preference {item}. Return code: {ret}");
            }

            var preference = new ConcretePreference
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

    /// <inheritdoc />
    public List<ConcretePreference> GetAllPreferences()
    {
        var preferences = new List<ConcretePreference>();

        try
        {
            // Loop through all preference items (1-18)
            for (int itemNum = 1; itemNum <= 18; itemNum++)
            {
                var item = (ConcretePreferenceItem)itemNum;
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

    /// <inheritdoc />
    public int SetPreference(ConcretePreferenceItem item, double value)
    {
        try
        {
            int ret = _aci318_14.SetPreference((int)item, value);

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

    /// <inheritdoc />
    public int SetFramingType(string frameName, int framingType)
    {
        if (framingType < 0 || framingType > 4)
            throw new ArgumentException("Framing type must be between 0 and 4", nameof(framingType));

        return SetOverwrite(frameName, ConcreteOverwriteItem.FramingType, framingType);
    }

    /// <inheritdoc />
    public int SetEffectiveLengthFactors(string frameName, double kMajor, double kMinor)
    {
        var overwrites = new Dictionary<ConcreteOverwriteItem, double>
        {
            { ConcreteOverwriteItem.EffectiveLengthFactorKMajor, kMajor },
            { ConcreteOverwriteItem.EffectiveLengthFactorKMinor, kMinor }
        };

        return SetMultipleOverwrites(frameName, overwrites);
    }

    /// <inheritdoc />
    public int SetUnbracedLengthRatios(string frameName, double ratioMajor, double ratioMinor)
    {
        var overwrites = new Dictionary<ConcreteOverwriteItem, double>
        {
            { ConcreteOverwriteItem.UnbracedLengthRatioMajor, ratioMajor },
            { ConcreteOverwriteItem.UnbracedLengthRatioMinor, ratioMinor }
        };

        return SetMultipleOverwrites(frameName, overwrites);
    }

    /// <inheritdoc />
    public int SetMomentCoefficients(string frameName, double cmMajor, double cmMinor)
    {
        var overwrites = new Dictionary<ConcreteOverwriteItem, double>
        {
            { ConcreteOverwriteItem.MomentCoefficientCmMajor, cmMajor },
            { ConcreteOverwriteItem.MomentCoefficientCmMinor, cmMinor }
        };

        return SetMultipleOverwrites(frameName, overwrites);
    }

    /// <inheritdoc />
    public int SetNonswayMomentFactors(string frameName, double dbMajor, double dbMinor)
    {
        var overwrites = new Dictionary<ConcreteOverwriteItem, double>
        {
            { ConcreteOverwriteItem.NonswayMomentFactorDbMajor, dbMajor },
            { ConcreteOverwriteItem.NonswayMomentFactorDbMinor, dbMinor }
        };

        return SetMultipleOverwrites(frameName, overwrites);
    }

    /// <inheritdoc />
    public int SetSwayMomentFactors(string frameName, double dsMajor, double dsMinor)
    {
        var overwrites = new Dictionary<ConcreteOverwriteItem, double>
        {
            { ConcreteOverwriteItem.SwayMomentFactorDsMajor, dsMajor },
            { ConcreteOverwriteItem.SwayMomentFactorDsMinor, dsMinor }
        };

        return SetMultipleOverwrites(frameName, overwrites);
    }

    #endregion
}