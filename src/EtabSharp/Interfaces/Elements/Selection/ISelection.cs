using EtabSharp.Elements.Selection.Models;

namespace EtabSharp.Interfaces.Elements.Selection;

/// <summary>
/// Provides methods for managing object selection in the ETABS model.
/// This interface handles selection, deselection, and retrieval of selected objects
/// including points, frames, cables, tendons, areas, solids, and links.
/// </summary>
public interface ISelection
{
    #region Selection Operations

    /// <summary>
    /// Selects or deselects all objects in the model.
    /// </summary>
    /// <param name="deselect">
    /// If true, all objects are deselected.
    /// If false, all objects are selected.
    /// </param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SelectAll(bool deselect = false);

    /// <summary>
    /// Deselects all objects in the model.
    /// This is equivalent to calling SelectAll(deselect: true).
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int ClearSelection();

    /// <summary>
    /// Inverts the current selection.
    /// All selected objects become deselected, and all deselected objects become selected.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int InvertSelection();

    /// <summary>
    /// Restores the previous selection state.
    /// </summary>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int RestorePreviousSelection();

    #endregion

    #region Group Selection

    /// <summary>
    /// Selects or deselects all objects in a specified group.
    /// </summary>
    /// <param name="groupName">The name of the group.</param>
    /// <param name="deselect">
    /// If true, objects in the group are deselected.
    /// If false, objects in the group are selected.
    /// </param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    /// <exception cref="ArgumentException">If group name is invalid.</exception>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    int SelectGroup(string groupName, bool deselect = false);

    #endregion

    #region Get Selected Objects

    /// <summary>
    /// Retrieves information about all currently selected objects in the model.
    /// </summary>
    /// <returns>
    /// A <see cref="SelectedObjects"/> object containing arrays of selected object names
    /// organized by type (points, frames, cables, tendons, areas, solids, links).
    /// </returns>
    /// <exception cref="EtabSharp.Exceptions.EtabsException">If ETABS API fails.</exception>
    SelectedObjects GetSelected();

    /// <summary>
    /// Gets the total count of all selected objects in the model.
    /// </summary>
    /// <returns>Total number of selected objects across all types.</returns>
    int GetSelectedCount();

    /// <summary>
    /// Checks if any objects are currently selected.
    /// </summary>
    /// <returns>True if at least one object is selected, false otherwise.</returns>
    bool HasSelection();

    #endregion

    #region Individual Object Selection

    /// <summary>
    /// Selects or deselects a specific point object.
    /// </summary>
    /// <param name="pointName">Name of the point object.</param>
    /// <param name="deselect">If true, deselects; if false, selects.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SelectPoint(string pointName, bool deselect = false);

    /// <summary>
    /// Selects or deselects a specific frame object.
    /// </summary>
    /// <param name="frameName">Name of the frame object.</param>
    /// <param name="deselect">If true, deselects; if false, selects.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SelectFrame(string frameName, bool deselect = false);

    /// <summary>
    /// Selects or deselects a specific area object.
    /// </summary>
    /// <param name="areaName">Name of the area object.</param>
    /// <param name="deselect">If true, deselects; if false, selects.</param>
    /// <returns>0 if successful, non-zero otherwise.</returns>
    int SelectArea(string areaName, bool deselect = false);

    #endregion
}