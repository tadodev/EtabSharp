using EtabSharp.Elements.AreaObj.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Elements.Objects;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.AreaObj;

/// <summary>
/// Manages area objects in the ETABS model.
/// Implements the IArea interface by wrapping cSapModel.AreaObj operations.
/// This is the main partial class containing core geometry and creation methods.
/// </summary>
public partial class AreaObjectManager : IArea
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the AreaObjectManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public AreaObjectManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Area Creation & Geometry

    /// <summary>
    /// Adds an area object by specifying coordinates.
    /// Wraps cSapModel.AreaObj.AddByCoord.
    /// </summary>
    /// <param name="coordinates">Array of coordinates defining the area boundary</param>
    /// <param name="propertyName">Name of the area property to assign</param>
    /// <param name="userName">Optional custom name for the area</param>
    /// <param name="csys">Coordinate system name</param>
    /// <returns>The name of the created area object</returns>
    public string AddAreaByCoordinates(AreaCoordinate[] coordinates, string propertyName = "Default", string userName = "", string csys = "Global")
    {
        try
        {
            if (coordinates == null || coordinates.Length < 3)
                throw new ArgumentException("At least 3 coordinates are required to define an area", nameof(coordinates));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (string.IsNullOrEmpty(csys))
                throw new ArgumentException("Coordinate system cannot be null or empty", nameof(csys));

            int numberPoints = coordinates.Length;
            double[] x = coordinates.Select(c => c.X).ToArray();
            double[] y = coordinates.Select(c => c.Y).ToArray();
            double[] z = coordinates.Select(c => c.Z).ToArray();
            string areaName = userName;

            int ret = _sapModel.AreaObj.AddByCoord(numberPoints, ref x, ref y, ref z, ref areaName, propertyName, userName, csys);

            if (ret != 0)
                throw new EtabsException(ret, "AddByCoord", $"Failed to add area with {numberPoints} points");

            _logger.LogDebug("Added area {AreaName} with {PointCount} points using property {PropertyName}",
                areaName, numberPoints, propertyName);

            return areaName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding area by coordinates: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Adds an area object by specifying point names.
    /// Wraps cSapModel.AreaObj.AddByPoint.
    /// </summary>
    /// <param name="pointNames">Array of point names defining the area boundary</param>
    /// <param name="propertyName">Name of the area property to assign</param>
    /// <param name="userName">Optional custom name for the area</param>
    /// <returns>The name of the created area object</returns>
    public string AddAreaByPoints(string[] pointNames, string propertyName = "Default", string userName = "")
    {
        try
        {
            if (pointNames == null || pointNames.Length < 3)
                throw new ArgumentException("At least 3 point names are required to define an area", nameof(pointNames));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int numberPoints = pointNames.Length;
            string areaName = userName;

            int ret = _sapModel.AreaObj.AddByPoint(numberPoints, ref pointNames, ref areaName, propertyName, userName);

            if (ret != 0)
                throw new EtabsException(ret, "AddByPoint", $"Failed to add area using points: {string.Join(", ", pointNames)}");

            _logger.LogDebug("Added area {AreaName} using points [{Points}] with property {PropertyName}",
                areaName, string.Join(", ", pointNames), propertyName);

            return areaName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding area by points: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Changes the name of an existing area object.
    /// Wraps cSapModel.AreaObj.ChangeName.
    /// </summary>
    /// <param name="currentName">Current name of the area</param>
    /// <param name="newName">New name for the area</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int ChangeName(string currentName, string newName)
    {
        try
        {
            if (string.IsNullOrEmpty(currentName))
                throw new ArgumentException("Current name cannot be null or empty", nameof(currentName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New name cannot be null or empty", nameof(newName));

            int ret = _sapModel.AreaObj.ChangeName(currentName, newName);

            if (ret != 0)
                throw new EtabsException(ret, "ChangeName", $"Failed to change area name from '{currentName}' to '{newName}'");

            _logger.LogDebug("Changed area name from {OldName} to {NewName}", currentName, newName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error changing area name from '{currentName}' to '{newName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets an area object with its properties and connectivity.
    /// Wraps multiple cSapModel.AreaObj methods.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Area model with properties and connectivity</returns>
    public Area GetArea(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            var area = new Area { Name = areaName };

            // Get points
            try
            {
                int numberPoints = 0;
                string[] points = null;
                int ret = _sapModel.AreaObj.GetPoints(areaName, ref numberPoints, ref points);
                if (ret == 0 && points != null)
                {
                    area.PointNames.AddRange(points);
                }
            }
            catch
            {
                // Ignore errors getting points - not critical
            }

            // Get property
            try
            {
                string propertyName = "";
                int ret = _sapModel.AreaObj.GetProperty(areaName, ref propertyName);
                if (ret == 0)
                {
                    area.PropertyName = propertyName;
                }
            }
            catch
            {
                // Ignore errors getting property - not critical
            }

            // Get label and story
            try
            {
                string label = "", story = "";
                int ret = _sapModel.AreaObj.GetLabelFromName(areaName, ref label, ref story);
                if (ret == 0)
                {
                    area.Label = label;
                    area.Story = story;
                }
            }
            catch
            {
                // Ignore errors getting label/story - not critical
            }

            // Get design orientation
            try
            {
                eAreaDesignOrientation orientation = eAreaDesignOrientation.Null;
                int ret = _sapModel.AreaObj.GetDesignOrientation(areaName, ref orientation);
                if (ret == 0)
                {
                    area.DesignOrientation = orientation;
                }
            }
            catch
            {
                // Ignore errors getting design orientation - not critical
            }

            // Get local axes
            try
            {
                double angle = 0;
                bool advanced = false;
                int ret = _sapModel.AreaObj.GetLocalAxes(areaName, ref angle, ref advanced);
                if (ret == 0)
                {
                    area.LocalAxisAngle = angle;
                    area.IsAdvancedLocalAxes = advanced;
                }
            }
            catch
            {
                // Ignore errors getting local axes - not critical
            }

            // Get material override
            try
            {
                string materialName = "";
                int ret = _sapModel.AreaObj.GetMaterialOverwrite(areaName, ref materialName);
                if (ret == 0)
                {
                    area.MaterialOverride = materialName;
                }
            }
            catch
            {
                // Ignore errors getting material override - not critical
            }

            return area;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all area objects with their properties.
    /// Wraps cSapModel.AreaObj.GetAllAreas.
    /// </summary>
    /// <returns>List of Area models</returns>
    public List<Area> GetAllAreas()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;
            eAreaDesignOrientation[] designOrientations = null;
            int numberBoundaryPts = 0;
            int[] pointDelimiter = null;
            string[] pointNames = null;
            double[] pointX = null, pointY = null, pointZ = null;

            int ret = _sapModel.AreaObj.GetAllAreas(ref numberNames, ref names, ref designOrientations,
                ref numberBoundaryPts, ref pointDelimiter, ref pointNames, ref pointX, ref pointY, ref pointZ);

            if (ret != 0)
                throw new EtabsException(ret, "GetAllAreas", "Failed to get all areas");

            var areas = new List<Area>();

            for (int i = 0; i < numberNames; i++)
            {
                var area = new Area
                {
                    Name = names[i],
                    DesignOrientation = designOrientations[i]
                };

                // Extract point information for this area
                int startIndex = i == 0 ? 0 : pointDelimiter[i - 1];
                int endIndex = pointDelimiter[i];

                for (int j = startIndex; j < endIndex && j < pointNames.Length; j++)
                {
                    area.PointNames.Add(pointNames[j]);
                    if (j < pointX.Length && j < pointY.Length && j < pointZ.Length)
                    {
                        area.Coordinates.Add(new AreaCoordinate(pointX[j], pointY[j], pointZ[j]));
                    }
                }

                // Calculate area value
                area.AreaValue = area.CalculateArea();

                areas.Add(area);
            }

            _logger.LogDebug("Retrieved {Count} areas", areas.Count);
            return areas;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all areas: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves the names of all defined area objects.
    /// Wraps cSapModel.AreaObj.GetNameList.
    /// </summary>
    /// <returns>Array of area object names</returns>
    public string[] GetNameList()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.AreaObj.GetNameList(ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get area name list");

            _logger.LogDebug("Retrieved {Count} area names", numberNames);

            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting area name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of area objects in the model.
    /// Wraps cSapModel.AreaObj.Count.
    /// </summary>
    /// <returns>Total number of areas</returns>
    public int Count()
    {
        try
        {
            int count = _sapModel.AreaObj.Count();
            _logger.LogDebug("Area count: {Count}", count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting area count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes specified area objects from the model.
    /// Wraps cSapModel.AreaObj.Delete.
    /// </summary>
    /// <param name="areaName">Name of the area to delete</param>
    /// <param name="itemType">Apply to object, group, or selected objects</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int Delete(string areaName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.Delete(areaName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "Delete", $"Failed to delete area '{areaName}'");

            _logger.LogDebug("Deleted area {AreaName}", areaName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    // Additional methods are implemented in partial class files:
    // - AreaObjectManager.Properties.cs (Property, Material, Local Axes, Modifier methods)
    // - AreaObjectManager.Loads.cs (Load methods)
    // - AreaObjectManager.Design.cs (Design, Pier, Spandrel, Diaphragm methods)
    // - AreaObjectManager.Selection.cs (Selection, Group, Label methods)
}