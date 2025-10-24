using ETABSv1;

namespace EtabSharp.Loads.Models;

/// <summary>
/// Represents a load pattern in the ETABS model.
/// Load patterns define the spatial distribution and nature of loads.
/// </summary>
public class LoadPattern
{
    /// <summary>
    /// Gets or sets the unique name of the load pattern.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of load pattern.
    /// Only types valid for ETABS and SAFE are supported.
    /// </summary>
    public eLoadPatternType LoadType { get; set; } = eLoadPatternType.Dead;

    /// <summary>
    /// Gets or sets the self-weight multiplier for this load pattern.
    /// Used to automatically apply self-weight of elements.
    /// </summary>
    public double SelfWeightMultiplier { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the auto seismic code name (if applicable).
    /// Empty string if not an auto seismic pattern.
    /// </summary>
    public string AutoSeismicCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the auto wind code name (if applicable).
    /// Empty string if not an auto wind pattern.
    /// </summary>
    public string AutoWindCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets whether this is an auto seismic pattern.
    /// </summary>
    public bool IsAutoSeismic => !string.IsNullOrEmpty(AutoSeismicCode);

    /// <summary>
    /// Gets whether this is an auto wind pattern.
    /// </summary>
    public bool IsAutoWind => !string.IsNullOrEmpty(AutoWindCode);

    /// <summary>
    /// Gets whether this pattern includes self-weight.
    /// </summary>
    public bool IncludesSelfWeight => Math.Abs(SelfWeightMultiplier) > 1e-10;

    /// <summary>
    /// Gets the load type as a string.
    /// </summary>
    public string LoadTypeString => LoadType switch
    {
        eLoadPatternType.Dead => "Dead",
        eLoadPatternType.SuperDead => "Super Dead",
        eLoadPatternType.Live => "Live",
        eLoadPatternType.ReduceLive => "Reducible Live",
        eLoadPatternType.Quake => "Seismic",
        eLoadPatternType.Wind => "Wind",
        eLoadPatternType.Snow => "Snow",
        eLoadPatternType.Other => "Other",
        eLoadPatternType.Temperature => "Temperature",
        eLoadPatternType.Rooflive => "Roof Live",
        eLoadPatternType.Notional => "Notional",
        eLoadPatternType.PatternLive => "Pattern Live",
        eLoadPatternType.Prestress => "Prestress",
        eLoadPatternType.Construction => "Construction",
        eLoadPatternType.PatternAuto => "Pattern Auto",
        eLoadPatternType.QuakeDrift => "Seismic Drift",
        _ => "Unknown"
    };

    /// <summary>
    /// Validates that the load pattern type is valid for ETABS/SAFE.
    /// </summary>
    public bool IsValidForEtabs()
    {
        return LoadType switch
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
    /// Creates a dead load pattern.
    /// </summary>
    public static LoadPattern CreateDeadLoad(string name, double selfWeightMultiplier = 1.0)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Dead,
            SelfWeightMultiplier = selfWeightMultiplier
        };
    }

    /// <summary>
    /// Creates a super dead load pattern (SDL - additional dead load).
    /// </summary>
    public static LoadPattern CreateSuperDeadLoad(string name, double selfWeightMultiplier = 0.0)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.SuperDead,
            SelfWeightMultiplier = selfWeightMultiplier
        };
    }

    /// <summary>
    /// Creates a live load pattern.
    /// </summary>
    public static LoadPattern CreateLiveLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Live,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a reducible live load pattern.
    /// </summary>
    public static LoadPattern CreateReducibleLiveLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.ReduceLive,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a roof live load pattern.
    /// </summary>
    public static LoadPattern CreateRoofLiveLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Rooflive,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a seismic load pattern.
    /// </summary>
    public static LoadPattern CreateSeismicLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Quake,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a wind load pattern.
    /// </summary>
    public static LoadPattern CreateWindLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Wind,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a snow load pattern.
    /// </summary>
    public static LoadPattern CreateSnowLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Snow,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a temperature load pattern.
    /// </summary>
    public static LoadPattern CreateTemperatureLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Temperature,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a notional load pattern (for stability analysis).
    /// </summary>
    public static LoadPattern CreateNotionalLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Notional,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a pattern live load.
    /// </summary>
    public static LoadPattern CreatePatternLiveLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.PatternLive,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a prestress load pattern.
    /// </summary>
    public static LoadPattern CreatePrestressLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Prestress,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates a construction load pattern.
    /// </summary>
    public static LoadPattern CreateConstructionLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Construction,
            SelfWeightMultiplier = 0.0
        };
    }

    /// <summary>
    /// Creates an other/generic load pattern.
    /// </summary>
    public static LoadPattern CreateOtherLoad(string name)
    {
        return new LoadPattern
        {
            Name = name,
            LoadType = eLoadPatternType.Other,
            SelfWeightMultiplier = 0.0
        };
    }

    public override string ToString()
    {
        string swInfo = IncludesSelfWeight ? $", SW={SelfWeightMultiplier:F2}" : "";
        string autoInfo = IsAutoSeismic ? $", Auto Seismic: {AutoSeismicCode}" :
                         IsAutoWind ? $", Auto Wind: {AutoWindCode}" : "";
        return $"LoadPattern: {Name} [{LoadTypeString}]{swInfo}{autoInfo}";
    }
}