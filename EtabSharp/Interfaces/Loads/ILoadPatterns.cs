using EtabSharp.Loads.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Loads;

/// <summary>
/// Interface for managing load patterns in the ETABS model.
/// Load patterns define the spatial distribution and nature of loads applied to the structure.
/// </summary>
public interface ILoadPatterns
{
    #region Load Pattern Creation and Management

    /// <summary>
    /// Adds a new load pattern to the model.
    /// Wraps cSapModel.LoadPatterns.Add.
    /// </summary>
    /// <param name="name">Unique name for the load pattern</param>
    /// <param name="loadType">Type of load pattern</param>
    /// <param name="selfWeightMultiplier">Multiplier for self-weight (typically 1.0 for dead load, 0.0 for others)</param>
    /// <param name="addAnalysisCase">If true, automatically creates an analysis case for this pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Add(string name, eLoadPatternType loadType, double selfWeightMultiplier = 0.0, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a new load pattern using a LoadPattern model.
    /// </summary>
    /// <param name="loadPattern">LoadPattern model with properties</param>
    /// <param name="addAnalysisCase">If true, automatically creates an analysis case for this pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Add(LoadPattern loadPattern, bool addAnalysisCase = true);

    /// <summary>
    /// Changes the name of an existing load pattern.
    /// Wraps cSapModel.LoadPatterns.ChangeName.
    /// </summary>
    /// <param name="currentName">Current name of the load pattern</param>
    /// <param name="newName">New name for the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Deletes a load pattern from the model.
    /// Wraps cSapModel.LoadPatterns.Delete.
    /// </summary>
    /// <param name="name">Name of the load pattern to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Delete(string name);

    /// <summary>
    /// Gets the count of load patterns in the model.
    /// Wraps cSapModel.LoadPatterns.Count.
    /// </summary>
    /// <returns>Total number of load patterns</returns>
    int Count();

    /// <summary>
    /// Gets the names of all load patterns in the model.
    /// Wraps cSapModel.LoadPatterns.GetNameList.
    /// </summary>
    /// <returns>Array of load pattern names</returns>
    string[] GetNameList();

    #endregion

    #region Load Pattern Properties

    /// <summary>
    /// Gets the load type of a load pattern.
    /// Wraps cSapModel.LoadPatterns.GetLoadType.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <returns>Load pattern type</returns>
    eLoadPatternType GetLoadType(string name);

    /// <summary>
    /// Sets the load type of a load pattern.
    /// Wraps cSapModel.LoadPatterns.SetLoadType.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="loadType">New load pattern type</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadType(string name, eLoadPatternType loadType);

    /// <summary>
    /// Gets the self-weight multiplier for a load pattern.
    /// Wraps cSapModel.LoadPatterns.GetSelfWTMultiplier.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <returns>Self-weight multiplier value</returns>
    double GetSelfWeightMultiplier(string name);

    /// <summary>
    /// Sets the self-weight multiplier for a load pattern.
    /// Wraps cSapModel.LoadPatterns.SetSelfWTMultiplier.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="multiplier">Self-weight multiplier value</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSelfWeightMultiplier(string name, double multiplier);

    /// <summary>
    /// Gets the auto seismic code name for a load pattern.
    /// Wraps cSapModel.LoadPatterns.GetAutoSeismicCode.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <returns>Auto seismic code name (empty if not an auto seismic pattern)</returns>
    string GetAutoSeismicCode(string name);

    /// <summary>
    /// Gets the auto wind code name for a load pattern.
    /// Wraps cSapModel.LoadPatterns.GetAutoWindCode.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <returns>Auto wind code name (empty if not an auto wind pattern)</returns>
    string GetAutoWindCode(string name);

    /// <summary>
    /// Gets a complete load pattern with all properties.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <returns>LoadPattern model with all properties</returns>
    LoadPattern GetLoadPattern(string name);

    /// <summary>
    /// Gets all load patterns in the model with their properties.
    /// </summary>
    /// <returns>List of all LoadPattern objects</returns>
    List<LoadPattern> GetAllLoadPatterns();

    #endregion

    #region Auto Seismic and Auto Wind

    /// <summary>
    /// Gets the auto seismic manager for defining seismic loads per code.
    /// Wraps cSapModel.LoadPatterns.AutoSeismic.
    /// </summary>
    cAutoSeismic AutoSeismic { get; }

    /// <summary>
    /// Gets the auto wind manager for defining wind loads per code.
    /// Wraps cSapModel.LoadPatterns.AutoWind.
    /// </summary>
    cAutoWind AutoWind { get; }

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Checks if a load pattern exists in the model.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <returns>True if the pattern exists, false otherwise</returns>
    bool LoadPatternExists(string name);

    /// <summary>
    /// Adds a dead load pattern with self-weight.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="selfWeightMultiplier">Self-weight multiplier (default: 1.0)</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddDeadLoad(string name, double selfWeightMultiplier = 1.0, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a super dead load pattern.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddSuperDeadLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a live load pattern.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddLiveLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a reducible live load pattern.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddReducibleLiveLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a roof live load pattern.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddRoofLiveLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a seismic load pattern.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddSeismicLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a wind load pattern.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddWindLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a snow load pattern.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddSnowLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a temperature load pattern.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddTemperatureLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Adds a notional load pattern for stability analysis.
    /// </summary>
    /// <param name="name">Name of the load pattern</param>
    /// <param name="addAnalysisCase">Create associated analysis case</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int AddNotionalLoad(string name, bool addAnalysisCase = true);

    /// <summary>
    /// Gets load patterns by type.
    /// </summary>
    /// <param name="loadType">Type of load pattern to retrieve</param>
    /// <returns>List of load pattern names of the specified type</returns>
    List<string> GetLoadPatternsByType(eLoadPatternType loadType);

    /// <summary>
    /// Gets all gravity load patterns (dead, live, snow, etc.).
    /// </summary>
    /// <returns>List of gravity load pattern names</returns>
    List<string> GetGravityLoadPatterns();

    /// <summary>
    /// Gets all lateral load patterns (seismic, wind, notional, etc.).
    /// </summary>
    /// <returns>List of lateral load pattern names</returns>
    List<string> GetLateralLoadPatterns();

    /// <summary>
    /// Gets load patterns that include self-weight.
    /// </summary>
    /// <returns>List of load pattern names that include self-weight</returns>
    List<string> GetLoadPatternsWithSelfWeight();

    /// <summary>
    /// Gets a summary of all load patterns in the model.
    /// </summary>
    /// <returns>LoadPatternSummary with statistics</returns>
    LoadPatternSummary GetLoadPatternSummary();

    /// <summary>
    /// Deletes all load patterns of a specific type.
    /// </summary>
    /// <param name="loadType">Type of load patterns to delete</param>
    /// <returns>Number of patterns deleted</returns>
    int DeleteLoadPatternsByType(eLoadPatternType loadType);

    /// <summary>
    /// Creates a standard set of load patterns for building design.
    /// Includes: DEAD, SDL, LIVE, ROOF LIVE, WIND, SEISMIC
    /// </summary>
    /// <param name="includeSeismic">Include seismic load pattern</param>
    /// <param name="includeWind">Include wind load pattern</param>
    /// <param name="includeSnow">Include snow load pattern</param>
    /// <returns>Number of patterns created</returns>
    int CreateStandardLoadPatterns(bool includeSeismic = true, bool includeWind = true, bool includeSnow = false);

    #endregion
}