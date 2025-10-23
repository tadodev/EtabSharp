using EtabSharp.Elements.PointObj.Models;
using ETABSv1;

namespace EtabSharp.Interfaces.Elements.Objects;

/// <summary>
/// Provides methods for managing point objects in the ETABS model.
/// Point objects are joints/nodes - the fundamental connection points in the structure.
/// these are where loads are applied, supports are defined, and frame/area elements connect.
/// </summary>
public interface IPoint
{
    #region Point Creation & Geometry

    /// <summary>
    /// Adds a point object at specified Cartesian coordinates.
    /// This is the primary method for creating joints in 3D space.
    /// </summary>
    /// <param name="x">X coordinate in current length units</param>
    /// <param name="y">Y coordinate in current length units</param>
    /// <param name="z">Z coordinate (elevation) in current length units</param>
    /// <param name="userName">Optional custom name for the point (auto-generated if empty)</param>
    /// <param name="csys">Coordinate system name (default: "Global")</param>
    /// <returns>The name of the created point object</returns>
    string AddPoint(double x, double y, double z, string userName = "", string csys = "Global");

    /// <summary>
    /// Changes the name of an existing point object.
    /// </summary>
    /// <param name="currentName">Current name of the point</param>
    /// <param name="newName">New name for the point</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int ChangeName(string currentName, string newName);

    /// <summary>
    /// Gets a point object with its coordinates and properties.
    /// Essential for verifying geometry and extracting node information.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Point model with coordinates and properties</returns>
    Point GetPoint(string pointName);

    /// <summary>
    /// Gets all point objects with their coordinates and properties.
    /// </summary>
    /// <returns>List of Point models</returns>
    List<Point> GetAllPoints();

    /// <summary>
    /// Retrieves the names of all defined point objects in the model.
    /// Useful for iterating through all joints.
    /// </summary>
    /// <returns>Array of point object names</returns>
    string[] GetNameList();

    /// <summary>
    /// Gets the count of point objects in the model.
    /// </summary>
    /// <returns>Total number of points</returns>
    int Count();

    #endregion

    #region Restraints (Supports)

    /// <summary>
    /// Assigns restraint conditions (supports) to a point object.
    /// This is critical for defining boundary conditions.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="restraint">PointRestraint model with restraint conditions</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetRestraint(string pointName, PointRestraint restraint, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the restraint conditions assigned to a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointRestraint model with restraint conditions</returns>
    PointRestraint? GetRestraint(string pointName);

