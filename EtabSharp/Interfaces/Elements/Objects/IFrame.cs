using EtabSharp.Elements.FrameObj.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Elements.Objects;

/// <summary>
/// Provides methods for managing frame objects in the ETABS model.
/// Frame objects represent beams, columns, braces - the 1D structural elements.
/// From a structural engineer's perspective, these are the primary load-carrying members.
/// </summary>
public interface IFrame
{
    #region Frame Creation & Geometry

    /// <summary>
    /// Adds a frame object by specifying end point names.
    /// This is the standard method for creating beams, columns, and braces.
    /// </summary>
    /// <param name="point1">Name of the first (I-end) point object</param>
    /// <param name="point2">Name of the second (J-end) point object</param>
    /// <param name="sectionName">Name of the frame section property to assign (default: "Default")</param>
    /// <param name="userName">Optional custom name (auto-generated if empty)</param>
    /// <returns>The name of the created frame object</returns>
    /// <exception cref="ArgumentException">If point names or section is invalid</exception>
    string AddFrame(string point1, string point2, string sectionName = "Default", string userName = "");

    /// <summary>
    /// Adds a frame object by specifying end point coordinates directly.
    /// Useful for parametric modeling without pre-creating points.
    /// </summary>
    /// <param name="xi">X coordinate of first point (I-end)</param>
    /// <param name="yi">Y coordinate of first point (I-end)</param>
    /// <param name="zi">Z coordinate of first point (I-end)</param>
    /// <param name="xj">X coordinate of second point (J-end)</param>
    /// <param name="yj">Y coordinate of second point (J-end)</param>
    /// <param name="zj">Z coordinate of second point (J-end)</param>
    /// <param name="sectionName">Name of the frame section property (default: "Default")</param>
    /// <param name="userName">Optional custom name</param>
    /// <param name="csys">Coordinate system (default: "Global")</param>
    /// <returns>The name of the created frame object</returns>
    string AddFrameByCoordinates(double xi, double yi, double zi, double xj, double yj, double zj,
        string sectionName = "Default", string userName = "", string csys = "Global");

    /// <summary>
    /// Changes the name of an existing frame object.
    /// </summary>
    /// <param name="currentName">Current name of the frame</param>
    /// <param name="newName">New name for the frame</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Gets a frame object with its properties and connectivity.
    /// Essential for understanding frame geometry and properties.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Frame model with properties and connectivity</returns>
    Frame GetFrame(string frameName);

    /// <summary>
    /// Retrieves the names of all defined frame objects in the model.
    /// </summary>
    /// <returns>Array of frame object names</returns>
    string[] GetNameList();

    /// <summary>
    /// Gets all frame objects with their properties and connectivity.
    /// Returns comprehensive information about all frames.
    /// </summary>
    /// <returns>List of Frame models with properties and connectivity</returns>
    List<Frame> GetAllFrames();

    /// <summary>
    /// Gets the count of frame objects in the model.
    /// </summary>
    /// <param name="frameType">Frame type filter: "All", "Straight", "Curved" (default: "All")</param>
    /// <returns>Total number of frames</returns>
    int Count(string frameType = "All");

    /// <summary>
    /// Deletes specified frame objects from the model.
    /// </summary>
    /// <param name="frameName">Name of the frame to delete</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Delete(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the frame type (straight or curved).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Frame type string</returns>
    string GetFrameType(string frameName);

    /// <summary>
    /// Gets curve data for curved frame objects.
    /// </summary>
    /// <param name="frameName">Name of the curved frame</param>
    /// <returns>Curve data (curve type, tension, control points)</returns>
    object GetCurveData(string frameName);

    #endregion

    #region Section Properties

