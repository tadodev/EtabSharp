using EtabSharp.Interfaces;
using EtabSharp.Models.UnitSystem;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.UnitSystem;

public class UnitSystem: IUnitSystem
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;
    private Units _cachedUnits = new(); // Default to metric

    public UnitSystem(cSapModel sapModel, ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
    }

    /// <summary>
    /// Sets the current ETABS unit system and updates the internal cache.
    /// </summary>
    public int SetPresentUnits(Units systemType)
    {
        if (systemType == null)
        {
            _logger.LogWarning("Attempted to set present units with a null systemType.");
            return -1; // Indicate failure
        }

        if (_sapModel == null)
        {
            _logger.LogError("ETABS model reference is null. Cannot set present units.");
            return -1;
        }

        try
        {
            _logger.LogInformation(
                "Setting ETABS present units to Force={Force}, Length={Length}, Temperature={Temperature}.",
                systemType.Force, systemType.Length, systemType.Temperature);

            int ret = _sapModel.SetPresentUnits_2(systemType.Force, systemType.Length, systemType.Temperature);

            if (ret == 0)
            {
                _cachedUnits = systemType;
                _logger.LogInformation("ETABS present units successfully updated.");
            }
            else
            {
                _logger.LogWarning("ETABS failed to set present units. Return code: {Ret}.", ret);
            }

            return ret;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while setting ETABS present units.");
            return -1;
        }
    }

    /// <summary>
    /// Retrieves the current ETABS present unit system and updates the internal cache.
    /// </summary>
    public Units GetPresentUnits()
    {
        if (_sapModel == null)
        {
            _logger.LogError("ETABS model reference is null. Cannot retrieve present units.");
            return _cachedUnits; // Return cached or default
        }

        try
        {
            var force = _cachedUnits.Force;
            var length = _cachedUnits.Length;
            var temperature = _cachedUnits.Temperature;

            int ret = _sapModel.GetPresentUnits_2(ref force, ref length, ref temperature);

            if (ret == 0)
            {
                _cachedUnits.Force = force;
                _cachedUnits.Length = length;
                _cachedUnits.Temperature = temperature;

                _logger.LogInformation(
                    "Retrieved ETABS units: Force={Force}, Length={Length}, Temperature={Temperature}.",
                    force, length, temperature);
            }
            else
            {
                _logger.LogWarning("ETABS failed to retrieve present units. Return code: {Ret}.", ret);
            }

            return _cachedUnits;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving ETABS present units.");
            return _cachedUnits;
        }
    }
}