using ETABSv1;

namespace EtabSharp.Interfaces.Elements.Objects;

/// <summary>
/// Provides methods for managing area objects in the ETABS model.
/// Area objects represent slabs, walls, ramps - the 2D structural elements (shells).
/// From a structural engineer's perspective, these are floor systems and shear walls.
/// </summary>
public interface IArea
{
    #region Area Creation & Geometry

    /// <summary>
    /// Adds an area object by specifying corner point names.
    /// This is the standard method for creating floor slabs and walls.
    /// </summary>
    /// <param name="pointNames">Array of point names defining the area perimeter (3+ points)</param>
    /// <param name="sectionName">Name of the area section property (slab/wall, default: "Default")</param>
    /// <param name="userName">Optional custom name (auto-generated if empty)</param>
    /// <returns>The name of the created area object</returns>
    /// <exception cref="ArgumentException">If fewer than 3 points or invalid section</exception>
    string AddArea(string[] pointNames, string sectionName = "Default", string userName = "");

    /// <summary>
    /// Adds an area object by specifying corner point coordinates directly.
    /// Useful for parametric modeling without pre-creating points.
    /// </summary>
    /// <param name="xCoords">Array of X coordinates for each corner</param>
    /// <param name="yCoords">Array of Y coordinates for each corner</param>
    /// <param name="zCoords">Array of Z coordinates for each corner</param>
    /// <param name="sectionName">Name of the area section property (default: "Default")</param>
    /// <param name="userName">Optional custom name</param>
    /// <param name="csys">Coordinate system (default: "Global")</param>
    /// <returns>The name of the created area object</returns>
    string AddAreaByCoordinates(double[] xCoords, double[] yCoords, double[] zCoords,
        string sectionName = "Default", string userName = "", string csys = "Global");

    /// <summary>
    /// Changes the name of an existing area object.
    /// </summary>
    /// <param name="currentName">Current name of the area</param>
    /// <param name="newName">New name for the area</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Gets the names of the corner points that define an area object.
    /// Essential for understanding area geometry and connectivity.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of point names in order around the perimeter</returns>
    string[] GetPoints(string areaName);

    /// <summary>
    /// Retrieves the names of all defined area objects in the model.
    /// </summary>
    /// <returns>Array of area object names</returns>
    string[] GetNameList();

    /// <summary>
    /// Gets all area data efficiently in a single call.
    /// Returns comprehensive information about all areas.
    /// </summary>
    /// <returns>Complex data structure with names, design orientation, boundary points, coordinates</returns>
    object GetAllAreas();

    /// <summary>
    /// Gets curve data for curved edges of an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Curved edge data</returns>
    object GetCurvedEdges(string areaName);

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

    #region Section Properties

    /// <summary>
    /// Assigns an area section property to an area object.
    /// Critical for defining slab thickness, wall thickness, material, etc.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="sectionName">Name of the area section property</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetProperty(string areaName, string sectionName);

    /// <summary>
    /// Gets the section property assigned to an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned area section</returns>
    string GetProperty(string areaName);

    #endregion

    #region Local Axes & Orientation

    /// <summary>
    /// Sets the local axis angle for an area object.
    /// Defines the orientation of local 1 and 2 axes (for reinforcement direction, etc.).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="angleInDegrees">Rotation angle in degrees</param>
    /// <param name="isAdvanced">If true, uses advanced local axis definition</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLocalAxes(string areaName, double angleInDegrees, bool isAdvanced = false);

    /// <summary>
    /// Gets the local axis angle of an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Rotation angle in degrees</returns>
    double GetLocalAxes(string areaName);

    #endregion

    #region Uniform to Frame Loads

