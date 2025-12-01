using EtabSharp.Elements.PointObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.PointObj;

/// <summary>
/// PointObjectManager partial class - Properties and Assignments Methods
/// </summary>
public partial class PointObjectManager
{
    #region Diaphragm Methods

    /// <summary>
    /// Assigns a point to a diaphragm (rigid floor constraint).
    /// Wraps cSapModel.PointObj.SetDiaphragm.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="diaphragmOption"></param>
    /// <param name="diaphragmName">Name of the diaphragm (empty string to remove assignment)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetDiaphragm(string pointName, eDiaphragmOption diaphragmOption, string diaphragmName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));


            int ret = _sapModel.PointObj.SetDiaphragm(pointName, diaphragmOption, diaphragmName ?? "");

            if (ret != 0)
                throw new EtabsException(ret, "SetDiaphragm", $"Failed to set diaphragm for point '{pointName}'");

            _logger.LogDebug("Set diaphragm for point {PointName}: {DiaphragmName}", pointName, diaphragmName ?? "None");

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting diaphragm for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the diaphragm assignment for a point object.
    /// Wraps cSapModel.PointObj.GetDiaphragm.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Name of assigned diaphragm (empty if none)</returns>
    public string GetDiaphragm(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            eDiaphragmOption diaphragmOption = default;
            string diaphragmName = "";

            int ret = _sapModel.PointObj.GetDiaphragm(pointName, ref diaphragmOption, ref diaphragmName);

            if (ret != 0)
                throw new EtabsException(ret, "GetDiaphragm", $"Failed to get diaphragm for point '{pointName}'");

            return diaphragmOption == 0 ? "" : diaphragmName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting diaphragm for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Mass Methods

    /// <summary>
    /// Assigns lumped mass to a point object.
    /// Wraps cSapModel.PointObj.SetMass.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="mass">PointMass model with mass values</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetMass(string pointName, PointMass mass, bool replace = true)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (mass == null)
                throw new ArgumentNullException(nameof(mass));

            double[] massArray = mass.ToArray();
            int ret = _sapModel.PointObj.SetMass(pointName, ref massArray, eItemType.Objects, mass.IsLocalCSys, replace);

            if (ret != 0)
                throw new EtabsException(ret, "SetMass", $"Failed to set mass for point '{pointName}'");

            _logger.LogDebug("Set mass for point {PointName}: {Mass}", pointName, mass.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting mass for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the lumped mass assigned to a point object.
    /// Wraps cSapModel.PointObj.GetMass.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointMass model with mass values, or null if no mass assigned</returns>
    public PointMass? GetMass(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            double[] massArray = new double[6];
            int ret = _sapModel.PointObj.GetMass(pointName, ref massArray);

            if (ret != 0)
                throw new EtabsException(ret, "GetMass", $"Failed to get mass for point '{pointName}'");

            // Check if any mass values are assigned
            if (massArray.All(m => Math.Abs(m) < 1e-10))
                return null; // No mass assigned

            var mass = PointMass.FromArray(massArray);
            _logger.LogDebug("Retrieved mass for point {PointName}: {Mass}", pointName, mass.ToString());

            return mass;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting mass for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes mass assignment from a point object.
    /// Wraps cSapModel.PointObj.DeleteMass.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteMass(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int ret = _sapModel.PointObj.DeleteMass(pointName);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteMass", $"Failed to delete mass for point '{pointName}'");

            _logger.LogDebug("Deleted mass for point {PointName}", pointName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting mass for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Connectivity Methods

    /// <summary>
    /// Gets all frame, area, and other objects connected to a point.
    /// Wraps cSapModel.PointObj.GetConnectivity.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointConnectivity model with connected object information</returns>
    public PointConnectivity GetConnectedObjects(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int numberItems = 0;
            int[] objectType = null;
            string[] objectName = null;
            int[] pointNumber = null;

            int ret = _sapModel.PointObj.GetConnectivity(pointName, ref numberItems, ref objectType, ref objectName, ref pointNumber);

            if (ret != 0)
                throw new EtabsException(ret, "GetConnectivity", $"Failed to get connectivity for point '{pointName}'");

            var connectivity = new PointConnectivity
            {
                PointName = pointName
            };

            for (int i = 0; i < numberItems; i++)
            {
                switch (objectType[i])
                {
                    case 2: // Frame object
                        connectivity.ConnectedFrames.Add(objectName[i]);
                        break;
                    case 5: // Area object
                        connectivity.ConnectedAreas.Add(objectName[i]);
                        break;
                    case 8: // Link object
                        connectivity.ConnectedLinks.Add(objectName[i]);
                        break;
                    case 9: // Cable object
                        connectivity.ConnectedCables.Add(objectName[i]);
                        break;
                    case 10: // Tendon object
                        connectivity.ConnectedTendons.Add(objectName[i]);
                        break;
                }
            }

            _logger.LogDebug("Retrieved connectivity for point {PointName}: {Connectivity}", pointName, connectivity.ToString());

            return connectivity;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting connectivity for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Checks if a point is connected to any structural elements.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>True if connected to any element, false otherwise</returns>
    public bool IsConnected(string pointName)
    {
        try
        {
            var connectivity = GetConnectedObjects(pointName);
            return connectivity.HasConnections;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error checking connectivity for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Special Point Methods

    /// <summary>
    /// Marks a point as a "special point" that can exist without connections.
    /// Wraps cSapModel.PointObj.SetSpecialPoint.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="isSpecial">True to mark as special, false to unmark</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSpecialPoint(string pointName, bool isSpecial)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int ret = _sapModel.PointObj.SetSpecialPoint(pointName, isSpecial);

            if (ret != 0)
                throw new EtabsException(ret, "SetSpecialPoint", $"Failed to set special point status for point '{pointName}'");

            _logger.LogDebug("Set special point status for point {PointName}: {IsSpecial}", pointName, isSpecial);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting special point status for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Checks if a point is marked as special.
    /// Wraps cSapModel.PointObj.GetSpecialPoint.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>True if special point, false otherwise</returns>
    public bool IsSpecialPoint(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            bool isSpecial = false;
            int ret = _sapModel.PointObj.GetSpecialPoint(pointName, ref isSpecial);

            if (ret != 0)
                throw new EtabsException(ret, "GetSpecialPoint", $"Failed to get special point status for point '{pointName}'");

            return isSpecial;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting special point status for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes all special points that have no connected elements.
    /// Wraps cSapModel.PointObj.DeleteSpecialPoint for all unconnected special points.
    /// </summary>
    /// <returns>Number of points deleted</returns>
    public int DeleteUnconnectedSpecialPoints()
    {
        try
        {
            var allPoints = GetNameList();
            int deletedCount = 0;

            foreach (var pointName in allPoints)
            {
                if (IsSpecialPoint(pointName) && !IsConnected(pointName))
                {
                    int ret = _sapModel.PointObj.DeleteSpecialPoint(pointName);
                    if (ret == 0)
                    {
                        deletedCount++;
                        _logger.LogDebug("Deleted unconnected special point {PointName}", pointName);
                    }
                }
            }

            _logger.LogDebug("Deleted {Count} unconnected special points", deletedCount);
            return deletedCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting unconnected special points: {ex.Message}", ex);
        }
    }

    #endregion

    #region Group Assignment Methods

    /// <summary>
    /// Assigns a point object to a group.
    /// Wraps cSapModel.PointObj.SetGroupAssign.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetGroupAssignment(string pointName, string groupName, bool remove = false)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("Group name cannot be null or empty", nameof(groupName));

            int ret = _sapModel.PointObj.SetGroupAssign(pointName, groupName, remove);

            if (ret != 0)
                throw new EtabsException(ret, "SetGroupAssign", $"Failed to {(remove ? "remove" : "assign")} point '{pointName}' {(remove ? "from" : "to")} group '{groupName}'");

            _logger.LogDebug("{Action} point {PointName} {Preposition} group {GroupName}",
                remove ? "Removed" : "Assigned", pointName, remove ? "from" : "to", groupName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting group assignment for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all groups that a point object is assigned to.
    /// Wraps cSapModel.PointObj.GetGroupAssign.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Array of group names</returns>
    public string[] GetGroupAssignment(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int numberGroups = 0;
            string[] groups = null;

            int ret = _sapModel.PointObj.GetGroupAssign(pointName, ref numberGroups, ref groups);

            if (ret != 0)
                throw new EtabsException(ret, "GetGroupAssign", $"Failed to get group assignments for point '{pointName}'");

            _logger.LogDebug("Retrieved {Count} group assignments for point {PointName}", numberGroups, pointName);

            return groups ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting group assignments for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Selection Methods

    /// <summary>
    /// Sets the selection state of a point object.
    /// Wraps cSapModel.PointObj.SetSelected.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSelected(string pointName, bool selected)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int ret = _sapModel.PointObj.SetSelected(pointName, selected);

            if (ret != 0)
                throw new EtabsException(ret, "SetSelected", $"Failed to set selection state for point '{pointName}'");

            _logger.LogDebug("Set selection state for point {PointName}: {Selected}", pointName, selected);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting selection state for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Checks if a point object is currently selected.
    /// Wraps cSapModel.PointObj.GetSelected.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>True if selected, false otherwise</returns>
    public bool IsSelected(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            bool selected = false;
            int ret = _sapModel.PointObj.GetSelected(pointName, ref selected);

            if (ret != 0)
                throw new EtabsException(ret, "GetSelected", $"Failed to get selection state for point '{pointName}'");

            return selected;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting selection state for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Label and Story Methods

    /// <summary>
    /// Gets points by story level.
    /// Wraps cSapModel.PointObj.GetNameListOnStory.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Array of point names on the specified story</returns>
    public string[] GetPointsOnStory(string storyName)
    {
        try
        {
            if (string.IsNullOrEmpty(storyName))
                throw new ArgumentException("Story name cannot be null or empty", nameof(storyName));

            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.PointObj.GetNameListOnStory(storyName, ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameListOnStory", $"Failed to get points on story '{storyName}'");

            _logger.LogDebug("Retrieved {Count} points on story {StoryName}", numberNames, storyName);

            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting points on story '{storyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets label and story information for all points.
    /// Wraps cSapModel.PointObj.GetLabelNameList.
    /// </summary>
    /// <returns>Tuple of (Names, Labels, Stories)</returns>
    public (string[] Names, string[] Labels, string[] Stories) GetLabelNameList()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;
            string[] labels = null;
            string[] stories = null;

            int ret = _sapModel.PointObj.GetLabelNameList(ref numberNames, ref names, ref labels, ref stories);

            if (ret != 0)
                throw new EtabsException(ret, "GetLabelNameList", "Failed to get label name list");

            _logger.LogDebug("Retrieved label information for {Count} points", numberNames);

            return (names ?? new string[0], labels ?? new string[0], stories ?? new string[0]);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting label name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the unique name from label and story.
    /// Wraps cSapModel.PointObj.GetNameFromLabel.
    /// </summary>
    /// <param name="label">Point label</param>
    /// <param name="story">Story name</param>
    /// <returns>Unique point name</returns>
    public string GetNameFromLabel(string label, string story)
    {
        try
        {
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("Label cannot be null or empty", nameof(label));
            if (string.IsNullOrEmpty(story))
                throw new ArgumentException("Story cannot be null or empty", nameof(story));

            string name = "";
            int ret = _sapModel.PointObj.GetNameFromLabel(label, story, ref name);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameFromLabel", $"Failed to get name from label '{label}' on story '{story}'");

            return name;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting name from label '{label}' on story '{story}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Sets uniform mass in all translational directions at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="mass">Mass value for all translational directions</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetUniformMass(string pointName, double mass, bool replace = true)
    {
        var pointMass = new PointMass
        {
            Mx = mass,
            My = mass,
            Mz = mass
        };

        return SetMass(pointName, pointMass, replace);
    }

    /// <summary>
    /// Gets a complete point object with all properties populated.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Complete Point model with all properties</returns>
    public Point GetCompletePoint(string pointName)
    {
        try
        {
            var point = GetPoint(pointName);

            // Add restraint information
            var restraint = GetRestraint(pointName);
            if (restraint != null)
            {
                point.Restraint = restraint;
            }

            // Add spring information
            var spring = GetSpring(pointName);
            if (spring != null)
            {
                point.Spring = spring;
            }

            // Add mass information
            var mass = GetMass(pointName);
            if (mass != null)
            {
                point.Mass = mass;
            }

            // Add diaphragm information
            var diaphragmName = GetDiaphragm(pointName);
            if (!string.IsNullOrEmpty(diaphragmName))
            {
                point.DiaphragmName = diaphragmName;
            }

            // Add group assignments
            var groups = GetGroupAssignment(pointName);
            point.Groups.AddRange(groups);

            return point;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting complete point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion
}