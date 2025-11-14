using EtabSharp.Elements.Selection.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Elements.Selection;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.Selection;

/// <summary>
/// Handles ETABS object selection operations.
/// </summary>
public sealed class Selection : ISelection
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    internal Selection(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Selection Operations

    /// <inheritdoc/>
    public int SelectAll(bool deselect = false)
    {
        try
        {
            _logger.LogInformation("{Action} all objects", deselect ? "Deselecting" : "Selecting");

            int ret = _sapModel.SelectObj.All(deselect);

            if (ret != 0)
            {
                _logger.LogError("Failed to {Action} all objects. Return code: {ReturnCode}",
                    deselect ? "deselect" : "select", ret);
                throw new EtabsException(ret, "SelectAll",
                    $"Failed to {(deselect ? "deselect" : "select")} all objects.");
            }

            _logger.LogInformation("Successfully {Action} all objects",
                deselect ? "deselected" : "selected");
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error in SelectAll operation");
            throw new EtabsException("Unexpected error in SelectAll operation", ex);
        }
    }

    /// <inheritdoc/>
    public int ClearSelection()
    {
        try
        {
            _logger.LogInformation("Clearing all selections");

            int ret = _sapModel.SelectObj.ClearSelection();

            if (ret != 0)
            {
                _logger.LogError("Failed to clear selection. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "ClearSelection", "Failed to clear selection.");
            }

            _logger.LogInformation("Successfully cleared all selections");
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error clearing selection");
            throw new EtabsException("Unexpected error clearing selection", ex);
        }
    }

    /// <inheritdoc/>
    public int InvertSelection()
    {
        try
        {
            _logger.LogInformation("Inverting selection");

            int ret = _sapModel.SelectObj.InvertSelection();

            if (ret != 0)
            {
                _logger.LogError("Failed to invert selection. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "InvertSelection", "Failed to invert selection.");
            }

            _logger.LogInformation("Successfully inverted selection");
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error inverting selection");
            throw new EtabsException("Unexpected error inverting selection", ex);
        }
    }

    /// <inheritdoc/>
    public int RestorePreviousSelection()
    {
        try
        {
            _logger.LogInformation("Restoring previous selection");

            int ret = _sapModel.SelectObj.PreviousSelection();

            if (ret != 0)
            {
                _logger.LogError("Failed to restore previous selection. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "PreviousSelection", "Failed to restore previous selection.");
            }

            _logger.LogInformation("Successfully restored previous selection");
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error restoring previous selection");
            throw new EtabsException("Unexpected error restoring previous selection", ex);
        }
    }

    #endregion

    #region Group Selection

    /// <inheritdoc/>
    public int SelectGroup(string groupName, bool deselect = false)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            throw new ArgumentException("Group name cannot be empty.", nameof(groupName));

        try
        {
            _logger.LogInformation("{Action} objects in group '{GroupName}'",
                deselect ? "Deselecting" : "Selecting", groupName);

            int ret = _sapModel.SelectObj.Group(groupName, deselect);

            if (ret != 0)
            {
                _logger.LogError("Failed to {Action} group '{GroupName}'. Return code: {ReturnCode}",
                    deselect ? "deselect" : "select", groupName, ret);
                throw new EtabsException(ret, "SelectGroup",
                    $"Failed to {(deselect ? "deselect" : "select")} group '{groupName}'.");
            }

            _logger.LogInformation("Successfully {Action} group '{GroupName}'",
                deselect ? "deselected" : "selected", groupName);
            return ret;
        }
        catch (Exception ex) when (ex is not EtabsException && ex is not ArgumentException)
        {
            _logger.LogError(ex, "Unexpected error selecting group '{GroupName}'", groupName);
            throw new EtabsException($"Unexpected error selecting group '{groupName}'", ex);
        }
    }

    #endregion

    #region Get Selected Objects

    /// <inheritdoc/>
    public SelectedObjects GetSelected()
    {
        try
        {
            _logger.LogDebug("Retrieving selected objects");

            int numberItems = 0;
            int[] objectType = null;
            string[] objectName = null;

            int ret = _sapModel.SelectObj.GetSelected(ref numberItems, ref objectType, ref objectName);

            if (ret != 0)
            {
                _logger.LogError("Failed to get selected objects. Return code: {ReturnCode}", ret);
                throw new EtabsException(ret, "GetSelected", "Failed to retrieve selected objects.");
            }

            // Create result object with raw API data
            var selectedObjects = new SelectedObjects
            {
                NumberItems = numberItems,
                ObjectType = objectType ?? Array.Empty<int>(),
                ObjectName = objectName ?? Array.Empty<string>()
            };

            if (numberItems == 0)
            {
                _logger.LogDebug("No objects are currently selected");
                return selectedObjects;
            }

            // Categorize objects by type using the numeric codes
            // 1=Point, 2=Frame, 3=Cable, 4=Tendon, 5=Area, 6=Solid, 7=Link
            var pointList = new List<string>();
            var frameList = new List<string>();
            var cableList = new List<string>();
            var tendonList = new List<string>();
            var areaList = new List<string>();
            var solidList = new List<string>();
            var linkList = new List<string>();

            for (int i = 0; i < numberItems; i++)
            {
                switch (objectType[i])
                {
                    case 1: // Point
                        pointList.Add(objectName[i]);
                        break;
                    case 2: // Frame
                        frameList.Add(objectName[i]);
                        break;
                    case 3: // Cable
                        cableList.Add(objectName[i]);
                        break;
                    case 4: // Tendon
                        tendonList.Add(objectName[i]);
                        break;
                    case 5: // Area
                        areaList.Add(objectName[i]);
                        break;
                    case 6: // Solid
                        solidList.Add(objectName[i]);
                        break;
                    case 7: // Link
                        linkList.Add(objectName[i]);
                        break;
                    default:
                        _logger.LogWarning("Unknown object type {ObjectType} for object {ObjectName}",
                            objectType[i], objectName[i]);
                        break;
                }
            }

            // Populate the categorized arrays
            selectedObjects.NumberPoints = pointList.Count;
            selectedObjects.PointNames = pointList.ToArray();

            selectedObjects.NumberFrames = frameList.Count;
            selectedObjects.FrameNames = frameList.ToArray();

            selectedObjects.NumberCables = cableList.Count;
            selectedObjects.CableNames = cableList.ToArray();

            selectedObjects.NumberTendons = tendonList.Count;
            selectedObjects.TendonNames = tendonList.ToArray();

            selectedObjects.NumberAreas = areaList.Count;
            selectedObjects.AreaNames = areaList.ToArray();

            selectedObjects.NumberSolids = solidList.Count;
            selectedObjects.SolidNames = solidList.ToArray();

            selectedObjects.NumberLinks = linkList.Count;
            selectedObjects.LinkNames = linkList.ToArray();

            _logger.LogDebug("Retrieved {Count} selected objects: {Summary}",
                numberItems, selectedObjects.ToString());

            return selectedObjects;
        }
        catch (Exception ex) when (ex is not EtabsException)
        {
            _logger.LogError(ex, "Unexpected error getting selected objects");
            throw new EtabsException("Unexpected error getting selected objects", ex);
        }
    }

    /// <inheritdoc/>
    public int GetSelectedCount()
    {
        try
        {
            var selected = GetSelected();
            return selected.NumberItems;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting selected count");
            return 0;
        }
    }

    /// <inheritdoc/>
    public bool HasSelection()
    {
        return GetSelectedCount() > 0;
    }

    #endregion

    #region Individual Object Selection

    /// <inheritdoc/>
    public int SelectPoint(string pointName, bool deselect = false)
    {
        if (string.IsNullOrWhiteSpace(pointName))
            throw new ArgumentException("Point name cannot be empty.", nameof(pointName));

        try
        {
            _logger.LogDebug("{Action} point '{PointName}'",
                deselect ? "Deselecting" : "Selecting", pointName);

            int ret = _sapModel.PointObj.SetSelected(pointName, !deselect);

            if (ret != 0)
            {
                _logger.LogWarning("Failed to {Action} point '{PointName}'. Return code: {ReturnCode}",
                    deselect ? "deselect" : "select", pointName, ret);
            }

            return ret;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error selecting point '{PointName}'", pointName);
            throw new EtabsException($"Unexpected error selecting point '{pointName}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SelectFrame(string frameName, bool deselect = false)
    {
        if (string.IsNullOrWhiteSpace(frameName))
            throw new ArgumentException("Frame name cannot be empty.", nameof(frameName));

        try
        {
            _logger.LogDebug("{Action} frame '{FrameName}'",
                deselect ? "Deselecting" : "Selecting", frameName);

            int ret = _sapModel.FrameObj.SetSelected(frameName, !deselect);

            if (ret != 0)
            {
                _logger.LogWarning("Failed to {Action} frame '{FrameName}'. Return code: {ReturnCode}",
                    deselect ? "deselect" : "select", frameName, ret);
            }

            return ret;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error selecting frame '{FrameName}'", frameName);
            throw new EtabsException($"Unexpected error selecting frame '{frameName}'", ex);
        }
    }

    /// <inheritdoc/>
    public int SelectArea(string areaName, bool deselect = false)
    {
        if (string.IsNullOrWhiteSpace(areaName))
            throw new ArgumentException("Area name cannot be empty.", nameof(areaName));

        try
        {
            _logger.LogDebug("{Action} area '{AreaName}'",
                deselect ? "Deselecting" : "Selecting", areaName);

            int ret = _sapModel.AreaObj.SetSelected(areaName, !deselect);

            if (ret != 0)
            {
                _logger.LogWarning("Failed to {Action} area '{AreaName}'. Return code: {ReturnCode}",
                    deselect ? "deselect" : "select", areaName, ret);
            }

            return ret;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error selecting area '{AreaName}'", areaName);
            throw new EtabsException($"Unexpected error selecting area '{areaName}'", ex);
        }
    }

    #endregion
}