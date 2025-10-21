using ETABSv1;

namespace EtabSharp.Interfaces.Elements.Objects;

/// <summary>
/// Provides methods for managing point objects in the ETABS model.
/// Point objects are joints/nodes - the fundamental connection points in the structure.
/// these are where loads are applied,supports are defined, and frame/area elements connect.
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
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails</exception>
    string AddPoint(double x, double y, double z, string userName = "", string csys = "Global");

    /// <summary>
    /// Gets the Cartesian coordinates of a point object.
    /// Essential for verifying geometry and extracting node locations.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Tuple of (X, Y, Z) coordinates in current units</returns>
    (double X, double Y, double Z) GetCoordinate(string pointName);

    /// <summary>
    /// Gets the Cartesian coordinates of all point objects.
    /// </summary>
    /// <returns></returns>
    (double[] X, double[] Y, double[] Z) GetAllPointCoordinates();

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
    /// <param name="restraints">6 boolean values: [Ux, Uy, Uz, Rx, Ry, Rz] where true = restrained</param>
    /// <param name="itemType"></param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    /// <exception cref="ArgumentException">If restraints array length ≠ 6</exception>
    int SetRestraint(string pointName, bool[] restraints, eItemType itemType = eItemType.Objects);

    /// <summary>
    /// Gets the restraint conditions assigned to a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Array of 6 booleans: [Ux, Uy, Uz, Rx, Ry, Rz] where true = restrained</returns>
    bool[] GetRestraint(string pointName);

    /// <summary>
    /// Removes all restraints from a point object (makes it free).
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType"></param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int DeleteRestraint(string pointName, eItemType itemType = eItemType.Objects);

    #endregion

    #region Spring Supports

    /// <summary>
    /// Assigns spring stiffness (uncoupled) to a point object.
    /// Used for flexible supports like soil springs.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="springs">6 spring values: [Kx, Ky, Kz, Krx, Kry, Krz] in current force/length units</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetSpring(string pointName, double[] springs);

    /// <summary>
    /// Gets the spring stiffness values assigned to a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Array of 6 spring values: [Kx, Ky, Kz, Krx, Kry, Krz]</returns>
    double[] GetSpring(string pointName);

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
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="forces">6 force/moment values: [Fx, Fy, Fz, Mx, My, Mz]</param>
    /// <param name="replace">If true, replaces existing loads; if false, adds to existing</param>
    /// <param name="csys">Coordinate system for load application (default: "Global")</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadForce(string pointName, string loadPattern, double[] forces,
        bool replace = true, string csys = "Global");

    /// <summary>
    /// Gets the force loads assigned to a point object for a specific load pattern.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>Array of 6 force/moment values: [Fx, Fy, Fz, Mx, My, Mz]</returns>
    double[] GetLoadForce(string pointName, string loadPattern);

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
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <param name="displacements">6 displacement values: [Ux, Uy, Uz, Rx, Ry, Rz]</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetLoadDisplacement(string pointName, string loadPattern, double[] displacements, bool replace = true);

    /// <summary>
    /// Gets ground displacement assignments for a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="loadPattern">Name of the load pattern</param>
    /// <returns>Array of 6 displacement values: [Ux, Uy, Uz, Rx, Ry, Rz]</returns>
    double[] GetLoadDisplacement(string pointName, string loadPattern);

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
    /// <param name="diaphragmName">Name of the diaphragm (empty string to remove assignment)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetDiaphragm(string pointName, string diaphragmName);

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
    /// <param name="massValues">6 mass values: [Mx, My, Mz, MMx, MMy, MMz] in current mass units</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetMass(string pointName, double[] massValues, bool replace = true);

    /// <summary>
    /// Gets the lumped mass assigned to a point object.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Array of 6 mass values: [Mx, My, Mz, MMx, MMy, MMz]</returns>
    double[] GetMass(string pointName);

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
    /// <returns>Tuple containing arrays of connected object names by type</returns>
    (string[] Frames, string[] Areas, string[] Links) GetConnectedObjects(string pointName);

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
}