    /// <summary>
    /// Removes all restraints from a point object (makes it free).
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteRestraint(string pointName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Spring Supports

    /// <summary>
    /// Assigns spring stiffness to a point object.
    /// Used for flexible supports like soil springs.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="spring">PointSpring model with spring stiffness values</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpring(string pointName, PointSpring spring);

    /// <summary>
    /// Gets the spring stiffness values assigned to a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointSpring model with spring stiffness values</returns>
    PointSpring? GetSpring(string pointName);

    /// <summary>
    /// Removes all spring assignments from a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteSpring(string pointName);

    #endregion

    #region Point Loads

    /// <summary>
    /// Assigns a concentrated force/moment to a point object.
    /// Essential for applying nodal loads.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="load">PointLoad model with force/moment values and load pattern</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadForce(string pointName, PointLoad load, bool replace = true);

    /// <summary>
    /// Gets the force loads assigned to a point object for a specific load pattern.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>PointLoad model with force/moment values, or null if no load found</returns>
    PointLoad? GetLoadForce(string pointName, string loadPattern);

    /// <summary>
    /// Deletes all force loads from a point object for a specific load pattern.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadForce(string pointName, string loadPattern);

    #endregion

    #region Ground Displacement (for Seismic/Settlement)

    /// <summary>
    /// Assigns ground displacement to a point object.
    /// Used for support settlement or seismic base motion.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="displacement">PointDisplacement model with displacement values and load pattern</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadDisplacement(string pointName, PointDisplacement displacement, bool replace = true);

    /// <summary>
    /// Gets ground displacement assignments for a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>PointDisplacement model with displacement values, or null if no displacement found</returns>
    PointDisplacement? GetLoadDisplacement(string pointName, string loadPattern);

    /// <summary>
    /// Deletes ground displacement assignments from a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteLoadDisplacement(string pointName, string loadPattern);

    #endregion

    #region Diaphragm Assignment

    /// <summary>
    /// Assigns a point to a diaphragm (rigid floor constraint).
    /// Critical for modeling floor diaphragm behavior.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="diaphragmOption">Diaphragm option type</param>
    /// <param name="diaphragmName">Name of the diaphragm (empty string to remove assignment)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDiaphragm(string pointName, eDiaphragmOption diaphragmOption, string diaphragmName);

    /// <summary>
    /// Gets the diaphragm assignment for a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Name of assigned diaphragm (empty if none)</returns>
    string GetDiaphragm(string pointName);

    #endregion

    #region Mass Assignment

    /// <summary>
    /// Assigns lumped mass to a point object.
    /// Used for dynamic analysis and representing dead load mass.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="mass">PointMass model with mass values</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMass(string pointName, PointMass mass, bool replace = true);

    /// <summary>
    /// Gets the lumped mass assigned to a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointMass model with mass values, or null if no mass assigned</returns>
    PointMass? GetMass(string pointName);

    /// <summary>
    /// Deletes mass assignment from a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteMass(string pointName);

    #endregion

    #region Connectivity & Relationships

    /// <summary>
    /// Gets all frame, area, and other objects connected to a point.
    /// Essential for understanding structural connectivity.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointConnectivity model with connected object information</returns>
    PointConnectivity GetConnectedObjects(string pointName);

    /// <summary>
    /// Checks if a point is connected to any structural elements.
    /// Useful for identifying isolated/unused points.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>True if connected to any element, false otherwise</returns>
    bool IsConnected(string pointName);

    #endregion

    #region Special Points & Cleanup

    /// <summary>
    /// Marks a point as a "special point" that can exist without connections.
    /// By default, ETABS removes unconnected points.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="isSpecial">True to mark as special, false to unmark</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpecialPoint(string pointName, bool isSpecial);

    /// <summary>
    /// Checks if a point is marked as special.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>True if special point, false otherwise</returns>
    bool IsSpecialPoint(string pointName);

    /// <summary>
    /// Deletes all special points that have no connected elements.
    /// Useful for model cleanup.
    /// </summary>
    /// <returns>Number of points deleted</returns>
    int DeleteUnconnectedSpecialPoints();

    #endregion

    #region Group Assignment

    /// <summary>
    /// Assigns a point object to a group.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroupAssignment(string pointName, string groupName, bool remove = false);

    /// <summary>
    /// Gets all groups that a point object is assigned to.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Array of group names</returns>
    string[] GetGroupAssignment(string pointName);

    #endregion

    #region Selection State

    /// <summary>
    /// Sets the selection state of a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSelected(string pointName, bool selected);

    /// <summary>
    /// Checks if a point object is currently selected.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>True if selected, false otherwise</returns>
    bool IsSelected(string pointName);

    #endregion

    #region Label and Story Methods

    /// <summary>
    /// Gets points by story level.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Array of point names on the specified story</returns>
    string[] GetPointsOnStory(string storyName);

    /// <summary>
    /// Gets label and story information for all points.
    /// </summary>
    /// <returns>Tuple of (Names, Labels, Stories)</returns>
    (string[] Names, string[] Labels, string[] Stories) GetLabelNameList();

    /// <summary>
    /// Gets the unique name from label and story.
    /// </summary>
    /// <param name="label">Point label</param>
    /// <param name="story">Story name</param>
    /// <returns>Unique point name</returns>
    string GetNameFromLabel(string label, string story);

    #endregion

    #region Convenience Methods - MISSING FROM ORIGINAL INTERFACE

    /// <summary>
    /// Sets a fixed support (all DOFs restrained) at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetFixedSupport(string pointName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a pinned support (translations restrained, rotations free) at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetPinnedSupport(string pointName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a roller support in Z direction (Uz restrained only) at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetRollerSupport(string pointName, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Sets a spring support with equal stiffness in all translational directions.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="translationalStiffness">Stiffness in X, Y, Z directions</param>
    /// <param name="rotationalStiffness">Stiffness in RX, RY, RZ directions (optional)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetUniformSpringSupport(string pointName, double translationalStiffness, double rotationalStiffness = 0);

    /// <summary>
    /// Sets a soil spring support with different stiffness in horizontal and vertical directions.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="horizontalStiffness">Stiffness in X and Y directions</param>
    /// <param name="verticalStiffness">Stiffness in Z direction</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSoilSpringSupport(string pointName, double horizontalStiffness, double verticalStiffness);

    /// <summary>
    /// Applies a vertical downward force at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="force">Downward force magnitude (positive value)</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetVerticalLoad(string pointName, string loadPattern, double force, bool replace = true);

    /// <summary>
    /// Applies horizontal forces at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="fx">Force in X direction</param>
    /// <param name="fy">Force in Y direction</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetHorizontalLoad(string pointName, string loadPattern, double fx, double fy, bool replace = true);

    /// <summary>
    /// Applies a moment about the Z-axis at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="moment">Moment magnitude</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetTorsionalMoment(string pointName, string loadPattern, double moment, bool replace = true);

    /// <summary>
    /// Applies support settlement (vertical displacement) at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="settlement">Settlement magnitude (positive for downward)</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSupportSettlement(string pointName, string loadPattern, double settlement, bool replace = true);

    /// <summary>
    /// Gets all force loads for a point across all load patterns.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>List of PointLoad models for all patterns</returns>
    List<PointLoad> GetAllForceLoads(string pointName);

    /// <summary>
    /// Gets all displacement loads for a point across all load patterns.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>List of PointDisplacement models for all patterns</returns>
    List<PointDisplacement> GetAllDisplacementLoads(string pointName);

    /// <summary>
    /// Sets uniform mass in all translational directions at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="mass">Mass value for all translational directions</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetUniformMass(string pointName, double mass, bool replace = true);

    /// <summary>
    /// Gets a complete point object with all properties populated.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Complete Point model with all properties</returns>
    Point GetCompletePoint(string pointName);

    #endregion
}