    /// <summary>
    /// Assigns uniform load to be distributed to surrounding frame elements.
    /// Used for one-way slabs where load transfers to supporting beams.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="value">Load value (force/area)</param>
    /// <param name="direction">Load direction</param>
    /// <param name="distributionType">Distribution type</param>
    /// <param name="replace">If true, replaces existing; if false, adds</param>
    /// <param name="csys">Coordinate system (default: "Global")</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadUniformToFrame(string areaName, string loadPattern, double value, int direction,
        int distributionType, bool replace = true, string csys = "Global",
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets uniform-to-frame load assignments for area objects.
    /// </summary>
    /// <param name="areaName">Name of the area object (empty for all)</param>
    /// <param name="itemType">Query object, group, or selected objects</param>
    /// <returns>Uniform-to-frame load data</returns>
    object GetLoadUniformToFrame(string areaName = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Deletes uniform-to-frame load assignments from an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadUniformToFrame(string areaName, string loadPattern, eItemType itemType = eItemType.Objects);

    #endregion

    #region Uniform Loads

    /// <summary>
    /// Assigns a uniform load to an area object.
    /// Essential for applying floor dead loads, live loads, and uniform pressures.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="value">Load value (force/area, typically kN/m² or psf)</param>
    /// <param name="direction">Direction: 3=Gravity, 4=Local-1, 5=Local-2, 6=Local-3, etc.</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadUniform(string areaName, string loadPattern, double value,
        int direction = 3, bool replace = true);

    /// <summary>
    /// Gets uniform load assignments for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <returns>Array of uniform load data</returns>
    object[] GetLoadUniform(string areaName, string loadPattern = "");

    /// <summary>
    /// Deletes uniform load assignments from an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadUniform(string areaName, string loadPattern);

    #endregion

    #region Wind Pressure Loads

