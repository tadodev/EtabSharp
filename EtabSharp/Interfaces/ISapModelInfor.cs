using EtabSharp.Models.SapModelInfor;
using ETABSv1;

namespace EtabSharp.Interfaces;

/// <summary>
/// SapModel methods like getting and setting unit system,information, grid, initialization, etc.
/// </summary>
public interface ISapModelInfor
{
    #region File Operations

    /// <summary>
    /// Gets the filename of the current model
    /// </summary>
    /// <param name="includePath">If true, returns full path; if false, only filename</param>
    string GetModelFilename(bool includePath = false);

    /// <summary>
    /// Gets the full filepath of the current model
    /// </summary>
    string GetModelFilepath();

    #endregion

    #region Model State

    /// <summary>
    /// Checks if the model is locked (read-only)
    /// </summary>
    bool IsLocked();

    /// <summary>
    /// Locks or unlocks the model
    /// </summary>
    /// <param name="isLocked">True to lock, false to unlock</param>
    void SetLocked(bool isLocked);

    #endregion

    #region Model Information

    /// <summary>
    /// Gets the ETABS program version (e.g., "22.7.0")
    /// </summary>
    string GetVersion();

    /// <summary>
    /// Gets program information (name, version, build, etc.)
    /// </summary>
    ProgramInfo GetProgramInfo();


    #endregion

    #region Initialization

    /// <summary>
    /// Initializes a new blank model (clears existing model)
    /// WARNING: This clears all existing data. Save first if needed.
    /// </summary>
    /// <param name="units"></param>
    int InitializeNewModel(eUnits units);


    #endregion

    #region Coordinate System

    /// <summary>
    /// Gets the name of the current active coordinate system
    /// </summary>
    string GetPresentCoordSystem();

    #endregion
}