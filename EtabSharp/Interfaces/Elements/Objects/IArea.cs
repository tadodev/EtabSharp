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
    /// <param name="coordinates">Array of coordinates defining the area boundary</param>
    /// <param name="propertyName">Name of the area property to assign</param>
    /// <param name="userName">Optional custom name for the area</param>
    /// <param name="csys">Coordinate system name</param>
    /// <returns>The name of the created area object</returns>
    string AddAreaByCoordinates(AreaCoordinate[] coordinates, string propertyName = "Default", string userName = "", string csys = "Global");
    
    /// <summary>
    /// Adds an area object by specifying point names.
    /// </summary>
    /// <param name="pointNames">Array of point names defining the area boundary</param>
    /// <param name="propertyName">Name of the area property to assign</param>
    /// <param name="userName">Optional custom name for the area</param>
    /// <returns>The name of the created area object</returns>
    string AddAreaByPoints(string[] pointNames, string propertyName = "Default", string userName = "");
    
    /// <summary>
    /// Changes the name of an existing area object.
    /// </summary>
    /// <param name="currentName">Current name of the area</param>
    /// <param name="newName">New name for the area</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ChangeName(string currentName, string newName);
    
    /// <summary>
    /// Gets an area object with its properties and connectivity.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Area model with properties and connectivity</returns>
    Area GetArea(string areaName);
    
    /// <summary>
    /// Gets all area objects with their properties.
    /// </summary>
    /// <returns>List of Area models</returns>
    List<Area> GetAllAreas();
    
    /// <summary>
    /// Retrieves the names of all defined area objects.
    /// </summary>
    /// <returns>Array of area object names</returns>
    string[] GetNameList();
    
    /// <summary>
    /// Gets the count of area objects in the model.
    /// </summary>
    /// <returns>Total number of areas</returns>
    int Count();
    
    /// <summary>
    /// Deletes specified area objects from the model.
    /// </summary>
    /// <param name="areaName">Name of the area to delete</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Delete(string areaName, eItemType itemType = eItemType.Objects);
    
    #endregion

    #region Properties
    
    /// <summary>
    /// Sets the area property for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="propertyName">Name of the area property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetProperty(string areaName, string propertyName, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets the area property assigned to an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned area property</returns>
    string GetProperty(string areaName);
    
    /// <summary>
    /// Sets material property override for an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="materialName">Name of the material property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMaterialOverwrite(string areaName, string materialName, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets material property override for an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the material override</returns>
    string GetMaterialOverwrite(string areaName);
    
    /// <summary>
    /// Sets local axes for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="angleInDegrees">Angle in degrees for local axis rotation</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLocalAxes(string areaName, double angleInDegrees, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets local axes for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Tuple of (Angle, IsAdvanced)</returns>
    (double Angle, bool IsAdvanced) GetLocalAxes(string areaName);
    
    /// <summary>
    /// Sets property modifiers for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="modifiers">AreaModifiers model with modifier values</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetModifiers(string areaName, AreaModifiers modifiers, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets property modifiers for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>AreaModifiers model with modifier values</returns>
    AreaModifiers GetModifiers(string areaName);
    
    /// <summary>
    /// Deletes property modifiers for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteModifiers(string areaName, eItemType itemType = eItemType.Objects);
    
    #endregion

    #region Loads
    
    /// <summary>
    /// Sets uniform load on an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="load">AreaUniformLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <param name="itemType">Item type for assignment (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadUniform(string areaName, AreaUniformLoad load, bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets uniform load on an area object using individual parameters.
    /// </summary>
    /// <param name="name">Name of an existing area object or group</param>
    /// <param name="loadPattern">Name of a defined load pattern</param>
    /// <param name="value">Uniform load value [F/L2]</param>
    /// <param name="direction">Load direction (1-11): 1-3=Local 1-3, 4-6=Global X-Z, 7-9=Projected X-Z, 10=Gravity, 11=Projected Gravity</param>
    /// <param name="replace">If true, replaces all previous uniform loads in the specified load pattern</param>
    /// <param name="coordinateSystem">Coordinate system name ("Local" or coordinate system name)</param>
    /// <param name="itemType">Item type for assignment (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadUniform(string name, string loadPattern, double value, int direction, 
        bool replace = true, string coordinateSystem = "Global", eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets uniform loads assigned to an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>List of AreaUniformLoad models</returns>
    List<AreaUniformLoad> GetLoadUniform(string areaName, string loadPattern = "");
    
    /// <summary>
    /// Deletes uniform loads from an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadUniform(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Sets uniform load transferred to frame objects.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="load">AreaUniformToFrameLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadUniformToFrame(string areaName, AreaUniformToFrameLoad load, bool replace = true);
    
    /// <summary>
    /// Gets uniform loads transferred to frame objects.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>List of AreaUniformToFrameLoad models</returns>
    List<AreaUniformToFrameLoad> GetLoadUniformToFrame(string areaName, string loadPattern = "");
    
    /// <summary>
    /// Deletes uniform loads transferred to frame objects.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadUniformToFrame(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Sets wind pressure load on an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="load">AreaWindPressureLoad model with load parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadWindPressure(string areaName, AreaWindPressureLoad load);
    
    /// <summary>
    /// Gets wind pressure loads assigned to an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>List of AreaWindPressureLoad models</returns>
    List<AreaWindPressureLoad> GetLoadWindPressure(string areaName, string loadPattern = "");
    
    /// <summary>
    /// Deletes wind pressure loads from an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadWindPressure(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Sets temperature load on an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="load">AreaTemperatureLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadTemperature(string areaName, AreaTemperatureLoad load, bool replace = true);
    
    /// <summary>
    /// Gets temperature loads assigned to area objects.
    /// </summary>
    /// <param name="areaName">Name of the area object (empty for all areas)</param>
    /// <param name="itemType">Item type for retrieval</param>
    /// <returns>List of AreaTemperatureLoad models</returns>
    List<AreaTemperatureLoad> GetLoadTemperature(string areaName = "", eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Deletes temperature loads from an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadTemperature(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);
    
    #endregion

    #region Mass and Springs
    
    /// <summary>
    /// Sets mass per unit area for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="massPerArea">Mass per unit area</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMass(string areaName, double massPerArea, bool replace = false, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets mass per unit area for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Mass per unit area</returns>
    double GetMass(string areaName);
    
    /// <summary>
    /// Deletes mass assignment from an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteMass(string areaName, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Sets spring assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="springPropertyName">Name of the spring property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpringAssignment(string areaName, string springPropertyName, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets spring assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned spring property</returns>
    string GetSpringAssignment(string areaName);
    
    /// <summary>
    /// Deletes spring assignment from an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteSpring(string areaName, eItemType itemType = eItemType.Objects);
    
    #endregion

    #region Design and Analysis
    
    /// <summary>
    /// Gets the design orientation for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Design orientation enumeration</returns>
    eAreaDesignOrientation GetDesignOrientation(string areaName);
    
    /// <summary>
    /// Sets edge constraint for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="constraintExists">True if constraint exists</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetEdgeConstraint(string areaName, bool constraintExists, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets edge constraint for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if constraint exists</returns>
    bool GetEdgeConstraint(string areaName);
    
    /// <summary>
    /// Sets opening status for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="isOpening">True if area is an opening</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOpening(string areaName, bool isOpening, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets opening status for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if area is an opening</returns>
    bool GetOpening(string areaName);
    
    /// <summary>
    /// Sets pier assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="pierName">Name of the pier</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetPier(string areaName, string pierName, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets pier assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned pier</returns>
    string GetPier(string areaName);
    
    /// <summary>
    /// Sets spandrel assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="spandrelName">Name of the spandrel</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpandrel(string areaName, string spandrelName, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets spandrel assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned spandrel</returns>
    string GetSpandrel(string areaName);
    
    /// <summary>
    /// Sets diaphragm assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="diaphragmName">Name of the diaphragm</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDiaphragm(string areaName, string diaphragmName);
    
    /// <summary>
    /// Gets diaphragm assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned diaphragm</returns>
    string GetDiaphragm(string areaName);
    
    #endregion

    #region Selection and Groups
    
    /// <summary>
    /// Sets the selection state of an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <param name="itemType">Item type for selection</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSelected(string areaName, bool selected, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets the selection state of an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if selected, false otherwise</returns>
    bool IsSelected(string areaName);
    
    /// <summary>
    /// Sets edge selection for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="edgeNumber">Edge number to select</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSelectedEdge(string areaName, int edgeNumber, bool selected);
    
    /// <summary>
    /// Gets edge selection for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of edge selection states</returns>
    bool[] GetSelectedEdges(string areaName);
    
    /// <summary>
    /// Assigns an area object to a group.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroupAssignment(string areaName, string groupName, bool remove = false, eItemType itemType = eItemType.Objects);
    
    /// <summary>
    /// Gets group assignments for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of group names</returns>
    string[] GetGroupAssignment(string areaName);
    
    #endregion

    #region Label and Story Methods
    
    /// <summary>
    /// Gets areas by story level.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Array of area names on the specified story</returns>
    string[] GetAreasOnStory(string storyName);
    
    /// <summary>
    /// Gets label and story information for all areas.
    /// </summary>
    /// <returns>Tuple of (Names, Labels, Stories)</returns>
    (string[] Names, string[] Labels, string[] Stories) GetLabelNameList();
    
    /// <summary>
    /// Gets label and story from area name.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Tuple of (Label, Story)</returns>
    (string Label, string Story) GetLabelFromName(string areaName);
    
    /// <summary>
    /// Gets area name from label and story.
    /// </summary>
    /// <param name="label">Area label</param>
    /// <param name="story">Story name</param>
    /// <returns>Area name</returns>
    string GetNameFromLabel(string label, string story);
    
    #endregion

    #region Advanced Methods
    
    /// <summary>
    /// Gets the GUID for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>GUID string</returns>
    string GetGUID(string areaName);
    
    /// <summary>
    /// Sets the GUID for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="guid">GUID string (empty to auto-generate)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGUID(string areaName, string guid = "");
    
    /// <summary>
    /// Gets the transformation matrix for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="isGlobal">True for global coordinates, false for local</param>
    /// <returns>Transformation matrix values</returns>
    double[] GetTransformationMatrix(string areaName, bool isGlobal = true);
    
    /// <summary>
    /// Gets element information for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of element names</returns>
    string[] GetElements(string areaName);
    
    /// <summary>
    /// Gets point connectivity for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of point names defining the area boundary</returns>
    string[] GetPoints(string areaName);
    
    /// <summary>
    /// Gets offsets for area object points.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>AreaOffsets model with offset information</returns>
    AreaOffsets GetOffsets(string areaName);
    
    /// <summary>
    /// Gets curved edge information for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>AreaCurvedEdges model with curved edge information</returns>
    AreaCurvedEdges GetCurvedEdges(string areaName);
    
    #endregion
}