    /// <summary>
    /// Assigns wind pressure load to an area object.
    /// Used for applying wind loads on walls and exposed surfaces.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="windPressureType">0=Windward, 1=Other</param>
    /// <param name="pressureCoefficient">Wind pressure coefficient (Cp)</param>
    /// <param name="replace">If true, replaces existing; if false, adds</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadWindPressure(string areaName, string loadPattern,
        int windPressureType, double pressureCoefficient, bool replace = true);

    /// <summary>
    /// Gets wind pressure load assignments for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of wind pressure load data</returns>
    object[] GetLoadWindPressure(string areaName);

    /// <summary>
    /// Deletes wind pressure load assignments from an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadWindPressure(string areaName, string loadPattern);

    #endregion

    #region Temperature Loads

    /// <summary>
    /// Assigns temperature load to an area object.
    /// Used for thermal analysis and temperature gradient effects.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="temperatureType">0=Temperature, 1=Temperature gradient</param>
    /// <param name="value">Temperature value or gradient value</param>
    /// <param name="patternName">Joint pattern name (if applicable)</param>
    /// <param name="replace">If true, replaces existing; if false, adds</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadTemperature(string areaName, string loadPattern,
        int temperatureType, double value, string patternName = "", bool replace = true);

    /// <summary>
    /// Gets temperature load assignments for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of temperature load data</returns>
    object[] GetLoadTemperature(string areaName);

    /// <summary>
    /// Deletes temperature load assignments from an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadTemperature(string areaName, string loadPattern);

    #endregion

    #region Diaphragm Assignment

    /// <summary>
    /// Assigns an area to a diaphragm (rigid or semi-rigid floor constraint).
    /// Critical for modeling floor diaphragm behavior in lateral analysis.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="diaphragmName">Name of the diaphragm (empty string to remove assignment)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDiaphragm(string areaName, string diaphragmName);

    /// <summary>
    /// Gets the diaphragm assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of assigned diaphragm (empty if none)</returns>
    string GetDiaphragm(string areaName);

    #endregion

    #region Opening Assignment

    /// <summary>
    /// Designates an area object as an opening (void in slab/wall).
    /// Used to model floor openings for stairs, shafts, or wall openings for doors/windows.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="isOpening">True to make it an opening, false to make it solid</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOpening(string areaName, bool isOpening);

    /// <summary>
    /// Checks if an area object is designated as an opening.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if opening, false if solid area</returns>
    bool IsOpening(string areaName);

    #endregion

    #region Design & Label Assignment

    /// <summary>
    /// Sets the design orientation for an area object.
    /// Defines primary reinforcement direction for slab/wall design.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="orientation">0=Global Z, 1=Local 3, 2=User defined</param>
    /// <param name="vectorX">X component of orientation vector (for user defined)</param>
    /// <param name="vectorY">Y component of orientation vector (for user defined)</param>
    /// <param name="vectorZ">Z component of orientation vector (for user defined)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDesignOrientation(string areaName, int orientation,
        double vectorX = 0, double vectorY = 0, double vectorZ = 1);

    /// <summary>
    /// Gets the design orientation for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Tuple of (orientation type, vector X, Y, Z)</returns>
    (int Orientation, double VectorX, double VectorY, double VectorZ) GetDesignOrientation(string areaName);

    /// <summary>
    /// Assigns a pier label to area objects (for shear wall piers).
    /// Used to group wall segments into vertical pier elements for analysis.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="pierLabel">Name of the pier label (empty to remove)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetPier(string areaName, string pierLabel);

    /// <summary>
    /// Gets the pier label assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Pier label name (empty if none)</returns>
    string GetPier(string areaName);

    /// <summary>
    /// Assigns a spandrel label to area objects (for coupling beams).
    /// Used to identify wall coupling elements.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="spandrelLabel">Name of the spandrel label (empty to remove)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpandrel(string areaName, string spandrelLabel);

    /// <summary>
    /// Gets the spandrel label assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Spandrel label name (empty if none)</returns>
    string GetSpandrel(string areaName);

    #endregion

    #region Spring Supports

    /// <summary>
    /// Assigns distributed spring stiffness to an area object.
    /// Used for foundation slabs on elastic supports (soil springs).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="springPropertyName">Name of the area spring property</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpringAssignment(string areaName, string springPropertyName);

    /// <summary>
    /// Gets the spring property assignment for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of assigned spring property (empty if none)</returns>
    string GetSpringAssignment(string areaName);

    /// <summary>
    /// Removes spring assignment from an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteSpring(string areaName);

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for an area object.
    /// Used to adjust membrane/bending stiffness for cracked sections, etc.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="modifiers">Array of 10 modifier values: [f11, f22, f12, m11, m22, m12, v13, v23, mass, weight]</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetModifiers(string areaName, double[] modifiers);

    /// <summary>
    /// Gets property modifiers for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of 10 modifier values</returns>
    double[] GetModifiers(string areaName);

    /// <summary>
    /// Deletes property modifiers (resets to 1.0).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteModifiers(string areaName);

    #endregion

    #region Mass Assignment

    /// <summary>
    /// Assigns additional lumped mass per unit area to an area object.
    /// Used for including superimposed dead load mass in dynamic analysis.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="massPerArea">Mass per unit area in current mass/area units</param>
    /// <param name="replace">If true, replaces existing; if false, adds</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMass(string areaName, double massPerArea, bool replace = true);

    /// <summary>
    /// Gets the additional mass per area assigned to an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Mass per unit area</returns>
    double GetMass(string areaName);

    /// <summary>
    /// Deletes additional mass assignment from an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteMass(string areaName);

    #endregion

    #region Edge Constraints

    /// <summary>
    /// Assigns automatic edge constraints to area object edges.
    /// Generates frame elements along area edges (for slab edge beams, wall boundaries).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="constraintExists">If true, creates edge constraint</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetEdgeConstraint(string areaName, bool constraintExists);

    /// <summary>
    /// Gets the edge constraint status for an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if edge constraint exists, false otherwise</returns>
    bool GetEdgeConstraint(string areaName);

    #endregion

    #region Group Assignment

    /// <summary>
    /// Assigns an area object to a group.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroupAssignment(string areaName, string groupName, bool remove = false);

    /// <summary>
    /// Gets all groups that an area object is assigned to.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of group names</returns>
    string[] GetGroupAssignment(string areaName);

    #endregion

    #region Selection State

    /// <summary>
    /// Sets the selection state of an area object.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSelected(string areaName, bool selected);

    /// <summary>
    /// Checks if an area object is currently selected.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if selected, false otherwise</returns>
    bool IsSelected(string areaName);

    #endregion

    #region Label and Story Information

    /// <summary>
    /// Gets the label and story level for an area object.
    /// </summary>
    /// <param name="areaName">Unique name of the area object</param>
    /// <returns>Tuple of (Label, Story)</returns>
    (string Label, string Story) GetLabelFromName(string areaName);

    /// <summary>
    /// Gets the unique name of an area object from its label and story.
    /// </summary>
    /// <param name="label">Area label</param>
    /// <param name="story">Story name</param>
    /// <returns>Unique area object name</returns>
    string GetNameFromLabel(string label, string story);

    /// <summary>
    /// Gets all area names on a specific story.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Array of area names on that story</returns>
    string[] GetAreasOnStory(string storyName);

    #endregion
}