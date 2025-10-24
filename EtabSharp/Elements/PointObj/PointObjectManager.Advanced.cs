using EtabSharp.Elements.PointObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.PointObj;

/// <summary>
/// PointObjectManager partial class - Advanced Methods
/// Contains coordinate transformations, local axes, panel zones, and other advanced features.
/// </summary>
public partial class PointObjectManager
{
    #region Coordinate System Methods

    /// <summary>
    /// Gets point coordinates in cylindrical coordinate system.
    /// Wraps cSapModel.PointObj.GetCoordCylindrical.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="csys">Coordinate system name (default: "Global")</param>
    /// <returns>Tuple of (R, Theta, Z) where R is radial distance, Theta is angle in degrees, Z is height</returns>
    public (double R, double Theta, double Z) GetCoordCylindrical(string pointName, string csys = "Global")
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            double r = 0, theta = 0, z = 0;
            int ret = _sapModel.PointObj.GetCoordCylindrical(pointName, ref r, ref theta, ref z, csys);

            if (ret != 0)
                throw new EtabsException(ret, "GetCoordCylindrical", $"Failed to get cylindrical coordinates for point '{pointName}'");

            _logger.LogDebug("Retrieved cylindrical coordinates for point {PointName}: R={R}, Theta={Theta}, Z={Z}", 
                pointName, r, theta, z);

            return (r, theta, z);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting cylindrical coordinates for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets point coordinates in spherical coordinate system.
    /// Wraps cSapModel.PointObj.GetCoordSpherical.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="csys">Coordinate system name (default: "Global")</param>
    /// <returns>Tuple of (R, A, B) where R is radial distance, A and B are angles in degrees</returns>
    public (double R, double A, double B) GetCoordSpherical(string pointName, string csys = "Global")
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            double r = 0, a = 0, b = 0;
            int ret = _sapModel.PointObj.GetCoordSpherical(pointName, ref r, ref a, ref b, csys);

            if (ret != 0)
                throw new EtabsException(ret, "GetCoordSpherical", $"Failed to get spherical coordinates for point '{pointName}'");

            _logger.LogDebug("Retrieved spherical coordinates for point {PointName}: R={R}, A={A}, B={B}", 
                pointName, r, a, b);

            return (r, a, b);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting spherical coordinates for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Local Axes Methods

