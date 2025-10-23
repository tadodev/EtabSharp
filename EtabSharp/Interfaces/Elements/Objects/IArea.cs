using EtabSharp.Elements.AreaObj.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Elements.Objects;

/// <summary>
/// Interface for area object operations in ETABS.
/// Provides type-safe methods for creating, modifying, and querying area objects.
/// </summary>
public interface IArea
{
    #region Area Creation & Geometry

    /// <summary>
    /// Adds an area object by specifying coordinates.
    /// </summary>
    string AddAreaByCoordinates(AreaCoordinate[] coordinates, string propertyName = "Default", string userName = "", string csys = "Global");

    /// <summary>
    /// Adds an area object by specifying point names.
    /// </summary>
    string AddAreaByPoints(string[] pointNames, string propertyName = "Default", string userName = "");

    /// <summary>
    /// Changes the name of an existing area object.
    /// </summary>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Gets an area object with its properties and connectivity.
    /// </summary>
    Area GetArea(string areaName);

    /// <summary>
    /// Gets all area objects with their properties.
    /// </summary>
    List<Area> GetAllAreas();

    /// <summary>
    /// Retrieves the names of all defined area objects.
    /// </summary>
    string[] GetNameList();

    /// <summary>
    /// Gets the count of area objects in the model.
    /// </summary>
    int Count();

