using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.FrameObj;

/// <summary>
/// FrameObjectManager partial class - Advanced Methods
/// Contains transformation matrices, hinges, supports, curved frames, and other advanced features.
/// </summary>
public partial class FrameObjectManager
{
    #region Transformation Matrix

    /// <summary>
    /// Gets the transformation matrix for a frame object.
    /// Wraps cSapModel.FrameObj.GetTransformationMatrix.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="isGlobal">If true, returns global-to-local transformation; if false, returns local-to-global</param>
    /// <returns>Transformation matrix as double array</returns>
    public double[] GetTransformationMatrix(string frameName, bool isGlobal = true)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            double[] matrix = new double[9]; // 3x3 matrix
            int ret = _sapModel.FrameObj.GetTransformationMatrix(frameName, ref matrix, isGlobal);

            if (ret != 0)
                throw new EtabsException(ret, "GetTransformationMatrix", $"Failed to get transformation matrix for frame '{frameName}'");

            _logger.LogDebug("Retrieved transformation matrix for frame {FrameName} (IsGlobal={IsGlobal})", 
                frameName, isGlobal);

            return matrix;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting transformation matrix for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Hinge Assignments

    /// <summary>
    /// Gets plastic hinge assignments for a frame object.
    /// Wraps cSapModel.FrameObj.GetHingeAssigns_1.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>List of FrameHinge models with hinge assignments</returns>
    public List<FrameHinge> GetHingeAssignments(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int numberHinges = 0;
            int[] hingeNum = null;
            string[] prop = null;
            int[] myType = null;
            int[] behavior = null;
            string[] source = null;
            eHingeLocationType[] locType = null;
            double[] rd = null;
            double[] ad = null;

            int ret = _sapModel.FrameObj.GetHingeAssigns_1(frameName, ref numberHinges, ref hingeNum, 
                ref prop, ref myType, ref behavior, ref source, ref locType, ref rd, ref ad);

            if (ret != 0)
                throw new EtabsException(ret, "GetHingeAssigns_1", $"Failed to get hinge assignments for frame '{frameName}'");

            var hinges = new List<FrameHinge>();
            for (int i = 0; i < numberHinges; i++)
            {
                hinges.Add(new FrameHinge
                {
                    FrameName = frameName,
                    HingeNumber = hingeNum[i],
                    PropertyName = prop[i],
                    HingeType = myType[i],
                    Behavior = behavior[i],
                    Source = source[i],
                    LocationType = locType[i],
                    RelativeDistance = rd[i],
                    AbsoluteDistance = ad[i]
                });
            }

            _logger.LogDebug("Retrieved {Count} hinge assignments for frame {FrameName}", numberHinges, frameName);
            return hinges;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting hinge assignments for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Support Information

    /// <summary>
    /// Gets the support objects (columns, beams, or walls) at the ends of a beam frame object.
    /// Wraps cSapModel.FrameObj.GetSupports.
    /// </summary>
    /// <param name="frameName">Name of the beam frame object</param>
    /// /// <returns>Tuple of (SupportName1, SupportType1, SupportName2, SupportType2)</returns>
    public (string SupportName1, eObjType SupportType1, string SupportName2, eObjType SupportType2) GetSupports(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string supportName1 = "";
            eObjType supportType1 = eObjType.Point;
            string supportName2 = "";
            eObjType supportType2 = eObjType.Point;

            int ret = _sapModel.FrameObj.GetSupports(frameName, ref supportName1, ref supportType1, 
                ref supportName2, ref supportType2);

            if (ret != 0)
                throw new EtabsException(ret, "GetSupports", $"Failed to get supports for frame '{frameName}'");

            _logger.LogDebug("Retrieved supports for frame {FrameName}: {Support1} ({Type1}), {Support2} ({Type2})", 
                frameName, supportName1, supportType1, supportName2, supportType2);

            return (supportName1, supportType1, supportName2, supportType2);
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting supports for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Curved Frame Methods

    /// <summary>
    /// Gets the frame type (straight or curved).
    /// Wraps cSapModel.FrameObj.GetTypeOAPI.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Frame type string</returns>
    public string GetFrameType(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string frameType = "";
            int ret = _sapModel.FrameObj.GetTypeOAPI(frameName, ref frameType);

            if (ret != 0)
                throw new EtabsException(ret, "GetTypeOAPI", $"Failed to get frame type for '{frameName}'");

            _logger.LogDebug("Retrieved frame type for {FrameName}: {FrameType}", frameName, frameType);
            return frameType;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting frame type for '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets curve data for curved frame objects.
    /// Wraps cSapModel.FrameObj.GetCurved_2.
    /// </summary>
    /// <param name="frameName">Name of the curved frame object</param>
    /// <returns>FrameCurveData model with curve information</returns>
    public FrameCurveData GetCurveData(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int curveType = 0;
            double tension = 0;
            int numPoints = 0;
            double[] gx = null;
            double[] gy = null;
            double[] gz = null;

            int ret = _sapModel.FrameObj.GetCurved_2(frameName, ref curveType, ref tension, 
                ref numPoints, ref gx, ref gy, ref gz);

            if (ret != 0)
                throw new EtabsException(ret, "GetCurved_2", $"Failed to get curve data for frame '{frameName}'");

            var curveData = new FrameCurveData
            {
                FrameName = frameName,
                CurveType = (FrameCurveType)curveType,
                Tension = tension,
                NumberOfPoints = numPoints,
                GlobalX = gx ?? Array.Empty<double>(),
                GlobalY = gy ?? Array.Empty<double>(),
                GlobalZ = gz ?? Array.Empty<double>()
            };

            _logger.LogDebug("Retrieved curve data for frame {FrameName}: Type={CurveType}, Points={NumPoints}", 
                frameName, curveData.CurveType, numPoints);

            return curveData;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting curve data for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Mass Assignment

    /// <summary>
    /// Assigns additional mass per unit length to a frame object.
    /// Wraps cSapModel.FrameObj.SetMass.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="massPerLength">Additional mass per unit length</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetMass(string frameName, double massPerLength, bool replace = false, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetMass(frameName, massPerLength, replace, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetMass", $"Failed to set mass for frame '{frameName}'");

            _logger.LogDebug("Set mass for frame {FrameName}: {MassPerLength} (Replace={Replace})", 
                frameName, massPerLength, replace);

            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error setting mass for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the additional mass per length assigned to a frame.
    /// Wraps cSapModel.FrameObj.GetMass.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Mass per unit length</returns>
    public double GetMass(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            double massOverL = 0;
            int ret = _sapModel.FrameObj.GetMass(frameName, ref massOverL);

            if (ret != 0)
                throw new EtabsException(ret, "GetMass", $"Failed to get mass for frame '{frameName}'");

            _logger.LogDebug("Retrieved mass for frame {FrameName}: {MassPerLength}", frameName, massOverL);
            return massOverL;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting mass for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes mass assignment from a frame.
    /// Wraps cSapModel.FrameObj.DeleteMass.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteMass(string frameName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.DeleteMass(frameName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteMass", $"Failed to delete mass for frame '{frameName}'");

            _logger.LogDebug("Deleted mass for frame {FrameName}", frameName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error deleting mass for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Spring Assignment

    /// <summary>
    /// Assigns a named line spring property to a frame object.
    /// Wraps cSapModel.FrameObj.SetSpringAssignment.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="springPropertyName">Name of the spring property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSpringAssignment(string frameName, string springPropertyName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (string.IsNullOrEmpty(springPropertyName))
                throw new ArgumentException("Spring property name cannot be null or empty", nameof(springPropertyName));

            int ret = _sapModel.FrameObj.SetSpringAssignment(frameName, springPropertyName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetSpringAssignment", $"Failed to set spring assignment for frame '{frameName}'");

            _logger.LogDebug("Set spring assignment for frame {FrameName}: {SpringProperty}", frameName, springPropertyName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error setting spring assignment for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the spring property assignment for a frame object.
    /// Wraps cSapModel.FrameObj.GetSpringAssignment.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Name of the assigned spring property</returns>
    public string GetSpringAssignment(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string springProp = "";
            int ret = _sapModel.FrameObj.GetSpringAssignment(frameName, ref springProp);

            if (ret != 0)
                throw new EtabsException(ret, "GetSpringAssignment", $"Failed to get spring assignment for frame '{frameName}'");

            _logger.LogDebug("Retrieved spring assignment for frame {FrameName}: {SpringProperty}", frameName, springProp);
            return springProp;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting spring assignment for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes spring assignment from a frame.
    /// Wraps cSapModel.FrameObj.DeleteSpring.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteSpring(string frameName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.DeleteSpring(frameName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteSpring", $"Failed to delete spring for frame '{frameName}'");

            _logger.LogDebug("Deleted spring for frame {FrameName}", frameName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error deleting spring for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Group Assignment

    /// <summary>
    /// Assigns a frame object to a group.
    /// Wraps cSapModel.FrameObj.SetGroupAssign.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetGroupAssignment(string frameName, string groupName, bool remove = false, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("Group name cannot be null or empty", nameof(groupName));

            int ret = _sapModel.FrameObj.SetGroupAssign(frameName, groupName, remove, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetGroupAssign", $"Failed to {(remove ? "remove" : "assign")} frame '{frameName}' {(remove ? "from" : "to")} group '{groupName}'");

            _logger.LogDebug("{Action} frame {FrameName} {Preposition} group {GroupName}", 
                remove ? "Removed" : "Assigned", frameName, remove ? "from" : "to", groupName);

            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error setting group assignment for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all groups that a frame object is assigned to.
    /// Wraps cSapModel.FrameObj.GetGroupAssign.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Array of group names</returns>
    public string[] GetGroupAssignment(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int numberGroups = 0;
            string[] groups = null;

            int ret = _sapModel.FrameObj.GetGroupAssign(frameName, ref numberGroups, ref groups);

            if (ret != 0)
                throw new EtabsException(ret, "GetGroupAssign", $"Failed to get group assignments for frame '{frameName}'");

            _logger.LogDebug("Retrieved {Count} group assignments for frame {FrameName}", numberGroups, frameName);
            return groups ?? Array.Empty<string>();
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting group assignments for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Selection State

    /// <summary>
    /// Sets the selection state of a frame object.
    /// Wraps cSapModel.FrameObj.SetSelected.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <param name="itemType">Item type for selection</param>
    /// /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSelected(string frameName, bool selected, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be nur empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetSelected(frameName, selected, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetSelected", $"Failed to set selection state for frame '{frameName}'");

            _logger.LogDebug("Set selection state for frame {FrameName}: {Selected}", frameName, selected);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error setting selection state for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Checks if a frame object is currently selected.
    /// Wraps cSapModel.FrameObj.GetSelected.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>True if selected, false otherwise</returns>
    public bool IsSelected(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            bool selected = false;
            int ret = _sapModel.FrameObj.GetSelected(frameName, ref selected);

            if (ret != 0)
                throw new EtabsException(ret, "GetSelected", $"Failed to get selection state for frame '{frameName}'");

            return selected;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting selection state for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Label and Story Methods

    /// <summary>
    /// Gets all frame names with their labels and stories.
    /// Wraps cSapModel.FrameObj.GetLabelNameList.
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

            int ret = _sapModel.FrameObj.GetLabelNameList(ref numberNames, ref names, ref labels, ref stories);

            if (ret != 0)
                throw new EtabsException(ret, "GetLabelNameList", "Failed to get label name list");

            _logger.LogDebug("Retrieved label information for {Count} frames", numberNames);
            return (names ?? Array.Empty<string>(), labels ?? Array.Empty<string>(), stories ?? Array.Empty<string>());
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting label name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the label and story level for a frame object.
    /// Wraps cSapModel.FrameObj.GetLabelFromName.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (Label, Story)</returns>
    public (string Label, string Story) GetLabelFromName(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string label = "";
            string story = "";

            int ret = _sapModel.FrameObj.GetLabelFromName(frameName, ref label, ref story);

            if (ret != 0)
                throw new EtabsException(ret, "GetLabelFromName", $"Failed to get label for frame '{frameName}'");

            return (label, story);
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting label for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the unique name of a frame object from its label and story.
    /// Wraps cSapModel.FrameObj.GetNameFromLabel.
    /// </summary>
    /// <param name="label">Frame label</param>
    /// <param name="story">Story name</param>
    /// <returns>Unique frame name</returns>
    public string GetNameFromLabel(string label, string story)
    {
        try
        {
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("Label cannot be null or empty", nameof(label));
            if (string.IsNullOrEmpty(story))
                throw new ArgumentException("Story cannot be null or empty", nameof(story));

            string name = "";
            int ret = _sapModel.FrameObj.GetNameFromLabel(label, story, ref name);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameFromLabel", $"Failed to get name from label '{label}' on story '{story}'");

            return name;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting name from label '{label}' on story '{story}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all frame names on a specific story.
    /// /// Wraps cSapModel.FrameObj.GetNameListOnStory.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Array of frame names on the specified story</returns>
    public string[] GetFramesOnStory(string storyName)
    {
        try
        {
            if (string.IsNullOrEmpty(storyName))
                throw new ArgumentException("Story name cannot be null or empty", nameof(storyName));

            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.FrameObj.GetNameListOnStory(storyName, ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameListOnStory", $"Failed to get frames on story '{storyName}'");

            _logger.LogDebug("Retrieved {Count} frames on story {StoryName}", numberNames, storyName);
            return names ?? Array.Empty<string>();
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            throw new EtabsException($"Unexpected error getting frames on story '{storyName}': {ex.Message}", ex);
        }
    }

    #endregion
}
