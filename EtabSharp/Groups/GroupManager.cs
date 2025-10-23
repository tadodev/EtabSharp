using EtabSharp.Exceptions;
using EtabSharp.Groups.Models;
using EtabSharp.Interfaces.Group;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Groups;

/// <summary>
/// Manages groups in the ETABS model.
/// Implements the IGroup interface by wrapping cSapModel.GroupDef operations.
/// </summary>
public class GroupManager : IGroup
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the GroupManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public GroupManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Group Creation and Management

    /// <summary>
    /// Creates or modifies a group with specified properties.
    /// Wraps cSapModel.GroupDef.SetGroup_1 (most comprehensive version).
    /// </summary>
    public int SetGroup(Group group)
    {
        try
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
            if (string.IsNullOrEmpty(group.Name))
                throw new ArgumentException("Group name cannot be null or empty", nameof(group));

            int ret = _sapModel.GroupDef.SetGroup_1(
                group.Name,
                group.Color,
                group.SpecifiedForSelection,
                group.SpecifiedForSectionCutDefinition,
                group.SpecifiedForSteelDesign,
                group.SpecifiedForConcreteDesign,
                group.SpecifiedForAluminumDesign,
                group.SpecifiedForStaticNLActiveStage,
                group.SpecifiedForAutoSeismicOutput,
                group.SpecifiedForAutoWindOutput,
                group.SpecifiedForMassAndWeight,
                group.SpecifiedForSteelJoistDesign,
                group.SpecifiedForWallDesign,
                group.SpecifiedForBasePlateDesign,
                group.SpecifiedForConnectionDesign);

            if (ret != 0)
                throw new EtabsException(ret, "SetGroup_1", $"Failed to set group '{group.Name}'");

            _logger.LogDebug("Set group {GroupName} with {ObjectCount} objects", group.Name, group.ObjectCount);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting group '{group?.Name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Creates or modifies a group with basic properties.
    /// </summary>
    public int SetGroup(string name, int color = -1, bool specifiedForSelection = true, bool specifiedForDesign = true)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Group name cannot be null or empty", nameof(name));

            int ret = _sapModel.GroupDef.SetGroup_1(
                name,
                color,
                specifiedForSelection,
                true, // SectionCutDefinition
                specifiedForDesign, // SteelDesign
                specifiedForDesign, // ConcreteDesign
                specifiedForDesign, // AluminumDesign
                true, // StaticNLActiveStage
                false, // AutoSeismicOutput
                false, // AutoWindOutput
                true, // MassAndWeight
                specifiedForDesign, // SteelJoistDesign
                specifiedForDesign, // WallDesign
                specifiedForDesign, // BasePlateDesign
                specifiedForDesign); // ConnectionDesign

            if (ret != 0)
                throw new EtabsException(ret, "SetGroup_1", $"Failed to set group '{name}'");

            _logger.LogDebug("Set group {GroupName}", name);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting group '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a group with all its properties.
    /// Wraps cSapModel.GroupDef.GetGroup_1 (most comprehensive version).
    /// </summary>
    public Group GetGroup(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Group name cannot be null or empty", nameof(name));

            int color = 0;
            bool specifiedForSelection = false;
            bool specifiedForSectionCutDefinition = false;
            bool specifiedForSteelDesign = false;
            bool specifiedForConcreteDesign = false;
            bool specifiedForAluminumDesign = false;
            bool specifiedForStaticNLActiveStage = false;
            bool specifiedForAutoSeismicOutput = false;
            bool specifiedForAutoWindOutput = false;
            bool specifiedForMassAndWeight = false;
            bool specifiedForSteelJoistDesign = false;
            bool specifiedForWallDesign = false;
            bool specifiedForBasePlateDesign = false;
            bool specifiedForConnectionDesign = false;

            int ret = _sapModel.GroupDef.GetGroup_1(
                name,
                ref color,
                ref specifiedForSelection,
                ref specifiedForSectionCutDefinition,
                ref specifiedForSteelDesign,
                ref specifiedForConcreteDesign,
                ref specifiedForAluminumDesign,
                ref specifiedForStaticNLActiveStage,
                ref specifiedForAutoSeismicOutput,
                ref specifiedForAutoWindOutput,
                ref specifiedForMassAndWeight,
                ref specifiedForSteelJoistDesign,
                ref specifiedForWallDesign,
                ref specifiedForBasePlateDesign,
                ref specifiedForConnectionDesign);

            if (ret != 0)
                throw new EtabsException(ret, "GetGroup_1", $"Failed to get group '{name}'");

            var group = new Group
            {
                Name = name,
                Color = color,
                SpecifiedForSelection = specifiedForSelection,
                SpecifiedForSectionCutDefinition = specifiedForSectionCutDefinition,
                SpecifiedForSteelDesign = specifiedForSteelDesign,
                SpecifiedForConcreteDesign = specifiedForConcreteDesign,
                SpecifiedForAluminumDesign = specifiedForAluminumDesign,
                SpecifiedForStaticNLActiveStage = specifiedForStaticNLActiveStage,
                SpecifiedForAutoSeismicOutput = specifiedForAutoSeismicOutput,
                SpecifiedForAutoWindOutput = specifiedForAutoWindOutput,
                SpecifiedForMassAndWeight = specifiedForMassAndWeight,
                SpecifiedForSteelJoistDesign = specifiedForSteelJoistDesign,
                SpecifiedForWallDesign = specifiedForWallDesign,
                SpecifiedForBasePlateDesign = specifiedForBasePlateDesign,
                SpecifiedForConnectionDesign = specifiedForConnectionDesign
            };

            // Get assignments
            group.Assignments = GetAssignments(name);

            _logger.LogDebug("Retrieved group {GroupName} with {ObjectCount} objects", name, group.ObjectCount);

            return group;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting group '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes a group from the model.
    /// Wraps cSapModel.GroupDef.Delete.
    /// </summary>
    public int Delete(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Group name cannot be null or empty", nameof(name));

            int ret = _sapModel.GroupDef.Delete(name);

            if (ret != 0)
                throw new EtabsException(ret, "Delete", $"Failed to delete group '{name}'");

            _logger.LogDebug("Deleted group {GroupName}", name);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting group '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the names of all defined groups in the model.
    /// Wraps cSapModel.GroupDef.GetNameList.
    /// </summary>
    public string[] GetNameList()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.GroupDef.GetNameList(ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get group name list");

            _logger.LogDebug("Retrieved {Count} group names", numberNames);

            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting group name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of groups in the model.
    /// Wraps cSapModel.GroupDef.Count.
    /// </summary>
    public int Count()
    {
        try
        {
            int count = _sapModel.GroupDef.Count();
            _logger.LogDebug("Group count: {Count}", count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting group count: {ex.Message}", ex);
        }
    }

    #endregion

    #region Group Assignments

    /// <summary>
    /// Gets all object assignments for a group.
    /// Wraps cSapModel.GroupDef.GetAssignments.
    /// </summary>
    public List<GroupAssignment> GetAssignments(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Group name cannot be null or empty", nameof(name));

            int numberItems = 0;
            int[] objectType = null;
            string[] objectName = null;

            int ret = _sapModel.GroupDef.GetAssignments(name, ref numberItems, ref objectType, ref objectName);

            if (ret != 0)
                throw new EtabsException(ret, "GetAssignments", $"Failed to get assignments for group '{name}'");

            var assignments = new List<GroupAssignment>();

            for (int i = 0; i < numberItems; i++)
            {
                assignments.Add(new GroupAssignment
                {
                    ObjectType = objectType[i],
                    ObjectName = objectName[i]
                });
            }

            _logger.LogDebug("Retrieved {Count} assignments for group {GroupName}", numberItems, name);

            return assignments;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting assignments for group '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets objects of a specific type assigned to a group.
    /// </summary>
    public string[] GetAssignmentsByType(string name, GroupObjectType objectType)
    {
        try
        {
            var assignments = GetAssignments(name);
            return assignments
                .Where(a => a.ObjectType == (int)objectType)
                .Select(a => a.ObjectName)
                .ToArray();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting assignments by type for group '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of objects in a group.
    /// </summary>
    public int GetAssignmentCount(string name)
    {
        try
        {
            var assignments = GetAssignments(name);
            return assignments.Count;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting assignment count for group '{name}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Creates a default group with standard settings.
    /// </summary>
    public int CreateDefaultGroup(string name, int color = -1)
    {
        var group = Group.CreateDefault(name);
        group.Color = color;
        return SetGroup(group);
    }

    /// <summary>
    /// Creates a group specifically for design purposes.
    /// </summary>
    public int CreateDesignGroup(string name, int color = -1)
    {
        var group = Group.CreateForDesign(name);
        group.Color = color;
        return SetGroup(group);
    }

    /// <summary>
    /// Creates a group specifically for output purposes.
    /// </summary>
    public int CreateOutputGroup(string name, int color = -1)
    {
        var group = Group.CreateForOutput(name);
        group.Color = color;
        return SetGroup(group);
    }

    /// <summary>
    /// Creates a group specifically for selection purposes.
    /// </summary>
    public int CreateSelectionGroup(string name, int color = -1)
    {
        var group = Group.CreateForSelection(name);
        group.Color = color;
        return SetGroup(group);
    }

    /// <summary>
    /// Checks if a group exists in the model.
    /// </summary>
    public bool GroupExists(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var groupNames = GetNameList();
            return groupNames.Contains(name);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets all groups in the model with their properties.
    /// </summary>
    public List<Group> GetAllGroups()
    {
        try
        {
            var groupNames = GetNameList();
            var groups = new List<Group>();

            foreach (var groupName in groupNames)
            {
                try
                {
                    var group = GetGroup(groupName);
                    groups.Add(group);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to get group {GroupName}: {Error}", groupName, ex.Message);
                }
            }

            _logger.LogDebug("Retrieved {Count} groups", groups.Count);
            return groups;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all groups: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the color of a group using RGB components.
    /// </summary>
    public int SetGroupColor(string name, int red, int green, int blue)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Group name cannot be null or empty", nameof(name));

            // Validate RGB values
            if (red < 0 || red > 255)
                throw new ArgumentOutOfRangeException(nameof(red), "Red value must be between 0 and 255");
            if (green < 0 || green > 255)
                throw new ArgumentOutOfRangeException(nameof(green), "Green value must be between 0 and 255");
            if (blue < 0 || blue > 255)
                throw new ArgumentOutOfRangeException(nameof(blue), "Blue value must be between 0 and 255");

            int colorInt = GroupColorHelper.ToColorInt(red, green, blue);

            // Get existing group properties
            var group = GetGroup(name);
            group.Color = colorInt;

            return SetGroup(group);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting color for group '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the color of a group using a predefined color integer.
    /// </summary>
    public int SetGroupColor(string name, int colorInt)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Group name cannot be null or empty", nameof(name));

            // Get existing group properties
            var group = GetGroup(name);
            group.Color = colorInt;

            return SetGroup(group);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting color for group '{name}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets groups that contain a specific object.
    /// </summary>
    public string[] GetGroupsContainingObject(string objectName, GroupObjectType objectType)
    {
        try
        {
            if (string.IsNullOrEmpty(objectName))
                throw new ArgumentException("Object name cannot be null or empty", nameof(objectName));

            var groupNames = GetNameList();
            var containingGroups = new List<string>();

            foreach (var groupName in groupNames)
            {
                try
                {
                    var assignments = GetAssignments(groupName);
                    bool containsObject = assignments.Any(a =>
                        a.ObjectName == objectName && a.ObjectType == (int)objectType);

                    if (containsObject)
                    {
                        containingGroups.Add(groupName);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to check group {GroupName} for object {ObjectName}: {Error}",
                        groupName, objectName, ex.Message);
                }
            }

            _logger.LogDebug("Found {Count} groups containing object {ObjectName}", containingGroups.Count, objectName);
            return containingGroups.ToArray();
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting groups containing object '{objectName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets statistics about all groups in the model.
    /// </summary>
    public Dictionary<string, int> GetGroupStatistics()
    {
        try
        {
            var stats = new Dictionary<string, int>
            {
                ["TotalGroups"] = 0,
                ["EmptyGroups"] = 0,
                ["DesignGroups"] = 0,
                ["OutputGroups"] = 0,
                ["SelectionGroups"] = 0,
                ["TotalObjects"] = 0
            };

            var groups = GetAllGroups();
            stats["TotalGroups"] = groups.Count;

            foreach (var group in groups)
            {
                if (group.ObjectCount == 0)
                    stats["EmptyGroups"]++;

                if (group.SpecifiedForSteelDesign || group.SpecifiedForConcreteDesign)
                    stats["DesignGroups"]++;

                if (group.SpecifiedForAutoSeismicOutput || group.SpecifiedForAutoWindOutput)
                    stats["OutputGroups"]++;

                if (group.SpecifiedForSelection)
                    stats["SelectionGroups"]++;

                stats["TotalObjects"] += group.ObjectCount;
            }

            _logger.LogDebug("Group statistics: {Stats}", string.Join(", ", stats.Select(kvp => $"{kvp.Key}={kvp.Value}")));
            return stats;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting group statistics: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes all empty groups (groups with no assignments).
    /// </summary>
    public int DeleteEmptyGroups()
    {
        try
        {
            var groupNames = GetNameList();
            int deletedCount = 0;

            foreach (var groupName in groupNames)
            {
                try
                {
                    var assignmentCount = GetAssignmentCount(groupName);
                    if (assignmentCount == 0)
                    {
                        int ret = Delete(groupName);
                        if (ret == 0)
                        {
                            deletedCount++;
                            _logger.LogDebug("Deleted empty group {GroupName}", groupName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to delete group {GroupName}: {Error}", groupName, ex.Message);
                }
            }

            _logger.LogDebug("Deleted {Count} empty groups", deletedCount);
            return deletedCount;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting empty groups: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Renames a group by creating a new group with the same properties and deleting the old one.
    /// </summary>
    public int RenameGroup(string oldName, string newName)
    {
        try
        {
            if (string.IsNullOrEmpty(oldName))
                throw new ArgumentException("Old name cannot be null or empty", nameof(oldName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New name cannot be null or empty", nameof(newName));

            if (oldName == newName)
                return 0; // No change needed

            // Check if old group exists
            if (!GroupExists(oldName))
                throw new EtabsException($"Group '{oldName}' does not exist");

            // Check if new group name already exists
            if (GroupExists(newName))
                throw new EtabsException($"Group '{newName}' already exists");

            // Get the old group properties
            var group = GetGroup(oldName);

            // Create new group with same properties
            group.Name = newName;
            int ret1 = SetGroup(group);
            if (ret1 != 0)
                return ret1;

            // Note: We cannot reassign objects because that's handled by the individual object managers
            // This just creates an empty group with the same properties
            // Objects need to be reassigned separately using PointObj.SetGroupAssign, FrameObj.SetGroupAssign, etc.

            // Delete old group
            int ret2 = Delete(oldName);

            _logger.LogDebug("Renamed group from {OldName} to {NewName}", oldName, newName);

            return ret2;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error renaming group from '{oldName}' to '{newName}': {ex.Message}", ex);
        }
    }

    #endregion
}