    /// <summary>
    /// Deletes specified area objects from the model.
    /// </summary>
    int Delete(string areaName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Properties

    /// <summary>
    /// Sets the area property for an area object.
    /// </summary>
    int SetProperty(string areaName, string propertyName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the area property assigned to an area object.
    /// </summary>
    string GetProperty(string areaName);

    /// <summary>
    /// Sets material property override for an area.
    /// </summary>
    int SetMaterialOverwrite(string areaName, string materialName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets material property override for an area.
    /// </summary>
    string GetMaterialOverwrite(string areaName);

    /// <summary>
    /// Sets local axes for an area object.
    /// </summary>
    int SetLocalAxes(string areaName, double angleInDegrees, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets local axes for an area object.
    /// </summary>
    (double Angle, bool IsAdvanced) GetLocalAxes(string areaName);

    /// <summary>
    /// Sets property modifiers for an area object.
    /// </summary>
    int SetModifiers(string areaName, AreaModifiers modifiers, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets property modifiers for an area object.
    /// </summary>
    AreaModifiers GetModifiers(string areaName);

    /// <summary>
    /// Deletes property modifiers for an area object.
    /// </summary>
    int DeleteModifiers(string areaName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Loads

    /// <summary>
    /// Sets uniform load on an area object.
    /// </summary>
    int SetLoadUniform(string areaName, AreaUniformLoad load, bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets uniform load on an area object using individual parameters.
    /// </summary>
    int SetLoadUniform(string name, string loadPattern, double value, int direction,
        bool replace = true, string coordinateSystem = "Global", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets uniform loads assigned to an area object.
    /// </summary>
    List<AreaUniformLoad> GetLoadUniform(string areaName, string loadPattern = "");

    /// <summary>
    /// Deletes uniform loads from an area object.
    /// </summary>
    int DeleteLoadUniform(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets uniform load transferred to frame objects.
    /// </summary>
    int SetLoadUniformToFrame(string areaName, AreaUniformToFrameLoad load, bool replace = true);

    /// <summary>
    /// Gets uniform loads transferred to frame objects.
    /// </summary>
    List<AreaUniformToFrameLoad> GetLoadUniformToFrame(string areaName, string loadPattern = "");

    /// <summary>
    /// Deletes uniform loads transferred to frame objects.
    /// </summary>
    int DeleteLoadUniformToFrame(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets wind pressure load on an area object.
    /// </summary>
    int SetLoadWindPressure(string areaName, AreaWindPressureLoad load);

    /// <summary>
    /// Gets wind pressure loads assigned to an area object.
    /// </summary>
    List<AreaWindPressureLoad> GetLoadWindPressure(string areaName, string loadPattern = "");

    /// <summary>
    /// Deletes wind pressure loads from an area object.
    /// </summary>
    int DeleteLoadWindPressure(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets temperature load on an area object.
    /// </summary>
    int SetLoadTemperature(string areaName, AreaTemperatureLoad load, bool replace = true);

    /// <summary>
    /// Gets temperature loads assigned to area objects.
    /// </summary>
    List<AreaTemperatureLoad> GetLoadTemperature(string areaName = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Deletes temperature loads from an area object.
    /// </summary>
    int DeleteLoadTemperature(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);

    #endregion

    #region Mass and Springs

    /// <summary>
    /// Sets mass per unit area for an area object.
    /// </summary>
    int SetMass(string areaName, double massPerArea, bool replace = false, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets mass per unit area for an area object.
    /// </summary>
    double GetMass(string areaName);

    /// <summary>
    /// Deletes mass assignment from an area object.
    /// </summary>
    int DeleteMass(string areaName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets spring assignment for an area object.
    /// </summary>
    int SetSpringAssignment(string areaName, string springPropertyName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets spring assignment for an area object.
    /// </summary>
    string GetSpringAssignment(string areaName);

    /// <summary>
    /// Deletes spring assignment from an area object.
    /// </summary>
    int DeleteSpring(string areaName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Design and Analysis

    /// <summary>
    /// Gets the design orientation for an area object.
    /// </summary>
    eAreaDesignOrientation GetDesignOrientation(string areaName);

    /// <summary>
    /// Sets edge constraint for an area object.
    /// </summary>
    int SetEdgeConstraint(string areaName, bool constraintExists, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets edge constraint for an area object.
    /// </summary>
    bool GetEdgeConstraint(string areaName);

    /// <summary>
    /// Sets opening status for an area object.
    /// </summary>
    int SetOpening(string areaName, bool isOpening, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets opening status for an area object.
    /// </summary>
    bool GetOpening(string areaName);

    /// <summary>
    /// Sets pier assignment for an area object.
    /// </summary>
    int SetPier(string areaName, string pierName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets pier assignment for an area object.
    /// </summary>
    string GetPier(string areaName);

    /// <summary>
    /// Sets spandrel assignment for an area object.
    /// </summary>
    int SetSpandrel(string areaName, string spandrelName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets spandrel assignment for an area object.
    /// </summary>
    string GetSpandrel(string areaName);

    /// <summary>
    /// Sets diaphragm assignment for an area object.
    /// </summary>
    int SetDiaphragm(string areaName, string diaphragmName);

    /// <summary>
    /// Gets diaphragm assignment for an area object.
    /// </summary>
    string GetDiaphragm(string areaName);

    #endregion

    #region Selection and Groups

    /// <summary>
    /// Sets the selection state of an area object.
    /// </summary>
    int SetSelected(string areaName, bool selected, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the selection state of an area object.
    /// </summary>
    bool IsSelected(string areaName);

    /// <summary>
    /// Sets edge selection for an area object.
    /// </summary>
    int SetSelectedEdge(string areaName, int edgeNumber, bool selected);

    /// <summary>
    /// Gets edge selection for an area object.
    /// </summary>
    bool[] GetSelectedEdges(string areaName);

    /// <summary>
    /// Assigns an area object to a group.
    /// </summary>
    int SetGroupAssignment(string areaName, string groupName, bool remove = false, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets group assignments for an area object.
    /// </summary>
    string[] GetGroupAssignment(string areaName);

    #endregion

    #region Label and Story Methods

    /// <summary>
    /// Gets areas by story level.
    /// </summary>
    string[] GetAreasOnStory(string storyName);

    /// <summary>
    /// Gets label and story information for all areas.
    /// </summary>
    (string[] Names, string[] Labels, string[] Stories) GetLabelNameList();

    /// <summary>
    /// Gets label and story from area name.
    /// </summary>
    (string Label, string Story) GetLabelFromName(string areaName);

    /// <summary>
    /// Gets area name from label and story.
    /// </summary>
    string GetNameFromLabel(string label, string story);

    #endregion

    #region Advanced Methods

    /// <summary>
    /// Gets the GUID for an area object.
    /// </summary>
    string GetGUID(string areaName);

    /// <summary>
    /// Sets the GUID for an area object.
    /// </summary>
    int SetGUID(string areaName, string guid = "");

    /// <summary>
    /// Gets the transformation matrix for an area object.
    /// </summary>
    double[] GetTransformationMatrix(string areaName, bool isGlobal = true);

    /// <summary>
    /// Gets element information for an area object.
    /// </summary>
    string[] GetElements(string areaName);

    /// <summary>
    /// Gets point connectivity for an area object.
    /// </summary>
    string[] GetPoints(string areaName);

    /// <summary>
    /// Gets offsets for area object points.
    /// </summary>
    AreaOffsets GetOffsets(string areaName);

    /// <summary>
    /// Gets curved edge information for an area object.
    /// </summary>
    AreaCurvedEdges GetCurvedEdges(string areaName);

    #endregion

    #region Convenience Methods - MISSING FROM ORIGINAL INTERFACE

    /// <summary>
    /// Sets an area as a wall pier.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="pierName">Name of the pier</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAsWallPier(string areaName, string pierName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets an area as a wall spandrel.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="spandrelName">Name of the spandrel</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAsWallSpandrel(string areaName, string spandrelName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets an area as a floor diaphragm.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="diaphragmName">Name of the diaphragm</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAsFloorDiaphragm(string areaName, string diaphragmName);

    /// <summary>
    /// Sets an area as an opening (void).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAsOpening(string areaName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets an area as a solid element (not an opening).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetAsSolid(string areaName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Checks if an area has any design assignments.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if design assignments exist</returns>
    bool HasDesignAssignments(string areaName);

    /// <summary>
    /// Gets a summary of all design assignments for an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>String summary of design assignments</returns>
    string GetDesignAssignmentSummary(string areaName);

    /// <summary>
    /// Applies a uniform gravity load (downward) to an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadValue">Load value (positive for downward)</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGravityLoad(string areaName, string loadPattern, double loadValue, bool replace = true);

    /// <summary>
    /// Applies a uniform pressure load (normal to surface) to an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="pressure">Pressure value (positive for outward normal)</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetPressureLoad(string areaName, string loadPattern, double pressure, bool replace = true);

    /// <summary>
    /// Applies a uniform temperature change to an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="temperatureChange">Temperature change value</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetUniformTemperatureLoad(string areaName, string loadPattern, double temperatureChange, bool replace = true);

    /// <summary>
    /// Gets all loads (uniform, wind, temperature) for an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>AreaLoads model with all load types</returns>
    AreaLoads GetAllLoads(string areaName, string loadPattern = "");

    /// <summary>
    /// Selects all areas in a group.
    /// </summary>
    /// <param name="groupName">Name of the group</param>
    /// <returns>Number of areas selected</returns>
    int SelectAreasInGroup(string groupName);

    /// <summary>
    /// Selects all areas on a story.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Number of areas selected</returns>
    int SelectAreasOnStory(string storyName);

    /// <summary>
    /// Deselects all areas in the model.
    /// </summary>
    /// <returns>Number of areas deselected</returns>
    int DeselectAllAreas();

    /// <summary>
    /// Gets all currently selected areas.
    /// </summary>
    /// <returns>Array of selected area names</returns>
    string[] GetSelectedAreas();

    /// <summary>
    /// Assigns multiple areas to a group.
    /// </summary>
    /// <param name="areaNames">Array of area names</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <returns>Number of areas successfully assigned/removed</returns>
    int SetGroupAssignmentMultiple(string[] areaNames, string groupName, bool remove = false);

    /// <summary>
    /// Gets a complete area object with all properties populated.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Complete Area model with all properties</returns>
    Area GetCompleteArea(string areaName);

    #endregion
}