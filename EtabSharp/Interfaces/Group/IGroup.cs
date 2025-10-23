using EtabSharp.Groups.Models;

namespace EtabSharp.Interfaces.Group;

/// <summary>
/// Interface for managing groups in the ETABS model.
/// Groups are used to organize structural elements for selection, design, and output purposes.
/// </summary>
public interface IGroup
{
    #region Group Creation and Management

    /// <summary>
    /// Creates or modifies a group with specified properties.
    /// Wraps cSapModel.GroupDef.SetGroup_1 (most comprehensive version).
    /// </summary>
    /// <param name="group">Group model with all properties</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroup(Groups.Models.Group group);

    /// <summary>
    /// Creates or modifies a group with basic properties.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <param name="color">Color for the group (RGB integer, -1 for automatic)</param>
    /// <param name="specifiedForSelection">Whether group is used for selection</param>
    /// <param name="specifiedForDesign">Whether group is used for design</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroup(string name, int color = -1, bool specifiedForSelection = true, bool specifiedForDesign = true);

    /// <summary>
    /// Gets a group with all its properties.
    /// Wraps cSapModel.GroupDef.GetGroup_1 (most comprehensive version).
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <returns>Group model with all properties</returns>
    Groups.Models.Group GetGroup(string name);

    /// <summary>
    /// Deletes a group from the model.
    /// Wraps cSapModel.GroupDef.Delete.
    /// </summary>
    /// <param name="name">Name of the group to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int Delete(string name);

    /// <summary>
    /// Gets the names of all defined groups in the model.
    /// Wraps cSapModel.GroupDef.GetNameList.
    /// </summary>
    /// <returns>Array of group names</returns>
    string[] GetNameList();

    /// <summary>
    /// Gets the count of groups in the model.
    /// Wraps cSapModel.GroupDef.Count.
    /// </summary>
    /// <returns>Total number of groups</returns>
    int Count();

    #endregion

    #region Group Assignments

    /// <summary>
    /// Gets all object assignments for a group.
    /// Wraps cSapModel.GroupDef.GetAssignments.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <returns>List of GroupAssignment objects</returns>
    List<GroupAssignment> GetAssignments(string name);

    /// <summary>
    /// Gets objects of a specific type assigned to a group.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <param name="objectType">Type of objects to retrieve</param>
    /// <returns>Array of object names of the specified type</returns>
    string[] GetAssignmentsByType(string name, GroupObjectType objectType);

    /// <summary>
    /// Gets the count of objects in a group.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <returns>Number of objects in the group</returns>
    int GetAssignmentCount(string name);

    #endregion

    #region Convenience Methods

    /// <summary>
    /// Creates a default group with standard settings.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <param name="color">Color for the group (optional)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CreateDefaultGroup(string name, int color = -1);

    /// <summary>
    /// Creates a group specifically for design purposes.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <param name="color">Color for the group (optional)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CreateDesignGroup(string name, int color = -1);

    /// <summary>
    /// Creates a group specifically for output purposes.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <param name="color">Color for the group (optional)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CreateOutputGroup(string name, int color = -1);

    /// <summary>
    /// Creates a group specifically for selection purposes.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <param name="color">Color for the group (optional)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int CreateSelectionGroup(string name, int color = -1);

    /// <summary>
    /// Checks if a group exists in the model.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <returns>True if the group exists, false otherwise</returns>
    bool GroupExists(string name);

    /// <summary>
    /// Gets all groups in the model with their properties.
    /// </summary>
    /// <returns>List of all Group objects</returns>
    List<Groups.Models.Group> GetAllGroups();

    /// <summary>
    /// Sets the color of a group.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <param name="red">Red component (0-255)</param>
    /// <param name="green">Green component (0-255)</param>
    /// <param name="blue">Blue component (0-255)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroupColor(string name, int red, int green, int blue);

    /// <summary>
    /// Sets the color of a group using a predefined color integer.
    /// </summary>
    /// <param name="name">Name of the group</param>
    /// <param name="colorInt">Color as RGB integer</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int SetGroupColor(string name, int colorInt);

    /// <summary>
    /// Gets groups that contain a specific object.
    /// </summary>
    /// <param name="objectName">Name of the object</param>
    /// <param name="objectType">Type of the object</param>
    /// <returns>Array of group names containing the object</returns>
    string[] GetGroupsContainingObject(string objectName, GroupObjectType objectType);

    /// <summary>
    /// Gets statistics about all groups in the model.
    /// </summary>
    /// <returns>Dictionary with group statistics</returns>
    Dictionary<string, int> GetGroupStatistics();

    /// <summary>
    /// Deletes all empty groups (groups with no assignments).
    /// </summary>
    /// <returns>Number of groups deleted</returns>
    int DeleteEmptyGroups();

    /// <summary>
    /// Renames a group.
    /// </summary>
    /// <param name="oldName">Current name of the group</param>
    /// <param name="newName">New name for the group</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    int RenameGroup(string oldName, string newName);

    #endregion
}