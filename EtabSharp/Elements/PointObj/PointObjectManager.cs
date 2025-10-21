using EtabSharp.Elements.PointObj.Models;
using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Elements.Objects;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.PointObj;

/// <summary>
/// Manages point objects in the ETABS model.
/// Implements the IPoint interface by wrapping cSapModel.PointObj operations.
/// This is the main partial class containing core geometry and creation methods.
/// </summary>
public partial class PointObjectManager : IPoint
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the PointObjectManager class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public PointObjectManager(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Point Creation & Geometry

    /// <summary>
    /// Adds a point object at specified Cartesian coordinates.
    /// Wraps cSapModel.PointObj.AddCartesian.
    /// </summary>
    /// <param name="x">X coordinate in current length units</param>
    /// <param name="y">Y coordinate in current length units</param>
    /// <param name="z">Z coordinate (elevation) in current length units</param>
    /// <param name="userName">Optional custom name for the point (auto-generated if empty)</param>
    /// <param name="csys">Coordinate system name (default: "Global")</param>
    /// <returns>The name of the created point object</returns>
    /// <exception cref="EtabsException">If ETABS API fails</exception>
    public string AddPoint(double x, double y, double z, string userName = "", string csys = "Global")
    {
        try
        {
            // Validate parameters
            if (string.IsNullOrEmpty(csys))
                throw new ArgumentException("Coordinate system cannot be null or empty", nameof(csys));

            string pointName = userName;
            int ret = _sapModel.PointObj.AddCartesian(x, y, z, ref pointName, userName, csys);

            if (ret != 0)
                throw new EtabsException(ret, "AddCartesian", $"Failed to add point at ({x}, {y}, {z})");

            _logger.LogDebug("Added point {PointName} at ({X}, {Y}, {Z}) in {CoordSys}", 
                pointName, x, y, z, csys);

            return pointName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error adding point at ({x}, {y}, {z}): {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Changes the name of an existing point object.
    /// Wraps cSapModel.PointObj.ChangeName.
    /// </summary>
    /// <param name="currentName">Current name of the point</param>
    /// <param name="newName">New name for the point</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int ChangeName(string currentName, string newName)
    {
        try
        {
            if (string.IsNullOrEmpty(currentName))
                throw new ArgumentException("Current name cannot be null or empty", nameof(currentName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New name cannot be null or empty", nameof(newName));

            int ret = _sapModel.PointObj.ChangeName(currentName, newName);

            if (ret != 0)
                throw new EtabsException(ret, "ChangeName", $"Failed to change point name from '{currentName}' to '{newName}'");

            _logger.LogDebug("Changed point name from {OldName} to {NewName}", currentName, newName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error changing point name from '{currentName}' to '{newName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a point object with its coordinates and properties.
    /// Wraps cSapModel.PointObj.GetCoordCartesian and other methods.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Point model with coordinates and properties</returns>
    public Point GetPoint(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            double x = 0, y = 0, z = 0;
            int ret = _sapModel.PointObj.GetCoordCartesian(pointName, ref x, ref y, ref z);

            if (ret != 0)
                throw new EtabsException(ret, "GetCoordCartesian", $"Failed to get coordinates for point '{pointName}'");

            var point = new Point
            {
                Name = pointName,
                X = x,
                Y = y,
                Z = z
            };

            // Get additional properties
            try
            {
                string label = "", story = "";
                ret = _sapModel.PointObj.GetLabelFromName(pointName, ref label, ref story);
                if (ret == 0)
                {
                    point.Label = label;
                    point.Story = story;
                }
            }
            catch
            {
                // Ignore errors getting label/story - not critical
            }

            try
            {
                bool isSpecial = false;
                ret = _sapModel.PointObj.GetSpecialPoint(pointName, ref isSpecial);
                if (ret == 0)
                {
                    point.IsSpecial = isSpecial;
                }
            }
            catch
            {
                // Ignore errors getting special point status
            }

            try
            {
                string guid = "";
                ret = _sapModel.PointObj.GetGUID(pointName, ref guid);
                if (ret == 0)
                {
                    point.GUID = guid;
                }
            }
            catch
            {
                // Ignore errors getting GUID
            }

            return point;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets all point objects with their coordinates and properties.
    /// Wraps cSapModel.PointObj.GetAllPoints.
    /// </summary>
    /// <returns>List of Point models</returns>
    public List<Point> GetAllPoints()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;
            double[] x = null, y = null, z = null;

            int ret = _sapModel.PointObj.GetAllPoints(ref numberNames, ref names, ref x, ref y, ref z);

            if (ret != 0)
                throw new EtabsException(ret, "GetAllPoints", "Failed to get all points");

            var points = new List<Point>();
            
            for (int i = 0; i < numberNames && i < (names?.Length ?? 0); i++)
            {
                var point = new Point
                {
                    Name = names[i],
                    X = i < (x?.Length ?? 0) ? x[i] : 0,
                    Y = i < (y?.Length ?? 0) ? y[i] : 0,
                    Z = i < (z?.Length ?? 0) ? z[i] : 0
                };
                points.Add(point);
            }

            _logger.LogDebug("Retrieved {Count} points", points.Count);
            return points;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting all points: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves the names of all defined point objects in the model.
    /// Wraps cSapModel.PointObj.GetNameList.
    /// </summary>
    /// <returns>Array of point object names</returns>
    public string[] GetNameList()
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.PointObj.GetNameList(ref numberNames, ref names);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get point name list");

            _logger.LogDebug("Retrieved {Count} point names", numberNames);

            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting point name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of point objects in the model.
    /// Wraps cSapModel.PointObj.Count.
    /// </summary>
    /// <returns>Total number of points</returns>
    public int Count()
    {
        try
        {
            int count = _sapModel.PointObj.Count();
            _logger.LogDebug("Point count: {Count}", count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting point count: {ex.Message}", ex);
        }
    }

    #endregion

    // Additional methods are implemented in partial class files:
    // - PointObjectManager.Restraints.cs (Restraint and Support methods)
    // - PointObjectManager.Springs.cs (Spring methods)  
    // - PointObjectManager.Loads.cs (Load methods)
    // - PointObjectManager.Properties.cs (Mass, Diaphragm, Connectivity, Group, Selection methods)
}