    /// <summary>
    /// Gets the local axes orientation angles for a point object.
    /// Wraps cSapModel.PointObj.GetLocalAxes.
    /// The local axes are defined by rotating from global axes:
    /// 1. Rotate about Z axis by angle A
    /// 2. Rotate about resulting Y axis by angle B
    /// 3. Rotate about resulting X axis by angle C
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>Tuple of (A, B, C, Advanced) where angles are in degrees</returns>
    public (double A, double B, double C, bool Advanced) GetLocalAxes(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            double a = 0, b = 0, c = 0;
            bool advanced = false;

            int ret = _sapModel.PointObj.GetLocalAxes(pointName, ref a, ref b, ref c, ref advanced);

            if (ret != 0)
                throw new EtabsException(ret, "GetLocalAxes", $"Failed to get local axes for point '{pointName}'");

            _logger.LogDebug("Retrieved local axes for point {PointName}: A={A}°, B={B}°, C={C}°, Advanced={Advanced}", 
                pointName, a, b, c, advanced);

            return (a, b, c, advanced);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting local axes for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the transformation matrix for a point object.
    /// Wraps cSapModel.PointObj.GetTransformationMatrix.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="isGlobal">If true, returns global-to-local transformation; if false, returns local-to-global</param>
    /// <returns>Transformation matrix as double array</returns>
    public double[] GetTransformationMatrix(string pointName, bool isGlobal = true)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            double[] matrix = new double[9]; // 3x3 matrix
            int ret = _sapModel.PointObj.GetTransformationMatrix(pointName, ref matrix, isGlobal);

            if (ret != 0)
                throw new EtabsException(ret, "GetTransformationMatrix", $"Failed to get transformation matrix for point '{pointName}'");

            _logger.LogDebug("Retrieved transformation matrix for point {PointName} (IsGlobal={IsGlobal})", 
                pointName, isGlobal);

            return matrix;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting transformation matrix for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Panel Zone Methods

    /// <summary>
    /// Gets panel zone assignment for a point object.
    /// Wraps cSapModel.PointObj.GetPanelZone.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointPanelZone model with panel zone properties, or null if no panel zone assigned</returns>
    public PointPanelZone? GetPanelZone(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int propType = 0;
            double thickness = 0, k1 = 0, k2 = 0;
            string linkProp = "";
            int connectivity = 0, localAxisFrom = 0;
            double localAxisAngle = 0;

            int ret = _sapModel.PointObj.GetPanelZone(pointName, ref propType, ref thickness, ref k1, ref k2, 
                ref linkProp, ref connectivity, ref localAxisFrom, ref localAxisAngle);

            if (ret != 0)
                return null; // No panel zone assigned

            var panelZone = new PointPanelZone
            {
                PointName = pointName,
                PropertyType = propType,
                Thickness = thickness,
                K1 = k1,
                K2 = k2,
                LinkProperty = linkProp,
                Connectivity = connectivity,
                LocalAxisFrom = localAxisFrom,
                LocalAxisAngle = localAxisAngle
            };

            _logger.LogDebug("Retrieved panel zone for point {PointName}: Type={PropType}", pointName, propType);

            return panelZone;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting panel zone for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets panel zone assignment for a point object.
    /// Wraps cSapModel.PointObj.SetPanelZone.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="panelZone">PointPanelZone model with panel zone properties</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetPanelZone(string pointName, PointPanelZone panelZone, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (panelZone == null)
                throw new ArgumentNullException(nameof(panelZone));

            int ret = _sapModel.PointObj.SetPanelZone(
                pointName,
                panelZone.PropertyType,
                panelZone.Thickness,
                panelZone.K1,
                panelZone.K2,
                panelZone.LinkProperty,
                panelZone.Connectivity,
                panelZone.LocalAxisFrom,
                panelZone.LocalAxisAngle,
                itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetPanelZone", $"Failed to set panel zone for point '{pointName}'");

            _logger.LogDebug("Set panel zone for point {PointName}: Type={PropType}", pointName, panelZone.PropertyType);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting panel zone for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes panel zone assignment from a point object.
    /// Wraps cSapModel.PointObj.DeletePanelZone.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeletePanelZone(string pointName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int ret = _sapModel.PointObj.DeletePanelZone(pointName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeletePanelZone", $"Failed to delete panel zone for point '{pointName}'");

            _logger.LogDebug("Deleted panel zone for point {PointName}", pointName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting panel zone for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of points with panel zone assignments.
    /// /// Wraps cSapModel.PointObj.CountPanelZone.
    /// </summary>
    /// <returns>Number of points with panezones</returns>
    public int CountPanelZone()
    {
        try
        {
            int count = _sapModel.PointObj.CountPanelZone();
            _logger.LogDebug("Panel zone count: {Count}", count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting panel zone count: {ex.Message}", ex);
        }
    }

    #endregion

    #region Mass Assignment Advanced Methods

    /// <summary>
    /// Assigns mass to a point based on volume and material density.
    /// Wraps cSapModel.PointObj.SetMassByVolume.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="materialProperty">Name of the material property</param>
    /// <param name="mass">PointMass model with mass values</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetMassByVolume(string pointName, string materialProperty, PointMass mass, 
        eItemType itemType = eItemType.Objects, bool replace = false)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (string.IsNullOrEmpty(materialProperty))
                throw new ArgumentException("Material property cannot be null or empty", nameof(materialProperty));
            if (mass == null)
                throw new ArgumentNullException(nameof(mass));

            double[] massArray = mass.ToArray();
            int ret = _sapModel.PointObj.SetMassByVolume(pointName, materialProperty, ref massArray, 
                itemType, mass.IsLocalCSys, replace);

            if (ret != 0)
                throw new EtabsException(ret, "SetMassByVolume", $"Failed to set mass by volume for point '{pointName}'");

            _logger.LogDebug("Set mass by volume for point {PointName} using material {Material}", 
                pointName, materialProperty);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting mass by volume for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Assigns mass to a point based on weight.
    /// Wraps cSapModel.PointObj.SetMassByWeight.
    /// </summary>
    /// /// <param name="pointName">Name of the point object</param>
    /// <param name="mass">PointMass model with mass values</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <param name="replace">If true, replisting; if false, adds to existing</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetMassByWeight(string pointName, PointMass mass, 
        eItemType itemType = eItemType.Objects, bool replace = false)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (mass == null)
                throw new ArgumentNullException(nameof(mass));

            double[] massArray = mass.ToArray();
            int ret = _sapModel.PointObj.SetMassByWeight(pointName, ref massArray, itemType, mass.IsLocalCSys, replace);

            if (ret != 0)
                throw new EtabsException(ret, "SetMassByWeight", $"Failed to set mass by weight for point '{pointName}'");

            _logger.LogDebug("Set mass by weight for point {PointName}", pointName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting mass by weight for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Count Methods

    /// <summary>
    /// Gets the count of points with restraint assignments.
    /// Wraps cSapModel.PointObj.CountRestraint.
    /// </summary>
    /// <returns>Number of points with restraints</returns>
    public int CountRestraint()
    {
        try
        {
            int count = _sapModel.PointObj.CountRestraint();
            _logger.LogDebug("Restraint count: {Count}", count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting restraint count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of points with spring assignments.
    /// Wraps cSapModel.PointObj.CountSpring.
    /// </summary>
    /// <returns>Number of points with springs</returns>
    public int CountSpring()
    {
        try
        {
            int count = _sapModel.PointObj.CountSpring();
            _logger.LogDebug("Spring count: {Count}", count);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting spring count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of force load assignments for a point.
    /// Wraps cSapModel.PointObj.CountLoadForce.
    /// </summary>
    /// <param name="pointName">Name of the point object (empty for all points)</param>
    /// <param name="loadPattern">Name of the load pattern (empty for all patterns)</param>
    /// <returns>Number of force load assignments</returns>
    public int CountLoadForce(string pointName = "", string loadPattern = "")
    {
        try
        {
            int count = 0;
            int ret = _sapModel.PointObj.CountLoadForce(ref count, pointName, loadPattern);

            if (ret != 0)
                throw new EtabsException(ret, "CountLoadForce", "Failed to get force load count");

            _logger.LogDebug("Force load count: {Count}", count);
            return count;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting force load count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of displacement load assignments for a point.
    /// Wraps cSapModel.PointObj.CountLoadDispl.
    /// </summary>
    /// <param name="pointName">Name of the point object (empty for all points)</param>
    /// <param name="loadPattern">Name of the load pattern (empty for all patterns)</param>
    /// <returns>Number of displacement load assignments</returns>
    public int CountLoadDispl(string pointName = "", string loadPattern = "")
    {
        try
        {
            int count = 0;
            int ret = _sapModel.PointObj.CountLoadDispl(ref count, pointName, loadPattern);

            if (ret != 0)
                throw new EtabsException(ret, "CountLoadDispl", "Failed to get displacement load count");

            _logger.LogDebug("Displacement load count: {Count}", count);
            return count;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting displacement load count: {ex.Message}", ex);
        }
    }

    #endregion
}
