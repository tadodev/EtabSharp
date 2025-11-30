using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Loads;
using EtabSharp.Loads.LoadPatterns.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Loads.LoadPatterns;

/// <summary>
/// Manages load patterns in the ETABS model.
/// Implements the ILoadPatterns interface by wrapping cSapModel.LoadPatterns operations.
/// </summary>
public class LoadPatternsManager : ILoadPatterns
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the LoadPatternsManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public LoadPatternsManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Load Pattern Creation and Management

    /// <summary>
    /// Adds a new load pattern to the model.
    /// Wraps cSapModel.LoadPatterns.Add.
    /// </summary>
    public int Add(string name, eLoadPatternType loadType, double selfWeightMultiplier = 0.0, bool addAnalysisCase = true)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            int ret = _sapModel.LoadPatterns.Add(name, loadType, selfWeightMultiplier, addAnalysisCase);

            if (ret != 0)
                throw new EtabsException(ret, "Add", $"Failed to add load pattern '{name}'");

            _logger.LogDebug("Added load pattern {Name} of type {LoadType} with SW multiplier {Multiplier}",
                name, loadType, selfWeightMultiplier);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding load pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Adds a new load pattern using a LoadPattern model.
    /// </summary>
    public int Add(LoadPattern loadPattern, bool addAnalysisCase = true)
    {
        try
        {
            if (loadPattern == null)
                throw new ArgumentNullException(nameof(loadPattern));

            if (!loadPattern.IsValidForEtabs())
                throw new ArgumentException($"Load pattern type '{loadPattern.LoadType}' is not valid for ETABS/SAFE", nameof(loadPattern));

            return Add(loadPattern.Name, loadPattern.LoadType, loadPattern.SelfWeightMultiplier, addAnalysisCase);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding load pattern '{loadPattern?.Name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Changes the name of an existing load pattern.
    /// Wraps cSapModel.LoadPatterns.ChangeName.
    /// </summary>
    public int ChangeName(string currentName, string newName)
    {
        try
        {
            if (string.IsNullOrEmpty(currentName))
                throw new ArgumentException("Current name cannot be null or empty", nameof(currentName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New name cannot be null or empty", nameof(newName));

            int ret = _sapModel.LoadPatterns.ChangeName(currentName, newName);

            if (ret != 0)
                throw new EtabsException(ret, "ChangeName", $"Failed to change load pattern name from '{currentName}' to '{newName}'");

            _logger.LogDebug("Changed load pattern name from {OldName} to {NewName}", currentName, newName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error changing load pattern name from '{currentName}' to '{newName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes a load pattern from the model.
    /// Wraps cSapModel.LoadPatterns.Delete.
    /// </summary>
    public int Delete(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            int ret = _sapModel.LoadPatterns.Delete(name);

            if (ret != 0)
                throw new EtabsException(ret, "Delete", $"Failed to delete load pattern '{name}'");

            _logger.LogDebug("Deleted load pattern {Name}", name);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting load pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of load patterns in the model.
    /// Wraps cSapModel.LoadPatterns.Count.
    /// </summary>
    public int Count()
    {
        try
        {
            int count = _sapModel.LoadPatterns.Count();
            _logger.LogDebug("Load pattern count: {Count}", count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting load pattern count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the names of all load patterns in the model.
    /// Wraps cSapModel.LoadPatterns.GetNameList.
    /// </summary>
    public string[] GetNameList()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.LoadPatterns.GetNameList(ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get load pattern name list");

            _logger.LogDebug("Retrieved {Count} load pattern names", numberNames);

            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load pattern name list: {ex.Message}", ex);
        }
    }

    #endregion

    #region Load Pattern Properties

    /// <summary>
    /// Gets the load type of a load pattern.
    /// Wraps cSapModel.LoadPatterns.GetLoadType.
    /// </summary>
    public eLoadPatternType GetLoadType(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            eLoadPatternType loadType = eLoadPatternType.Dead;
            int ret = _sapModel.LoadPatterns.GetLoadType(name, ref loadType);

            if (ret != 0)
                throw new EtabsException(ret, "GetLoadType", $"Failed to get load type for pattern '{name}'");

            return loadType;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load type for pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the load type of a load pattern.
    /// Wraps cSapModel.LoadPatterns.SetLoadType.
    /// </summary>
    public int SetLoadType(string name, eLoadPatternType loadType)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            if (!LoadPatternTypeHelper.IsValidForEtabs(loadType))
                throw new ArgumentException($"Load pattern type '{loadType}' is not valid for ETABS/SAFE", nameof(loadType));

            int ret = _sapModel.LoadPatterns.SetLoadType(name, loadType);

            if (ret != 0)
                throw new EtabsException(ret, "SetLoadType", $"Failed to set load type for pattern '{name}'");

            _logger.LogDebug("Set load type {LoadType} for pattern {Name}", loadType, name);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting load type for pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the self-weight multiplier for a load pattern.
    /// Wraps cSapModel.LoadPatterns.GetSelfWTMultiplier.
    /// </summary>
    public double GetSelfWeightMultiplier(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            double multiplier = 0.0;
            int ret = _sapModel.LoadPatterns.GetSelfWTMultiplier(name, ref multiplier);

            if (ret != 0)
                throw new EtabsException(ret, "GetSelfWTMultiplier", $"Failed to get self-weight multiplier for pattern '{name}'");

            return multiplier;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting self-weight multiplier for pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the self-weight multiplier for a load pattern.
    /// Wraps cSapModel.LoadPatterns.SetSelfWTMultiplier.
    /// </summary>
    public int SetSelfWeightMultiplier(string name, double multiplier)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            int ret = _sapModel.LoadPatterns.SetSelfWTMultiplier(name, multiplier);

            if (ret != 0)
                throw new EtabsException(ret, "SetSelfWTMultiplier", $"Failed to set self-weight multiplier for pattern '{name}'");

            _logger.LogDebug("Set self-weight multiplier {Multiplier} for pattern {Name}", multiplier, name);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting self-weight multiplier for pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the auto seismic code name for a load pattern.
    /// Wraps cSapModel.LoadPatterns.GetAutoSeismicCode.
    /// </summary>
    public string GetAutoSeismicCode(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            string codeName = "";
            int ret = _sapModel.LoadPatterns.GetAutoSeismicCode(name, ref codeName);

            if (ret != 0)
                throw new EtabsException(ret, "GetAutoSeismicCode", $"Failed to get auto seismic code for pattern '{name}'");

            return codeName ?? "";
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting auto seismic code for pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the auto wind code name for a load pattern.
    /// Wraps cSapModel.LoadPatterns.GetAutoWindCode.
    /// </summary>
    public string GetAutoWindCode(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            string codeName = "";
            int ret = _sapModel.LoadPatterns.GetAutoWindCode(name, ref codeName);

            if (ret != 0)
                throw new EtabsException(ret, "GetAutoWindCode", $"Failed to get auto wind code for pattern '{name}'");

            return codeName ?? "";
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting auto wind code for pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a complete load pattern with all properties.
    /// </summary>
    public LoadPattern GetLoadPattern(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Load pattern name cannot be null or empty", nameof(name));

            var pattern = new LoadPattern
            {
                Name = name,
                LoadType = GetLoadType(name),
                SelfWeightMultiplier = GetSelfWeightMultiplier(name),
                AutoSeismicCode = GetAutoSeismicCode(name),
                AutoWindCode = GetAutoWindCode(name)
            };

            _logger.LogDebug("Retrieved load pattern {Name}: {Pattern}", name, pattern.ToString());

            return pattern;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load pattern '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all load patterns in the model with their properties.
    /// </summary>
    public List<LoadPattern> GetAllLoadPatterns()
    {
        try
        {
            var patternNames = GetNameList();
            var patterns = new List<LoadPattern>();

            foreach (var patternName in patternNames)
            {
                try
                {
                    var pattern = GetLoadPattern(patternName);
                    patterns.Add(pattern);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to get load pattern {PatternName}: {Error}", patternName, ex.Message);
                }
            }

            _logger.LogDebug("Retrieved {Count} load patterns", patterns.Count);
            return patterns;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all load patterns: {ex.Message}", ex);
        }
    }

    #endregion

    #region Auto Seismic and Auto Wind

    /// <summary>
    /// Gets the auto seismic manager for defining seismic loads per code.
    /// Wraps cSapModel.LoadPatterns.AutoSeismic.
    /// </summary>
    public cAutoSeismic AutoSeismic => _sapModel.LoadPatterns.AutoSeismic;

    /// <summary>
    /// Gets the auto wind manager for defining wind loads per code.
    /// Wraps cSapModel.LoadPatterns.AutoWind.
    /// </summary>
    public cAutoWind AutoWind => _sapModel.LoadPatterns.AutoWind;

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Checks if a load pattern exists in the model.
    /// </summary>
    public bool LoadPatternExists(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var patternNames = GetNameList();
            return patternNames.Contains(name);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Adds a dead load pattern with self-weight.
    /// </summary>
    public int AddDeadLoad(string name, double selfWeightMultiplier = 1.0, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.Dead, selfWeightMultiplier, addAnalysisCase);
    }

    /// <summary>
    /// Adds a super dead load pattern.
    /// </summary>
    public int AddSuperDeadLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.SuperDead, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Adds a live load pattern.
    /// </summary>
    public int AddLiveLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.Live, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Adds a reducible live load pattern.
    /// </summary>
    public int AddReducibleLiveLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.ReduceLive, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Adds a roof live load pattern.
    /// </summary>
    public int AddRoofLiveLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.Rooflive, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Adds a seismic load pattern.
    /// </summary>
    public int AddSeismicLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.Quake, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Adds a wind load pattern.
    /// </summary>
    public int AddWindLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.Wind, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Adds a snow load pattern.
    /// </summary>
    public int AddSnowLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.Snow, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Adds a temperature load pattern.
    /// </summary>
    public int AddTemperatureLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.Temperature, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Adds a notional load pattern for stability analysis.
    /// </summary>
    public int AddNotionalLoad(string name, bool addAnalysisCase = true)
    {
        return Add(name, eLoadPatternType.Notional, 0.0, addAnalysisCase);
    }

    /// <summary>
    /// Gets load patterns by type.
    /// </summary>
    public List<string> GetLoadPatternsByType(eLoadPatternType loadType)
    {
        try
        {
            var allPatterns = GetAllLoadPatterns();
            return allPatterns
                .Where(p => p.LoadType == loadType)
                .Select(p => p.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load patterns by type: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all gravity load patterns (dead, live, snow, etc.).
    /// </summary>
    public List<string> GetGravityLoadPatterns()
    {
        try
        {
            var allPatterns = GetAllLoadPatterns();
            return allPatterns
                .Where(p => LoadPatternTypeHelper.IsGravityLoad(p.LoadType))
                .Select(p => p.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting gravity load patterns: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all lateral load patterns (seismic, wind, notional, etc.).
    /// </summary>
    public List<string> GetLateralLoadPatterns()
    {
        try
        {
            var allPatterns = GetAllLoadPatterns();
            return allPatterns
                .Where(p => LoadPatternTypeHelper.IsLateralLoad(p.LoadType))
                .Select(p => p.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting lateral load patterns: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets load patterns that include self-weight.
    /// </summary>
    public List<string> GetLoadPatternsWithSelfWeight()
    {
        try
        {
            var allPatterns = GetAllLoadPatterns();
            return allPatterns
                .Where(p => p.IncludesSelfWeight)
                .Select(p => p.Name)
                .ToList();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load patterns with self-weight: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a summary of all load patterns in the model.
    /// </summary>
    public LoadPatternSummary GetLoadPatternSummary()
    {
        try
        {
            var allPatterns = GetAllLoadPatterns();
            var summary = new LoadPatternSummary
            {
                TotalPatterns = allPatterns.Count,
                DeadLoads = allPatterns.Count(p => p.LoadType == eLoadPatternType.Dead),
                LiveLoads = allPatterns.Count(p => p.LoadType == eLoadPatternType.Live ||
                                                   p.LoadType == eLoadPatternType.ReduceLive ||
                                                   p.LoadType == eLoadPatternType.Rooflive),
                SeismicLoads = allPatterns.Count(p => p.LoadType == eLoadPatternType.Quake ||
                                                     p.LoadType == eLoadPatternType.QuakeDrift),
                WindLoads = allPatterns.Count(p => p.LoadType == eLoadPatternType.Wind),
                SnowLoads = allPatterns.Count(p => p.LoadType == eLoadPatternType.Snow),
                TemperatureLoads = allPatterns.Count(p => p.LoadType == eLoadPatternType.Temperature),
                PatternsWithSelfWeight = allPatterns.Count(p => p.IncludesSelfWeight),
                AutoSeismicPatterns = allPatterns.Count(p => p.IsAutoSeismic),
                AutoWindPatterns = allPatterns.Count(p => p.IsAutoWind),
                OtherLoads = allPatterns.Count(p => p.LoadType == eLoadPatternType.Other ||
                                                   p.LoadType == eLoadPatternType.Construction ||
                                                   p.LoadType == eLoadPatternType.Prestress)
            };

            _logger.LogDebug("Load pattern summary: {Summary}", summary.ToString());
            return summary;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting load pattern summary: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes all load patterns of a specific type.
    /// </summary>
    public int DeleteLoadPatternsByType(eLoadPatternType loadType)
    {
        try
        {
            var patternsToDelete = GetLoadPatternsByType(loadType);
            int deletedCount = 0;

            foreach (var patternName in patternsToDelete)
            {
                try
                {
                    int ret = Delete(patternName);
                    if (ret == 0)
                    {
                        deletedCount++;
                        _logger.LogDebug("Deleted load pattern {PatternName} of type {LoadType}", patternName, loadType);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to delete load pattern {PatternName}: {Error}", patternName, ex.Message);
                }
            }

            _logger.LogDebug("Deleted {Count} load patterns of type {LoadType}", deletedCount, loadType);
            return deletedCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting load patterns by type: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Creates a standard set of load patterns for building design.
    /// Includes: DEAD, SDL, LIVE, ROOF LIVE, WIND, SEISMIC
    /// </summary>
    public int CreateStandardLoadPatterns(bool includeSeismic = true, bool includeWind = true, bool includeSnow = false)
    {
        try
        {
            int createdCount = 0;

            // Dead Load with self-weight
            if (!LoadPatternExists("DEAD"))
            {
                if (AddDeadLoad("DEAD", 1.0) == 0)
                    createdCount++;
            }

            // Super Dead Load (SDL)
            if (!LoadPatternExists("SDL"))
            {
                if (AddSuperDeadLoad("SDL") == 0)
                    createdCount++;
            }

            // Live Load
            if (!LoadPatternExists("LIVE"))
            {
                if (AddLiveLoad("LIVE") == 0)
                    createdCount++;
            }

            // Roof Live Load
            if (!LoadPatternExists("ROOF LIVE"))
            {
                if (AddRoofLiveLoad("ROOF LIVE") == 0)
                    createdCount++;
            }

            // Wind Load
            if (includeWind && !LoadPatternExists("WIND"))
            {
                if (AddWindLoad("WIND") == 0)
                    createdCount++;
            }

            // Seismic Load
            if (includeSeismic && !LoadPatternExists("SEISMIC"))
            {
                if (AddSeismicLoad("SEISMIC") == 0)
                    createdCount++;
            }

            // Snow Load
            if (includeSnow && !LoadPatternExists("SNOW"))
            {
                if (AddSnowLoad("SNOW") == 0)
                    createdCount++;
            }

            _logger.LogDebug("Created {Count} standard load patterns", createdCount);
            return createdCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error creating standard load patterns: {ex.Message}", ex);
        }
    }

    #endregion
}