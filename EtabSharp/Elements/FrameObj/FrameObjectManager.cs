using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Elements.Objects;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.FrameObj;

/// <summary>
/// Manages frame objects in the ETABS model.
/// Implements the IFrame interface by wrapping cSapModel.FrameObj operations.
/// This is the main partial class containing core geometry and creation methods.
/// </summary>
public partial class FrameObjectManager : IFrame
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the FrameObjectManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public FrameObjectManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Frame Creation & Geometry

    /// <summary>
    /// Adds a frame object by specifying end point names.
    /// Wraps cSapModel.FrameObj.AddByPoint.
    /// </summary>
    /// <param name="point1">Name of the first (I-end) point object</param>
    /// <param name="point2">Name of the second (J-end) point object</param>
    /// <param name="sectionName">Name of the frame section property to assign (default: "Default")</param>
    /// <param name="userName">Optional custom name (auto-generated if empty)</param>
    /// <returns>The name of the created frame object</returns>
    public string AddFrame(string point1, string point2, string sectionName = "Default", string userName = "")
    {
        try
        {
            if (string.IsNullOrEmpty(point1))
                throw new ArgumentException("Point1 name cannot be null or empty", nameof(point1));
            if (string.IsNullOrEmpty(point2))
                throw new ArgumentException("Point2 name cannot be null or empty", nameof(point2));
            if (string.IsNullOrEmpty(sectionName))
                throw new ArgumentException("Section name cannot be null or empty", nameof(sectionName));

            string frameName = userName;
            int ret = _sapModel.FrameObj.AddByPoint(point1, point2, ref frameName, sectionName, userName);

            if (ret != 0)
                throw new EtabsException(ret, "AddByPoint", $"Failed to add frame between points '{point1}' and '{point2}'");

            _logger.LogDebug("Added frame {FrameName} between points {Point1} and {Point2} with section {Section}", 
                frameName, point1, point2, sectionName);

            return frameName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding frame between points '{point1}' and '{point2}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Adds a frame object by specifying end point coordinates directly.
    /// Wraps cSapModel.FrameObj.AddByCoord.
    /// </summary>
    /// <param name="xi">X coordinate of first point (I-end)</param>
    /// <param name="yi">Y coordinate of first point (I-end)</param>
    /// <param name="zi">Z coordinate of first point (I-end)</param>
    /// <param name="xj">X coordinate of second point (J-end)</param>
    /// <param name="yj">Y coordinate of second point (J-end)</param>
    /// <param name="zj">Z coordinate of second point (J-end)</param>
    /// <param name="sectionName">Name of the frame section property (default: "Default")</param>
    /// <param name="userName">Optional custom name</param>
    /// <param name="csys">Coordinate system (default: "Global")</param>
    /// <returns>The name of the created frame object</returns>
    public string AddFrameByCoordinates(double xi, double yi, double zi, double xj, double yj, double zj,
        string sectionName = "Default", string userName = "", string csys = "Global")
    {
        try
        {
            if (string.IsNullOrEmpty(sectionName))
                throw new ArgumentException("Section name cannot be null or empty", nameof(sectionName));
            if (string.IsNullOrEmpty(csys))
                throw new ArgumentException("Coordinate system cannot be null or empty", nameof(csys));

            string frameName = userName;
            int ret = _sapModel.FrameObj.AddByCoord(xi, yi, zi, xj, yj, zj, ref frameName, sectionName, userName, csys);

            if (ret != 0)
                throw new EtabsException(ret, "AddByCoord", $"Failed to add frame from ({xi}, {yi}, {zi}) to ({xj}, {yj}, {zj})");

            _logger.LogDebug("Added frame {FrameName} from ({Xi}, {Yi}, {Zi}) to ({Xj}, {Yj}, {Zj}) with section {Section}", 
                frameName, xi, yi, zi, xj, yj, zj, sectionName);

            return frameName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding frame by coordinates: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Changes the name of an existing frame object.
    /// Wraps cSapModel.FrameObj.ChangeName.
    /// </summary>
    /// <param name="currentName">Current name of the frame</param>
    /// <param name="newName">New name for the frame</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int ChangeName(string currentName, string newName)
    {
        try
        {
            if (string.IsNullOrEmpty(currentName))
                throw new ArgumentException("Current name cannot be null or empty", nameof(currentName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New name cannot be null or empty", nameof(newName));

            int ret = _sapModel.FrameObj.ChangeName(currentName, newName);

            if (ret != 0)
                throw new EtabsException(ret, "ChangeName", $"Failed to change frame name from '{currentName}' to '{newName}'");

            _logger.LogDebug("Changed frame name from {OldName} to {NewName}", currentName, newName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error changing frame name from '{currentName}' to '{newName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a frame object with its properties and connectivity.
    /// Wraps cSapModel.FrameObj.GetPoints and other methods.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Frame model with properties and connectivity</returns>
    public Frame GetFrame(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            // Get end points
            string point1 = "", point2 = "";
            int ret = _sapModel.FrameObj.GetPoints(frameName, ref point1, ref point2);

            if (ret != 0)
                throw new EtabsException(ret, "GetPoints", $"Failed to get points for frame '{frameName}'");

            var frame = new Frame
            {
                Name = frameName,
                Point1Name = point1,
                Point2Name = point2
            };

            // Get section
            try
            {
                string sectionName = "", autoSelectList = "";
                ret = _sapModel.FrameObj.GetSection(frameName, ref sectionName, ref autoSelectList);
                if (ret == 0)
                {
                    frame.SectionName = sectionName;
                }
            }
            catch
            {
                // Ignore errors getting section - not critical
            }

            // Get label and story
            try
            {
                string label = "", story = "";
                ret = _sapModel.FrameObj.GetLabelFromName(frameName, ref label, ref story);
                if (ret == 0)
                {
                    frame.Label = label;
                    frame.Story = story;
                }
            }
            catch
            {
                // Ignore errors getting label/story - not critical
            }

            // Get local axis angle
            try
            {
                double angle = 0;
                bool advanced = false;
                ret = _sapModel.FrameObj.GetLocalAxes(frameName, ref angle, ref advanced);
                if (ret == 0)
                {
                    frame.LocalAxisAngle = angle;
                }
            }
            catch
            {
                // Ignore errors getting local axes - not critical
            }

            // Get design procedure
            try
            {
                int designType = 0;
                ret = _sapModel.FrameObj.GetDesignProcedure(frameName, ref designType);
                if (ret == 0)
                {
                    frame.DesignProcedure = designType;
                }
            }
            catch
            {
                // Ignore errors getting design procedure - not critical
            }

            return frame;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves the names of all defined frame objects in the model.
    /// Wraps cSapModel.FrameObj.GetNameList.
    /// </summary>
    /// <returns>Array of frame object names</returns>
    public string[] GetNameList()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.FrameObj.GetNameList(ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get frame name list");

            _logger.LogDebug("Retrieved {Count} frame names", numberNames);

            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting frame name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all frame objects with their properties and connectivity.
    /// Wraps cSapModel.FrameObj.GetAllFrames.
    /// </summary>
    /// <returns>List of Frame models with properties and connectivity</returns>
    public List<Frame> GetAllFrames()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;
            string[] propNames = null;
            string[] storyNames = null;
            string[] pointName1 = null;
            string[] pointName2 = null;
            double[] point1X = null, point1Y = null, point1Z = null;
            double[] point2X = null, point2Y = null, point2Z = null;
            double[] angles = null;
            double[] offset1X = null, offset2X = null, offset1Y = null, offset2Y = null, offset1Z = null, offset2Z = null;
            int[] cardinalPoints = null;

            int ret = _sapModel.FrameObj.GetAllFrames(ref numberNames, ref names, ref propNames, ref storyNames,
                ref pointName1, ref pointName2, ref point1X, ref point1Y, ref point1Z, ref point2X, ref point2Y, ref point2Z,
                ref angles, ref offset1X, ref offset2X, ref offset1Y, ref offset2Y, ref offset1Z, ref offset2Z, ref cardinalPoints);

            if (ret != 0)
                throw new EtabsException(ret, "GetAllFrames", "Failed to get all frames");

            var frames = new List<Frame>();

            for (int i = 0; i < numberNames; i++)
            {
                var frame = new Frame
                {
                    Name = names[i],
                    Point1Name = pointName1[i],
                    Point2Name = pointName2[i],
                    SectionName = propNames[i],
                    Story = storyNames[i],
                    LocalAxisAngle = angles[i]
                };

                // Calculate length
                double dx = point2X[i] - point1X[i];
                double dy = point2Y[i] - point1Y[i];
                double dz = point2Z[i] - point1Z[i];
                frame.Length = Math.Sqrt(dx * dx + dy * dy + dz * dz);

                frames.Add(frame);
            }

            _logger.LogDebug("Retrieved {Count} frames", frames.Count);
            return frames;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all frames: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of frame objects in the model.
    /// Wraps cSapModel.FrameObj.Count.
    /// </summary>
    /// <param name="frameType">Frame type filter: "All", "Straight", "Curved" (default: "All")</param>
    /// <returns>Total number of frames</returns>
    public int Count(string frameType = "All")
    {
        try
        {
            int count = _sapModel.FrameObj.Count(frameType);
            _logger.LogDebug("Frame count ({FrameType}): {Count}", frameType, count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting frame count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes specified frame objects from the model.
    /// Wraps cSapModel.FrameObj.Delete.
    /// </summary>
    /// <param name="frameName">Name of the frame to delete</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int Delete(string frameName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.Delete(frameName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "Delete", $"Failed to delete frame '{frameName}'");

            _logger.LogDebug("Deleted frame {FrameName}", frameName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    // Additional methods are implemented in partial class files:
    // - FrameObjectManager.Properties.cs (Section, Material, Local Axes, Insertion Point, Modifiers)
    // - FrameObjectManager.EndReleases.cs (End Releases)
    // - FrameObjectManager.Loads.cs (Distributed, Point, Temperature Loads)
    // - FrameObjectManager.Design.cs (Design Procedure, Pier, Spandrel, Column Splice, T/C Limits, Lateral Bracing, Output Stations)
    // - FrameObjectManager.Advanced.cs (Mass, Spring, Group, Selection, Label, Transformation, Hinges, Supports, Curved Frames)
}