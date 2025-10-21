using EtabSharp.Elements.AreaObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;

namespace EtabSharp.Elements.AreaObj;

/// <summary>
/// AreaObjectManager partial class - Selection, Group, and Label Methods
/// </summary>
public partial class AreaObjectManager
{
    #region Selection Methods

    /// <summary>
    /// Sets the selection state of an area object.
    /// Wraps cSapModel.AreaObj.SetSelected.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <param name="itemType">Item type for selection</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSelected(string areaName, bool selected, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.SetSelected(areaName, selected, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetSelected", $"Failed to set selection state for area '{areaName}'");

            _logger.LogDebug("Set selection state for area {AreaName}: {Selected}", areaName, selected);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting selection state for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the selection state of an area object.
    /// Wraps cSapModel.AreaObj.GetSelected.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if selected, false otherwise</returns>
    public bool IsSelected(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            bool selected = false;
            int ret = _sapModel.AreaObj.GetSelected(areaName, ref selected);

            if (ret != 0)
                throw new EtabsException(ret, "GetSelected", $"Failed to get selection state for area '{areaName}'");

            return selected;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting selection state for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets edge selection for an area object.
    /// Wraps cSapModel.AreaObj.SetSelectedEdge.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="edgeNumber">Edge number to select</param>
    /// <param name="selected">True to select, false to deselect</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSelectedEdge(string areaName, int edgeNumber, bool selected)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (edgeNumber < 1)
                throw new ArgumentException("Edge number must be greater than 0", nameof(edgeNumber));

            int ret = _sapModel.AreaObj.SetSelectedEdge(areaName, edgeNumber, selected);

            if (ret != 0)
                throw new EtabsException(ret, "SetSelectedEdge", $"Failed to set edge selection for area '{areaName}' edge {edgeNumber}");

            _logger.LogDebug("Set edge {EdgeNumber} selection for area {AreaName}: {Selected}", edgeNumber, areaName, selected);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting edge selection for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets edge selection for an area object.
    /// Wraps cSapModel.AreaObj.GetSelectedEdge.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of edge selection states</returns>
    public bool[] GetSelectedEdges(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberEdges = 0;
            bool[] selected = null;

            int ret = _sapModel.AreaObj.GetSelectedEdge(areaName, ref numberEdges, ref selected);

            if (ret != 0)
                throw new EtabsException(ret, "GetSelectedEdge", $"Failed to get edge selection for area '{areaName}'");

            return selected ?? new bool[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting edge selection for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Group Assignment Methods

    /// <summary>
    /// Assigns an area object to a group.
    /// Wraps cSapModel.AreaObj.SetGroupAssign.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetGroupAssignment(string areaName, string groupName, bool remove = false, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("Group name cannot be null or empty", nameof(groupName));

            int ret = _sapModel.AreaObj.SetGroupAssign(areaName, groupName, remove, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetGroupAssign", $"Failed to {(remove ? "remove" : "assign")} area '{areaName}' {(remove ? "from" : "to")} group '{groupName}'");

            _logger.LogDebug("{Action} area {AreaName} {Preposition} group {GroupName}", 
                remove ? "Removed" : "Assigned", areaName, remove ? "from" : "to", groupName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting group assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets group assignments for an area object.
    /// Wraps cSapModel.AreaObj.GetGroupAssign.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of group names</returns>
    public string[] GetGroupAssignment(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberGroups = 0;
            string[] groups = null;

            int ret = _sapModel.AreaObj.GetGroupAssign(areaName, ref numberGroups, ref groups);

            if (ret != 0)
                throw new EtabsException(ret, "GetGroupAssign", $"Failed to get group assignments for area '{areaName}'");

            _logger.LogDebug("Retrieved {Count} group assignments for area {AreaName}", numberGroups, areaName);

            return groups ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting group assignments for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Label and Story Methods

    /// <summary>
    /// Gets areas by story level.
    /// Wraps cSapModel.AreaObj.GetNameListOnStory.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Array of area names on the specified story</returns>
    public string[] GetAreasOnStory(string storyName)
    {
        try
        {
            if (string.IsNullOrEmpty(storyName))
                throw new ArgumentException("Story name cannot be null or empty", nameof(storyName));

            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.AreaObj.GetNameListOnStory(storyName, ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameListOnStory", $"Failed to get areas on story '{storyName}'");

            _logger.LogDebug("Retrieved {Count} areas on story {StoryName}", numberNames, storyName);

            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting areas on story '{storyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets label and story information for all areas.
    /// Wraps cSapModel.AreaObj.GetLabelNameList.
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

            int ret = _sapModel.AreaObj.GetLabelNameList(ref numberNames, ref names, ref labels, ref stories);

            if (ret != 0)
                throw new EtabsException(ret, "GetLabelNameList", "Failed to get label name list");

            _logger.LogDebug("Retrieved label information for {Count} areas", numberNames);

            return (names ?? new string[0], labels ?? new string[0], stories ?? new string[0]);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting label name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets label and story from area name.
    /// Wraps cSapModel.AreaObj.GetLabelFromName.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Tuple of (Label, Story)</returns>
    public (string Label, string Story) GetLabelFromName(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            string label = "", story = "";
            int ret = _sapModel.AreaObj.GetLabelFromName(areaName, ref label, ref story);

            if (ret != 0)
                throw new EtabsException(ret, "GetLabelFromName", $"Failed to get label from name for area '{areaName}'");

            return (label, story);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting label from name for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets area name from label and story.
    /// Wraps cSapModel.AreaObj.GetNameFromLabel.
    /// </summary>
    /// <param name="label">Area label</param>
    /// <param name="story">Story name</param>
    /// <returns>Area name</returns>
    public string GetNameFromLabel(string label, string story)
    {
        try
        {
            if (string.IsNullOrEmpty(label))
                throw new ArgumentException("Label cannot be null or empty", nameof(label));
            if (string.IsNullOrEmpty(story))
                throw new ArgumentException("Story cannot be null or empty", nameof(story));

            string name = "";
            int ret = _sapModel.AreaObj.GetNameFromLabel(label, story, ref name);

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
    /// Selects all areas in a group.
    /// </summary>
    /// <param name="groupName">Name of the group</param>
    /// <returns>Number of areas selected</returns>
    public int SelectAreasInGroup(string groupName)
    {
        try
        {
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("Group name cannot be null or empty", nameof(groupName));

            var allAreas = GetNameList();
            int selectedCount = 0;

            foreach (var areaName in allAreas)
            {
                var groups = GetGroupAssignment(areaName);
                if (groups.Contains(groupName))
                {
                    SetSelected(areaName, true);
                    selectedCount++;
                }
            }

            _logger.LogDebug("Selected {Count} areas in group {GroupName}", selectedCount, groupName);
            return selectedCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error selecting areas in group '{groupName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Selects all areas on a story.
    /// </summary>
    /// <param name="storyName">Name of the story</param>
    /// <returns>Number of areas selected</returns>
    public int SelectAreasOnStory(string storyName)
    {
        try
        {
            var areasOnStory = GetAreasOnStory(storyName);
            
            foreach (var areaName in areasOnStory)
            {
                SetSelected(areaName, true);
            }

            _logger.LogDebug("Selected {Count} areas on story {StoryName}", areasOnStory.Length, storyName);
            return areasOnStory.Length;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error selecting areas on story '{storyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deselects all areas in the model.
    /// </summary>
    /// <returns>Number of areas deselected</returns>
    public int DeselectAllAreas()
    {
        try
        {
            var allAreas = GetNameList();
            int deselectedCount = 0;

            foreach (var areaName in allAreas)
            {
                if (IsSelected(areaName))
                {
                    SetSelected(areaName, false);
                    deselectedCount++;
                }
            }

            _logger.LogDebug("Deselected {Count} areas", deselectedCount);
            return deselectedCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deselecting all areas: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all currently selected areas.
    /// </summary>
    /// <returns>Array of selected area names</returns>
    public string[] GetSelectedAreas()
    {
        try
        {
            var allAreas = GetNameList();
            var selectedAreas = new List<string>();

            foreach (var areaName in allAreas)
            {
                if (IsSelected(areaName))
                {
                    selectedAreas.Add(areaName);
                }
            }

            _logger.LogDebug("Found {Count} selected areas", selectedAreas.Count);
            return selectedAreas.ToArray();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting selected areas: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Assigns multiple areas to a group.
    /// </summary>
    /// <param name="areaNames">Array of area names</param>
    /// <param name="groupName">Name of the group</param>
    /// <param name="remove">If true, removes from group; if false, adds to group</param>
    /// <returns>Number of areas successfully assigned/removed</returns>
    public int SetGroupAssignmentMultiple(string[] areaNames, string groupName, bool remove = false)
    {
        try
        {
            if (areaNames == null || areaNames.Length == 0)
                throw new ArgumentException("Area names array cannot be null or empty", nameof(areaNames));
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("Group name cannot be null or empty", nameof(groupName));

            int successCount = 0;

            foreach (var areaName in areaNames)
            {
                try
                {
                    int ret = SetGroupAssignment(areaName, groupName, remove);
                    if (ret == 0)
                        successCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to {Action} area {AreaName} {Preposition} group {GroupName}: {Error}", 
                        remove ? "remove" : "assign", areaName, remove ? "from" : "to", groupName, ex.Message);
                }
            }

            _logger.LogDebug("{Action} {SuccessCount}/{TotalCount} areas {Preposition} group {GroupName}", 
                remove ? "Removed" : "Assigned", successCount, areaNames.Length, remove ? "from" : "to", groupName);

            return successCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting group assignment for multiple areas: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a complete area object with all properties populated.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Complete Area model with all properties</returns>
    public Area GetCompleteArea(string areaName)
    {
        try
        {
            var area = GetArea(areaName);
            
            // Add additional properties
            area.IsOpening = GetOpening(areaName);
            area.HasEdgeConstraints = GetEdgeConstraint(areaName);
            area.PierName = GetPier(areaName);
            area.SpandrelName = GetSpandrel(areaName);
            area.DiaphragmName = GetDiaphragm(areaName);
            area.SpringPropertyName = GetSpringAssignment(areaName);
            area.MassPerArea = GetMass(areaName);
            area.Modifiers = GetModifiers(areaName);
            area.Groups.AddRange(GetGroupAssignment(areaName));
            area.IsSelected = IsSelected(areaName);
            area.GUID = GetGUID(areaName);
            area.Loads = GetAllLoads(areaName);
            area.Offsets = GetOffsets(areaName);
            area.CurvedEdges = GetCurvedEdges(areaName);

            return area;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting complete area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion
}