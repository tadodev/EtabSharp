using EtabSharp.Elements.FrameObj;
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
    string AddFrame(string point1, string point2, string sectionName = "Default", string userName = "");

    /// <summary>
    /// Adds a frame object by specifying end point coordinates directly.
    /// Useful for parametric modeling without pre-creating points.
    /// </summary>
    string AddFrameByCoordinates(double xi, double yi, double zi, double xj, double yj, double zj,
        string sectionName = "Default", string userName = "", string csys = "Global");

    /// <summary>
    /// Changes the name of an existing frame object.
    /// </summary>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Gets a frame object with its properties and connectivity.
    /// Essential for understanding frame geometry and properties.
    /// </summary>
    Frame GetFrame(string frameName);

    /// <summary>
    /// Retrieves the names of all defined frame objects in the model.
    /// </summary>
    string[] GetNameList();

    /// <summary>
    /// Gets all frame objects with their properties and connectivity.
    /// Returns comprehensive information about all frames.
    /// </summary>
    List<Frame> GetAllFrames();

    /// <summary>
    /// Gets the count of frame objects in the model.
    /// </summary>
    int Count(string frameType = "All");

    /// <summary>
    /// Deletes specified frame objects from the model.
    /// </summary>
    int Delete(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the frame type (straight or curved).
    /// </summary>
    string GetFrameType(string frameName);

    /// <summary>
    /// Gets curve data for curved frame objects.
    /// </summary>
    FrameCurveData GetCurveData(string frameName);

    #endregion

    #region Section Properties

    /// <summary>
    /// Assigns a frame section property to a frame object.
    /// Critical for defining member sizes (beam/column dimensions).
    /// </summary>
    int SetSection(string frameName, string sectionName, eItemType itemType = eItemType.Objects,
        double sVarRelStartLoc = 0.0, double sVarTotalLength = 0.0);

    /// <summary>
    /// Gets the section property assigned to a frame object.
    /// </summary>
    (string SectionName, string AutoSelectList) GetSection(string frameName);

    /// <summary>
    /// Gets non-prismatic section data for a frame.
    /// </summary>
    (string SectionName, double TotalLength, double RelStartLoc) GetSectionNonPrismatic(string frameName);

    /// <summary>
    /// Gets or sets material property override for a frame.
    /// </summary>
    int SetMaterialOverwrite(string frameName, string materialName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets material property override for a frame.
    /// </summary>
    string GetMaterialOverwrite(string frameName);

    #endregion

    #region Local Axes & Orientation

    /// <summary>
    /// Sets the local axis angle for a frame object.
    /// Essential for defining beam/column orientation (rotation about longitudinal axis).
    /// </summary>
    int SetLocalAxes(string frameName, double angleInDegrees, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the local axis angle of a frame object.
    /// </summary>
    (double Angle, bool IsAdvanced) GetLocalAxes(string frameName);

    /// <summary>
    /// Gets the design orientation for a frame object.
    /// Determines which axis is considered "strong" for design.
    /// </summary>
    eFrameDesignOrientation GetDesignOrientation(string frameName);

    #endregion

    #region End Releases & Fixity

    /// <summary>
    /// Assigns end releases (hinges/pins) to a frame object.
    /// Critical for defining connection types: pinned, fixed, or partially fixed.
    /// </summary>
    int SetReleases(string frameName, FrameReleases releases, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the end release conditions for a frame object.
    /// </summary>
    FrameReleases? GetReleases(string frameName);

    /// <summary>
    /// Removes all end releases from a frame object (makes it fully fixed).
    /// </summary>
    int DeleteReleases(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region End Releases - Convenience Methods

    /// <summary>
    /// Sets a pinned connection at the I-end (start) of a frame (releases all moments and torsion).
    /// </summary>
    int SetIEndPinned(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a pinned connection at the J-end of a frame (releases all moments and torsion).
    /// </summary>
    int SetJEndPinned(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets pinned connections at both ends of a frame (releases all moments and torsion).
    /// </summary>
    int SetBothEndsPinned(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a roller connection at the I-end (releases axial force and all moments).
    /// </summary>
    int SetIEndRoller(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a roller connection at the J-end (releases axial force and all moments).
    /// </summary>
    int SetJEndRoller(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a torsion release at the I-end (releases R1 only).
    /// </summary>
    int SetIEndTorsionRelease(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a torsion release at the J-end (releases R1 only).
    /// </summary>
    int SetJEndTorsionRelease(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets moment releases only at both ends (releases R2 and R3).
    /// Common for simple beam connections.
    /// </summary>
    int SetBothEndsMomentReleased(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets partial moment releases with specified spring stiffness values.
    /// </summary>
    int SetPartialMomentReleases(string frameName, double iEndR2Spring, double iEndR3Spring,
        double jEndR2Spring, double jEndR3Spring, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a semi-rigid connection with specified rotational stiffness.
    /// Releases R2 and R3 with partial fixity springs.
    /// </summary>
    int SetSemiRigidConnection(string frameName, FrameEnd endLocation,
        double rotationalStiffness, eItemType itemType = eItemType.Objects);

    #endregion

    #region End Offsets & Insertion Point

    /// <summary>
    /// Sets frame end length offsets (rigid zones at connections).
    /// Used to model joint sizes and rigid end zones.
    /// </summary>
    int SetEndLengthOffset(string frameName, bool autoOffset, double length1, double length2, double rz,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the end length offset assignments for a frame.
    /// </summary>
    (bool AutoOffset, double Length1, double Length2, double RZ) GetEndLengthOffset(string frameName);

    /// <summary>
    /// Sets the insertion point and end joint offsets for a frame using a model object.
    /// Defines where the frame centerline is relative to grid points.
    /// </summary>
    int SetInsertionPoint(string frameName, FrameInsertionPoint insertionPoint, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets the insertion point and end joint offsets for a frame using individual parameters.
    /// Defines where the frame centerline is relative to grid points.
    /// </summary>
    int SetInsertionPoint(string frameName, int cardinalPoint, bool mirror2, bool mirror3,
        bool stiffTransform, double[] offset1, double[] offset2, string csys = "Local",
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets insertion point data for a frame object as a model object.
    /// </summary>
    FrameInsertionPoint GetInsertionPoint(string frameName);

    /// <summary>
    /// Gets insertion point data for a frame object as individual values.
    /// </summary>
    (int CardinalPoint, bool Mirror2, bool Mirror3, bool StiffTransform, double[] Offset1, double[] Offset2, string CSys)
        GetInsertionPointValues(string frameName);

    /// <summary>
    /// Deletes the insertion point assignment for a frame object (resets to defaults).
    /// </summary>
    int DeleteInsertionPoint(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Distributed Loads

    /// <summary>
    /// Assigns a distributed load to a frame object using a model object.
    /// Essential for applying uniform or trapezoidal loads on beams.
    /// </summary>
    int SetLoadDistributed(string frameName, FrameDistributedLoad load, bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Assigns distributed loads to frame objects using individual parameters.
    /// Wraps cSapModel.FrameObj.SetLoadDistributed directly with ETABS API parameters.
    /// </summary>
    int SetLoadDistributed(string name, string loadPattern, int loadType, int direction,
        double startDistance, double endDistance, double startLoad, double endLoad,
        string coordinateSystem = "Global", bool isRelativeDistance = true, bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets distributed load assignments for a frame object.
    /// </summary>
    List<FrameDistributedLoad> GetLoadDistributed(string frameName, string loadPattern = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Deletes distributed load assignments from a frame.
    /// </summary>
    int DeleteLoadDistributed(string frameName, string loadPattern, eItemType itemType = eItemType.Objects);

    #endregion

    #region Point Loads

    /// <summary>
    /// Assigns a concentrated load at a point along a frame object.
    /// Used for applying point loads on beams (equipment, concentrated live loads).
    /// </summary>
    int SetLoadPoint(string frameName, FramePointLoad load, bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets point load assignments for a frame object.
    /// </summary>
    List<FramePointLoad> GetLoadPoint(string frameName, string loadPattern = "");

    /// <summary>
    /// Deletes point load assignments from a frame.
    /// </summary>
    int DeleteLoadPoint(string frameName, string loadPattern);

    #endregion

    #region Temperature Loads

    /// <summary>
    /// Assigns temperature load to a frame object.
    /// </summary>
    int SetLoadTemperature(string frameName, string loadPattern, int loadType, double value,
        string patternName = "", bool replace = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets temperature load assignments for frame objects.
    /// </summary>
    object GetLoadTemperature(string frameName = "", eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Deletes temperature load assignments from a frame.
    /// </summary>
    int DeleteLoadTemperature(string frameName, string loadPattern, eItemType itemType = eItemType.Objects);

    #endregion

    #region Load Convenience Methods

    /// <summary>
    /// Applies a uniform distributed load over the entire length of a frame.
    /// </summary>
    int SetUniformLoad(string frameName, string loadPattern, double loadValue,
        eFrameLoadDirection direction = eFrameLoadDirection.Gravity, string coordinateSystem = "Global", bool replace = true);

    /// <summary>
    /// Applies a triangular distributed load over the entire length of a frame.
    /// </summary>
    int SetTriangularLoad(string frameName, string loadPattern, double startLoad, double endLoad,
        eFrameLoadDirection direction = eFrameLoadDirection.Gravity, string coordinateSystem = "Global", bool replace = true);

    /// <summary>
    /// Applies a concentrated load at the midspan of a frame.
    /// </summary>
    int SetMidspanLoad(string frameName, string loadPattern, double loadValue,
        eFrameLoadDirection direction = eFrameLoadDirection.Gravity, string coordinateSystem = "Global", bool replace = true);

    /// <summary>
    /// Applies a uniform temperature change to a frame.
    /// </summary>
    int SetUniformTemperatureLoad(string frameName, string loadPattern, double temperatureChange, bool replace = true);

    /// <summary>
    /// Gets all loads (distributed, point, and temperature) for a frame.
    /// </summary>
    FrameLoads GetAllLoads(string frameName, string loadPattern = "");

    #endregion

    #region Design & Label Assignment

    /// <summary>
    /// Sets the design procedure/type for a frame object.
    /// Defines whether it's designed as a column, beam, or brace.
    /// </summary>
    int SetDesignProcedure(string frameName, int designType, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the design procedure assigned to a frame object.
    /// </summary>
    int GetDesignProcedure(string frameName);

    /// <summary>
    /// Assigns a pier label to frame objects (for lateral analysis).
    /// Used to group columns into vertical pier elements.
    /// </summary>
    int SetPier(string frameName, string pierLabel, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the pier label assignment for a frame object.
    /// </summary>
    string GetPier(string frameName);

    /// <summary>
    /// Assigns a spandrel label to frame objects (for lateral analysis).
    /// Used to group beams into horizontal spandrel elements.
    /// </summary>
    int SetSpandrel(string frameName, string spandrelLabel, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the spandrel label assignment for a frame object.
    /// </summary>
    string GetSpandrel(string frameName);

    /// <summary>
    /// Sets column splice overwrite at a specific height.
    /// </summary>
    int SetColumnSpliceOverwrite(string frameName, int spliceOption, double height,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets column splice overwrite data.
    /// </summary>
    (int SpliceOption, double Height) GetColumnSpliceOverwrite(string frameName);

    #endregion

    #region Design Convenience Methods

    /// <summary>
    /// Sets a frame for steel design.
    /// </summary>
    int SetSteelDesign(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a frame for concrete design.
    /// </summary>
    int SetConcreteDesign(string frameName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Disables design for a frame.
    /// </summary>
    int SetNoDesign(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Output Stations

    /// <summary>
    /// Sets the output stations for analysis results along a frame.
    /// Defines where forces/stresses are calculated along the member.
    /// </summary>
    int SetOutputStations(string frameName, int stationType, double maxSegmentSize, int minStations,
        bool noOutputAtElementEnds = false, bool noOutputAtPointLoads = false,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets output station data for a frame object.
    /// </summary>
    (int StationType, double MaxSegSize, int MinStations, bool NoOutputAtEnds, bool NoOutputAtLoads)
        GetOutputStations(string frameName);

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for a frame object.
    /// Used to adjust member stiffness for cracked sections, construction sequencing, etc.
    /// </summary>
    int SetModifiers(string frameName, double[] modifiers, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets property modifiers for a frame object.
    /// </summary>
    double[] GetModifiers(string frameName);

    /// <summary>
    /// Deletes property modifiers (resets to 1.0).
    /// </summary>
    int DeleteModifiers(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Mass Assignment

    /// <summary>
    /// Assigns additional mass per unit length to a frame object.
    /// </summary>
    int SetMass(string frameName, double massPerLength, bool replace = false,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the additional mass per length assigned to a frame.
    /// </summary>
    double GetMass(string frameName);

    /// <summary>
    /// Deletes mass assignment from a frame.
    /// </summary>
    int DeleteMass(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Spring Assignment

    /// <summary>
    /// Assigns a named line spring property to a frame object.
    /// Used for elastic supports along the frame length.
    /// </summary>
    int SetSpringAssignment(string frameName, string springPropertyName,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the spring property assignment for a frame object.
    /// </summary>
    string GetSpringAssignment(string frameName);

    /// <summary>
    /// Deletes spring assignment from a frame.
    /// </summary>
    int DeleteSpring(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Lateral Bracing

    /// <summary>
    /// Assigns lateral bracing to a frame object.
    /// Defines locations where the frame is braced against lateral-torsional buckling.
    /// </summary>
    int SetLateralBracing(string frameName, BracingType bracingType, BracingLocation location,
        double distance1, double distance2, bool relDist = true, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets lateral bracing assignments for a frame.
    /// </summary>
    LateralBracingData GetLateralBracing(string frameName);

    /// <summary>
    /// Deletes lateral bracing assignments from a frame.
    /// </summary>
    int DeleteLateralBracing(string frameName, BracingType bracingType,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Clears all lateral bracing for a frame object.
    /// This is a convenience method that uses SetLateralBracing with MyType=3.
    /// </summary>
    int ClearAllLateralBracing(string frameName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Lateral Bracing Convenience Methods

    /// <summary>
    /// Sets full lateral bracing for the entire length of a frame.
    /// </summary>
    int SetFullLateralBracing(string frameName, BracingType bracingType, BracingLocation bracingLocation,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets point lateral bracing at specific locations on a frame.
    /// </summary>
    int SetPointLateralBracing(string frameName, BracingType bracingType, BracingLocation bracingLocation,
        double distance, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Clears all lateral bracing from a frame.
    /// </summary>
    int ClearLateralBracing(string frameName, BracingType bracingType, eItemType itemType = eItemType.Objects);

    #endregion

    #region Tension/Compression Limits

    /// <summary>
    /// Assigns tension/compression force limits to frame objects.
    /// Used to model cables (tension-only) or struts (compression-only).
    /// </summary>
    int SetTCLimits(string frameName, bool limitCompressionExists, double limitCompression,
        bool limitTensionExists, double limitTension, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets tension/compression limits for a frame object.
    /// </summary>
    (bool CompressionExists, double CompressionLimit, bool TensionExists, double TensionLimit)
        GetTCLimits(string frameName);

    #endregion

    #region Group Assignment

    /// <summary>
    /// Assigns a frame object to a group.
    /// </summary>
    int SetGroupAssignment(string frameName, string groupName, bool remove = false,
        eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets all groups that a frame object is assigned to.
    /// </summary>
    string[] GetGroupAssignment(string frameName);

    #endregion

    #region Selection State

    /// <summary>
    /// Sets the selection state of a frame object.
    /// </summary>
    int SetSelected(string frameName, bool selected, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Checks if a frame object is currently selected.
    /// </summary>
    bool IsSelected(string frameName);

    #endregion

    #region Label and Story Information

    /// <summary>
    /// Gets all frame names with their labels and stories.
    /// </summary>
    (string[] Names, string[] Labels, string[] Stories) GetLabelNameList();

    /// <summary>
    /// Gets the label and story level for a frame object.
    /// </summary>
    (string Label, string Story) GetLabelFromName(string frameName);

    /// <summary>
    /// Gets the unique name of a frame object from its label and story.
    /// </summary>
    string GetNameFromLabel(string label, string story);

    /// <summary>
    /// Gets all frame names on a specific story.
    /// </summary>
    string[] GetFramesOnStory(string storyName);

    #endregion
}