    /// <summary>
    /// Assigns a frame section property to a frame object.
    /// Critical for defining member sizes (beam/column dimensions).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="sectionName">Name of the frame section property</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <param name="sVarRelStartLoc">Relative start location for non-prismatic section (0.0-1.0)</param>
    /// <param name="sVarTotalLength">Total length for non-prismatic section variation</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSection(string frameName, string sectionName, eItemType itemType = eItemType.Objects,
        double sVarRelStartLoc = 0.0, double sVarTotalLength = 0.0);

    /// <summary>
    /// Gets the section property assigned to a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (section name, auto-select list name)</returns>
    (string SectionName, string AutoSelectList) GetSection(string frameName);

    /// <summary>
    /// Gets non-prismatic section data for a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (section name, total length, relative start location)</returns>
    (string SectionName, double TotalLength, double RelStartLoc) GetSectionNonPrismatic(string frameName);

    /// <summary>
    /// Gets or sets material property override for a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="materialName">Material property name to override</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMaterialOverwrite(string frameName, string materialName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets material property override for a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Material property name (empty if no override)</returns>
    string GetMaterialOverwrite(string frameName);

    #endregion

    #region Local Axes & Orientation

    /// <summary>
    /// Sets the local axis angle for a frame object.
    /// Essential for defining beam/column orientation (rotation about longitudinal axis).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="angleInDegrees">Rotation angle in degrees</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLocalAxes(string frameName, double angleInDegrees, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the local axis angle of a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (angle in degrees, isAdvanced)</returns>
    (double Angle, bool IsAdvanced) GetLocalAxes(string frameName);

    /// <summary>
    /// Gets the design orientation for a frame object.
    /// Determines which axis is considered "strong" for design.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Design orientation enum value</returns>
    eFrameDesignOrientation GetDesignOrientation(string frameName);

    #endregion

    #region End Releases & Fixity

    /// <summary>
    /// Assigns end releases (hinges/pins) to a frame object.
    /// Critical for defining connection types: pinned, fixed, or partially fixed.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="releases">FrameReleases model with I-end and J-end release conditions</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetReleases(string frameName, FrameReleases releases, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the end release conditions for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>FrameReleases model with I-end and J-end release conditions, or null if no releases</returns>
    FrameReleases? GetReleases(string frameName);

    #endregion

    #region End Offsets & Insertion Point

    /// <summary>
    /// Sets frame end length offsets (rigid zones at connections).
    /// Used to model joint sizes and rigid end zones.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="autoOffset">True for automatic offset calculation</param>
    /// <param name="length1">Rigid zone length at I-end (ignored if autoOffset=true)</param>
    /// <param name="length2">Rigid zone length at J-end (ignored if autoOffset=true)</param>
    /// <param name="rz">Rigid zone factor (typically 1.0)</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetEndLengthOffset(string frameName, bool autoOffset, double length1, double length2, double rz,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the end length offset assignments for a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (autoOffset, length1, length2, rigidZoneFactor)</returns>
    (bool AutoOffset, double Length1, double Length2, double RZ) GetEndLengthOffset(string frameName);

    /// <summary>
    /// Sets the insertion point and end joint offsets for a frame.
    /// Defines where the frame centerline is relative to grid points.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="cardinalPoint">Cardinal point (1-11): 1=bottom-left, 10=centroid, etc.</param>
    /// <param name="mirror2">Mirror about local 2-axis</param>
    /// <param name="mirror3">Mirror about local 3-axis</param>
    /// <param name="stiffTransform">Transform stiffness for offsets</param>
    /// <param name="offset1">3D offset at I-end [dx, dy, dz]</param>
    /// <param name="offset2">3D offset at J-end [dx, dy, dz]</param>
    /// <param name="csys">Coordinate system for offsets (default: "Local")</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetInsertionPoint(string frameName, int cardinalPoint, bool mirror2, bool mirror3,
        bool stiffTransform, double[] offset1, double[] offset2, string csys = "Local",
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets insertion point data for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (cardinalPoint, mirror2, mirror3, stiffTransform, offset1, offset2, csys)</returns>
    FrameInsertionPoint
        GetInsertionPoint(string frameName);

    #endregion

    #region Distributed Loads

    /// <summary>
    /// Assigns a distributed load to a frame object.
    /// Essential for applying uniform or trapezoidal loads on beams.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="load">FrameDistributedLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds</param>
    /// <param name="itemType">Item type for assignment (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadDistributed(string frameName, FrameDistributedLoad load, bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Assigns distributed loads to frame objects using individual parameters.
    /// Wraps cSapModel.FrameObj.SetLoadDistributed directly with ETABS API parameters.
    /// </summary>
    /// <param name="name">Name of an existing frame object or group</param>
    /// <param name="loadPattern">Name of a defined load pattern</param>
    /// <param name="loadType">Type of distributed load (1=Force per unit length, 2=Moment per unit length)</param>
    /// <param name="direction">Load direction (1-11)</param>
    /// <param name="startDistance">Distance from I-End to start of load</param>
    /// <param name="endDistance">Distance from I-End to end of load</param>
    /// <param name="startLoad">Load value at start</param>
    /// <param name="endLoad">Load value at end</param>
    /// <param name="coordinateSystem">Coordinate system name ("Local" or coordinate system name)</param>
    /// <param name="isRelativeDistance">If true, distances are relative (0-1); if false, absolute distances</param>
    /// <param name="replace">If true, replaces all previous loads in the specified load pattern</param>
    /// <param name="itemType">Item type for assignment (Objects, Group, or SelectedObjects)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadDistributed(string name, string loadPattern, int loadType, int direction,
        double startDistance, double endDistance, double startLoad, double endLoad,
        string coordinateSystem = "Global", bool isRelativeDistance = true, bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets distributed load assignments for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Load pattern name (empty for all patterns)</param>
    /// <param name="itemType">Item type 1-object, 2- Group, 3-SelectedObject</param>
    /// <returns>List of FrameDistributedLoad models</returns>
    public List<FrameDistributedLoad> GetLoadDistributed(string frameName, string loadPattern = "",
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Deletes distributed load assignments from a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Item type 1-object, 2- Group, 3-SelectedObject</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLoadDistributed(string frameName, string loadPattern, eItemType itemType = eItemType.Objects);

    #endregion

    #region Point Loads

    /// <summary>
    /// Assigns a concentrated load at a point along a frame object.
    /// Used for applying point loads on beams (equipment, concentrated live loads).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="load">FramePointLoad model with load parameters</param>
    /// <param name="replace">If true, replaces existing; if false, adds</param>
    /// <param name="itemType">Object selected type</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadPoint(string frameName, FramePointLoad load, bool replace = true, eItemType itemType =eItemType.Objects);

    /// <summary>
    /// Gets point load assignments for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Load pattern name (empty for all)</param>
    /// <returns>List of FramePointLoad models</returns>
    List<FramePointLoad> GetLoadPoint(string frameName, string loadPattern = "");

    /// <summary>
    /// Deletes point load assignments from a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadPoint(string frameName, string loadPattern);

    #endregion

    #region Temperature Loads

    /// <summary>
    /// Assigns temperature load to a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="loadType">Temperature load type</param>
    /// <param name="value">Temperature value</param>
    /// <param name="patternName">Joint pattern name (if applicable)</param>
    /// <param name="replace">If true, replaces existing; if false, adds</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadTemperature(string frameName, string loadPattern, int loadType, double value,
        string patternName = "", bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets temperature load assignments for frame objects.
    /// </summary>
    /// <param name="frameName">Name of the frame object (empty for all)</param>
    /// <param name="itemType">Query object, group, or selected objects</param>
    /// <returns>Temperature load data</returns>
    object GetLoadTemperature(string frameName = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Deletes temperature load assignments from a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadTemperature(string frameName, string loadPattern, eItemType itemType = eItemType.Objects);

    #endregion

    #region Design & Label Assignment

    /// <summary>
    /// Sets the design procedure/type for a frame object.
    /// Defines whether it's designed as a column, beam, or brace.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="designType">0=Program determined, 1=Column, 2=Beam, 3=Brace</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDesignProcedure(string frameName, int designType, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the design procedure assigned to a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Design type: 0=Auto, 1=Column, 2=Beam, 3=Brace</returns>
    int GetDesignProcedure(string frameName);

    /// <summary>
    /// Assigns a pier label to frame objects (for lateral analysis).
    /// Used to group columns into vertical pier elements.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="pierLabel">Name of the pier label (empty to remove)</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetPier(string frameName, string pierLabel, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the pier label assignment for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Pier label name (empty if none)</returns>
    string GetPier(string frameName);

    /// <summary>
    /// Assigns a spandrel label to frame objects (for lateral analysis).
    /// Used to group beams into horizontal spandrel elements.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="spandrelLabel">Name of the spandrel label (empty to remove)</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpandrel(string frameName, string spandrelLabel, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the spandrel label assignment for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Spandrel label name (empty if none)</returns>
    string GetSpandrel(string frameName);

    /// <summary>
    /// Sets column splice overwrite at a specific height.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="spliceOption">Splice option</param>
    /// <param name="height">Height of splice location</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetColumnSpliceOverwrite(string frameName, int spliceOption, double height,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets column splice overwrite data.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (splice option, height)</returns>
    (int SpliceOption, double Height) GetColumnSpliceOverwrite(string frameName);

    #endregion

    #region Output Stations

    /// <summary>
    /// Sets the output stations for analysis results along a frame.
    /// Defines where forces/stresses are calculated along the member.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="stationType">0=MinNumStations, 1=MaxSpacing</param>
    /// <param name="maxSegmentSize">Maximum segment size between stations</param>
    /// <param name="minStations">Minimum number of stations</param>
    /// <param name="noOutputAtElementEnds">If true, suppresses output at element ends</param>
    /// <param name="noOutputAtPointLoads">If true, suppresses output at point loads</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetOutputStations(string frameName, int stationType, double maxSegmentSize, int minStations,
        bool noOutputAtElementEnds = false, bool noOutputAtPointLoads = false,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets output station data for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (stationType, maxSegSize, minStations, noOutputAtEnds, noOutputAtLoads)</returns>
    (int StationType, double MaxSegSize, int MinStations, bool NoOutputAtEnds, bool NoOutputAtLoads)
        GetOutputStations(string frameName);

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for a frame object.
    /// Used to adjust member stiffness for cracked sections, construction sequencing, etc.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="modifiers">Array of 8 modifier values: [Area, As2, As3, Torsion, I22, I33, Mass, Weight]</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetModifiers(string frameName, double[] modifiers, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets property modifiers for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Array of 8 modifier values</returns>
    double[] GetModifiers(string frameName);

    /// <summary>
    /// Deletes property modifiers (resets to 1.0).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteModifiers(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Mass Assignment

    /// <summary>
    /// Assigns additional mass per unit length to a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="massPerLength">Mass per unit length</param>
    /// <param name="replace">If true, replaces existing; if false, adds</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMass(string frameName, double massPerLength, bool replace = false,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the additional mass per length assigned to a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Mass per unit length</returns>
    double GetMass(string frameName);

    /// <summary>
    /// Deletes mass assignment from a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteMass(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Spring Assignment

    /// <summary>
    /// Assigns a named line spring property to a frame object.
    /// Used for elastic supports along the frame length.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="springPropertyName">Name of the line spring property</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpringAssignment(string frameName, string springPropertyName,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the spring property assignment for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Name of spring property (empty if none)</returns>
    string GetSpringAssignment(string frameName);

    /// <summary>
    /// Deletes spring assignment from a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteSpring(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Lateral Bracing

    /// <summary>
    /// Assigns lateral bracing to a frame object.
    /// Defines locations where the frame is braced against lateral-torsional buckling.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="bracingType">Bracing type</param>
    /// <param name="location">Location type</param>
    /// <param name="distance1">Start distance</param>
    /// <param name="distance2">End distance</param>
    /// <param name="relDist">Relative distance (0-1)</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLateralBracing(string frameName, BracingType bracingType, BracingLocation location,
        double distance1, double distance2, bool relDist = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets lateral bracing assignments for a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Lateral bracing data</returns>
    public LateralBracingData GetLateralBracing(string frameName);

    /// <summary>
    /// Deletes lateral bracing assignments from a frame.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="bracingType">Bracing type to delete (3 = all types)</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteLateralBracing(string frameName, BracingType bracingType,
        eItemType itemType = eItemType.Objects);

    #endregion

    #region Tension/Compression Limits

    /// <summary>
    /// Assigns tension/compression force limits to frame objects.
    /// Used to model cables (tension-only) or struts (compression-only).
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="limitCompressionExists">True to apply compression limit</param>
    /// <param name="limitCompression">Compression force limit (negative value)</param>
    /// <param name="limitTensionExists">True to apply tension limit</param>
    /// <param name="limitTension">Tension force limit (positive value)</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetTCLimits(string frameName, bool limitCompressionExists, double limitCompression,
        bool limitTensionExists, double limitTension, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets tension/compression limits for a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (compressionExists, compressionLimit, tensionExists, tensionLimit)</returns>
    (bool CompressionExists, double CompressionLimit, bool TensionExists, double TensionLimit)
        GetTCLimits(string frameName);

    #endregion

    #region Group Assignment

    /// <summary>
    /// Assigns a frame object to a group.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroupAssignment(string frameName, string groupName, bool remove = false,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets all groups that a frame object is assigned to.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Array of group names</returns>
    string[] GetGroupAssignment(string frameName);

    #endregion

    #region Selection State

    /// <summary>
    /// Sets the selection state of a frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSelected(string frameName, bool selected, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Checks if a frame object is currently selected.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>True if selected, false otherwise</returns>
    bool IsSelected(string frameName);

    #endregion

    #region Label and Story Information

    /// <summary>
    /// Gets all frame names with their labels and stories.
    /// </summary>
    /// <returns>Tuple of (Names, Labels, Stories)</returns>
    (string[] Names, string[] Labels, string[] Stories) GetLabelNameList();

    /// <summary>
    /// Gets the label and story level for a frame object.
    /// </summary>
    /// <param name="frameName">Unique name of the frame object</param>
    /// <returns>Tuple of (Label, Story)</returns>
    (string Label, string Story) GetLabelFromName(string frameName);

    /// <summary>
    /// Gets the unique name of a frame object from its label and story.
    /// </summary>
    /// <param name="label">Frame label</param>
    /// <param name="story">Story name</param>
    /// <returns>Unique frame object name</returns>
    string GetNameFromLabel(string label, string story);

    /// <summary>
    /// Gets all frame names on a specific story.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Array of frame names on that story</returns>
    string[] GetFramesOnStory(string storyName);

    #endregion

}