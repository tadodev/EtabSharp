using ETABSv1;

namespace EtabSharp.Loads.Models;

/// <summary>
/// Helper class for load pattern type information.
/// Only includes types valid for ETABS and SAFE.
/// </summary>
public static class LoadPatternTypeHelper
{
    /// <summary>
    /// Gets a friendly description for a load pattern type.
    /// </summary>
    public static string GetDescription(eLoadPatternType loadType)
    {
        return loadType switch
        {
            eLoadPatternType.Dead => "Dead Load - Permanent gravity loads (self-weight, floor finish, etc.)",
            eLoadPatternType.SuperDead => "Super Dead Load - Additional permanent loads applied after construction",
            eLoadPatternType.Live => "Live Load - Occupancy and movable loads",
            eLoadPatternType.ReduceLive => "Reducible Live Load - Live loads that can be reduced per code",
            eLoadPatternType.Quake => "Seismic Load - Earthquake forces",
            eLoadPatternType.Wind => "Wind Load - Wind pressure and suction",
            eLoadPatternType.Snow => "Snow Load - Snow accumulation on roof",
            eLoadPatternType.Other => "Other Load - General purpose load pattern",
            eLoadPatternType.Temperature => "Temperature Load - Thermal effects",
            eLoadPatternType.Rooflive => "Roof Live Load - Maintenance and construction loads on roof",
            eLoadPatternType.Notional => "Notional Load - Hypothetical lateral loads for stability",
            eLoadPatternType.PatternLive => "Pattern Live Load - Patterned application of live loads",
            eLoadPatternType.Prestress => "Prestress Load - Post-tensioning and prestressing forces",
            eLoadPatternType.Construction => "Construction Load - Construction stage loads",
            eLoadPatternType.PatternAuto => "Pattern Auto - Automatically generated pattern",
            eLoadPatternType.QuakeDrift => "Seismic Drift - Earthquake drift forces",
            _ => "Unknown or Invalid Load Type for ETABS/SAFE"
        };
    }

    /// <summary>
    /// Gets the typical self-weight multiplier for a load type.
    /// </summary>
    public static double GetTypicalSelfWeightMultiplier(eLoadPatternType loadType)
    {
        return loadType switch
        {
            eLoadPatternType.Dead => 1.0,
            eLoadPatternType.SuperDead => 0.0,
            _ => 0.0
        };
    }

    /// <summary>
    /// Checks if a load type is typically gravity-based.
    /// </summary>
    public static bool IsGravityLoad(eLoadPatternType loadType)
    {
        return loadType switch
        {
            eLoadPatternType.Dead => true,
            eLoadPatternType.SuperDead => true,
            eLoadPatternType.Live => true,
            eLoadPatternType.ReduceLive => true,
            eLoadPatternType.Rooflive => true,
            eLoadPatternType.Snow => true,
            eLoadPatternType.PatternLive => true,
            _ => false
        };
    }

    /// <summary>
    /// Checks if a load type is typically lateral.
    /// </summary>
    public static bool IsLateralLoad(eLoadPatternType loadType)
    {
        return loadType switch
        {
            eLoadPatternType.Quake => true,
            eLoadPatternType.Wind => true,
            eLoadPatternType.Notional => true,
            eLoadPatternType.QuakeDrift => true,
            _ => false
        };
    }

    /// <summary>
    /// Checks if a load pattern type is valid for ETABS and SAFE.
    /// </summary>
    public static bool IsValidForEtabs(eLoadPatternType loadType)
    {
        return loadType switch
        {
            eLoadPatternType.Dead => true,
            eLoadPatternType.SuperDead => true,
            eLoadPatternType.Live => true,
            eLoadPatternType.ReduceLive => true,
            eLoadPatternType.Quake => true,
            eLoadPatternType.Wind => true,
            eLoadPatternType.Snow => true,
            eLoadPatternType.Other => true,
            eLoadPatternType.Temperature => true,
            eLoadPatternType.Rooflive => true,
            eLoadPatternType.Notional => true,
            eLoadPatternType.PatternLive => true,
            eLoadPatternType.Prestress => true,
            eLoadPatternType.Construction => true,
            eLoadPatternType.PatternAuto => true,
            eLoadPatternType.QuakeDrift => true,
            _ => false
        };
    }

    /// <summary>
    /// Gets all load pattern types valid for ETABS and SAFE.
    /// </summary>
    public static eLoadPatternType[] GetValidTypesForEtabs()
    {
        return new[]
        {
            eLoadPatternType.Dead,
            eLoadPatternType.SuperDead,
            eLoadPatternType.Live,
            eLoadPatternType.ReduceLive,
            eLoadPatternType.Quake,
            eLoadPatternType.Wind,
            eLoadPatternType.Snow,
            eLoadPatternType.Other,
            eLoadPatternType.Temperature,
            eLoadPatternType.Rooflive,
            eLoadPatternType.Notional,
            eLoadPatternType.PatternLive,
            eLoadPatternType.Prestress,
            eLoadPatternType.Construction,
            eLoadPatternType.PatternAuto,
            eLoadPatternType.QuakeDrift
        };
    }

    /// <summary>
    /// Gets common load pattern types for typical building projects.
    /// </summary>
    public static eLoadPatternType[] GetCommonBuildingTypes()
    {
        return new[]
        {
            eLoadPatternType.Dead,
            eLoadPatternType.SuperDead,
            eLoadPatternType.Live,
            eLoadPatternType.ReduceLive,
            eLoadPatternType.Rooflive,
            eLoadPatternType.Snow,
            eLoadPatternType.Quake,
            eLoadPatternType.Wind,
            eLoadPatternType.Temperature,
            eLoadPatternType.Notional
        };
    }
}