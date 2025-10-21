using EtabSharp.Elements.AreaObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.AreaObj;

/// <summary>
/// AreaObjectManager partial class - Properties and Material Methods
/// </summary>
public partial class AreaObjectManager
{
    #region Property Methods

    /// <summary>
    /// Sets the area property for an area object.
    /// Wraps cSapModel.AreaObj.SetProperty.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="propertyName">Name of the area property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetProperty(string areaName, string propertyName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int ret = _sapModel.AreaObj.SetProperty(areaName, propertyName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetProperty", $"Failed to set property '{propertyName}' for area '{areaName}'");

            _logger.LogDebug("Set property {PropertyName} for area {AreaName}", propertyName, areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting property for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the area property assigned to an area object.
    /// Wraps cSapModel.AreaObj.GetProperty.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned area property</returns>
    public string GetProperty(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            string propertyName = "";
            int ret = _sapModel.AreaObj.GetProperty(areaName, ref propertyName);

            if (ret != 0)
                throw new EtabsException(ret, "GetProperty", $"Failed to get property for area '{areaName}'");

            return propertyName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting property for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Material Methods

    /// <summary>
    /// Sets material property override for an area.
    /// Wraps cSapModel.AreaObj.SetMaterialOverwrite.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="materialName">Name of the material property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetMaterialOverwrite(string areaName, string materialName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(materialName))
                throw new ArgumentException("Material name cannot be null or empty", nameof(materialName));

            int ret = _sapModel.AreaObj.SetMaterialOverwrite(areaName, materialName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetMaterialOverwrite", $"Failed to set material override '{materialName}' for area '{areaName}'");

            _logger.LogDebug("Set material override {MaterialName} for area {AreaName}", materialName, areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting material override for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets material property override for an area.
    /// Wraps cSapModel.AreaObj.GetMaterialOverwrite.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the material override</returns>
    public string GetMaterialOverwrite(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            string materialName = "";
            int ret = _sapModel.AreaObj.GetMaterialOverwrite(areaName, ref materialName);

            if (ret != 0)
                throw new EtabsException(ret, "GetMaterialOverwrite", $"Failed to get material override for area '{areaName}'");

            return materialName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting material override for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Local Axes Methods

    /// <summary>
    /// Sets local axes for an area object.
    /// Wraps cSapModel.AreaObj.SetLocalAxes.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="angleInDegrees">Angle in degrees for local axis rotation</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLocalAxes(string areaName, double angleInDegrees, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.SetLocalAxes(areaName, angleInDegrees, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetLocalAxes", $"Failed to set local axes for area '{areaName}'");

            _logger.LogDebug("Set local axes angle {Angle}° for area {AreaName}", angleInDegrees, areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting local axes for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets local axes for an area object.
    /// Wraps cSapModel.AreaObj.GetLocalAxes.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Tuple of (Angle, IsAdvanced)</returns>
    public (double Angle, bool IsAdvanced) GetLocalAxes(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            double angle = 0;
            bool advanced = false;
            int ret = _sapModel.AreaObj.GetLocalAxes(areaName, ref angle, ref advanced);

            if (ret != 0)
                throw new EtabsException(ret, "GetLocalAxes", $"Failed to get local axes for area '{areaName}'");

            return (angle, advanced);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting local axes for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Modifier Methods

    /// <summary>
    /// Sets property modifiers for an area object.
    /// Wraps cSapModel.AreaObj.SetModifiers.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="modifiers">AreaModifiers model with modifier values</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetModifiers(string areaName, AreaModifiers modifiers, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (modifiers == null)
                throw new ArgumentNullException(nameof(modifiers));

            double[] modifierArray = modifiers.ToArray();
            int ret = _sapModel.AreaObj.SetModifiers(areaName, ref modifierArray, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetModifiers", $"Failed to set modifiers for area '{areaName}'");

            _logger.LogDebug("Set modifiers for area {AreaName}", areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting modifiers for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets property modifiers for an area object.
    /// Wraps cSapModel.AreaObj.GetModifiers.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>AreaModifiers model with modifier values</returns>
    public AreaModifiers GetModifiers(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            double[] modifierArray = new double[10];
            int ret = _sapModel.AreaObj.GetModifiers(areaName, ref modifierArray);

            if (ret != 0)
                throw new EtabsException(ret, "GetModifiers", $"Failed to get modifiers for area '{areaName}'");

            return AreaModifiers.FromArray(modifierArray);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting modifiers for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes property modifiers for an area object.
    /// Wraps cSapModel.AreaObj.DeleteModifiers.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteModifiers(string areaName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.DeleteModifiers(areaName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteModifiers", $"Failed to delete modifiers for area '{areaName}'");

            _logger.LogDebug("Deleted modifiers for area {AreaName}", areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting modifiers for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Mass and Spring Methods

    /// <summary>
    /// Sets mass per unit area for an area object.
    /// Wraps cSapModel.AreaObj.SetMass.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="massPerArea">Mass per unit area</param>
    /// <param name="replace">If true, replaces existing; if false, adds to existing</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetMass(string areaName, double massPerArea, bool replace = false, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.SetMass(areaName, massPerArea, replace, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetMass", $"Failed to set mass for area '{areaName}'");

            _logger.LogDebug("Set mass {Mass} per unit area for area {AreaName}", massPerArea, areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting mass for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets mass per unit area for an area object.
    /// Wraps cSapModel.AreaObj.GetMass.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Mass per unit area</returns>
    public double GetMass(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            double massPerArea = 0;
            int ret = _sapModel.AreaObj.GetMass(areaName, ref massPerArea);

            if (ret != 0)
                throw new EtabsException(ret, "GetMass", $"Failed to get mass for area '{areaName}'");

            return massPerArea;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting mass for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes mass assignment from an area object.
    /// Wraps cSapModel.AreaObj.DeleteMass.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteMass(string areaName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.DeleteMass(areaName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteMass", $"Failed to delete mass for area '{areaName}'");

            _logger.LogDebug("Deleted mass for area {AreaName}", areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting mass for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets spring assignment for an area object.
    /// Wraps cSapModel.AreaObj.SetSpringAssignment.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="springPropertyName">Name of the spring property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSpringAssignment(string areaName, string springPropertyName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(springPropertyName))
                throw new ArgumentException("Spring property name cannot be null or empty", nameof(springPropertyName));

            int ret = _sapModel.AreaObj.SetSpringAssignment(areaName, springPropertyName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetSpringAssignment", $"Failed to set spring assignment for area '{areaName}'");

            _logger.LogDebug("Set spring assignment {SpringProperty} for area {AreaName}", springPropertyName, areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting spring assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets spring assignment for an area object.
    /// Wraps cSapModel.AreaObj.GetSpringAssignment.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned spring property</returns>
    public string GetSpringAssignment(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            string springProperty = "";
            int ret = _sapModel.AreaObj.GetSpringAssignment(areaName, ref springProperty);

            if (ret != 0)
                throw new EtabsException(ret, "GetSpringAssignment", $"Failed to get spring assignment for area '{areaName}'");

            return springProperty;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting spring assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes spring assignment from an area object.
    /// Wraps cSapModel.AreaObj.DeleteSpring.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteSpring(string areaName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.DeleteSpring(areaName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteSpring", $"Failed to delete spring assignment for area '{areaName}'");

            _logger.LogDebug("Deleted spring assignment for area {AreaName}", areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting spring assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Advanced Methods

    /// <summary>
    /// Gets the GUID for an area object.
    /// Wraps cSapModel.AreaObj.GetGUID.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>GUID string</returns>
    public string GetGUID(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            string guid = "";
            int ret = _sapModel.AreaObj.GetGUID(areaName, ref guid);

            if (ret != 0)
                throw new EtabsException(ret, "GetGUID", $"Failed to get GUID for area '{areaName}'");

            return guid;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting GUID for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets the GUID for an area object.
    /// Wraps cSapModel.AreaObj.SetGUID.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="guid">GUID string (empty to auto-generate)</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetGUID(string areaName, string guid = "")
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.SetGUID(areaName, guid);

            if (ret != 0)
                throw new EtabsException(ret, "SetGUID", $"Failed to set GUID for area '{areaName}'");

            _logger.LogDebug("Set GUID for area {AreaName}: {GUID}", areaName, guid);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting GUID for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the transformation matrix for an area object.
    /// Wraps cSapModel.AreaObj.GetTransformationMatrix.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="isGlobal">True for global coordinates, false for local</param>
    /// <returns>Transformation matrix values</returns>
    public double[] GetTransformationMatrix(string areaName, bool isGlobal = true)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            double[] matrix = new double[16];
            int ret = _sapModel.AreaObj.GetTransformationMatrix(areaName, ref matrix, isGlobal);

            if (ret != 0)
                throw new EtabsException(ret, "GetTransformationMatrix", $"Failed to get transformation matrix for area '{areaName}'");

            return matrix;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting transformation matrix for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets element information for an area object.
    /// Wraps cSapModel.AreaObj.GetElm.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of element names</returns>
    public string[] GetElements(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberElements = 0;
            string[] elements = null;

            int ret = _sapModel.AreaObj.GetElm(areaName, ref numberElements, ref elements);

            if (ret != 0)
                throw new EtabsException(ret, "GetElm", $"Failed to get elements for area '{areaName}'");

            return elements ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting elements for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets point connectivity for an area object.
    /// Wraps cSapModel.AreaObj.GetPoints.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Array of point names defining the area boundary</returns>
    public string[] GetPoints(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberPoints = 0;
            string[] points = null;

            int ret = _sapModel.AreaObj.GetPoints(areaName, ref numberPoints, ref points);

            if (ret != 0)
                throw new EtabsException(ret, "GetPoints", $"Failed to get points for area '{areaName}'");

            return points ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting points for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets offsets for area object points.
    /// Wraps cSapModel.AreaObj.GetOffsets3.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>AreaOffsets model with offset information</returns>
    public AreaOffsets GetOffsets(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numberPoints = 0;
            double[] offsets = null;

            int ret = _sapModel.AreaObj.GetOffsets3(areaName, ref numberPoints, ref offsets);

            if (ret != 0)
                throw new EtabsException(ret, "GetOffsets3", $"Failed to get offsets for area '{areaName}'");

            return new AreaOffsets
            {
                AreaName = areaName,
                Offsets = offsets?.ToList() ?? new List<double>()
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting offsets for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets curved edge information for an area object.
    /// Wraps cSapModel.AreaObj.GetCurvedEdges.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>AreaCurvedEdges model with curved edge information</returns>
    public AreaCurvedEdges GetCurvedEdges(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int numEdges = 0;
            int[] curveType = null;
            double[] tension = null;
            int[] numPoints = null;
            double[] gx = null, gy = null, gz = null;

            int ret = _sapModel.AreaObj.GetCurvedEdges(areaName, ref numEdges, ref curveType, ref tension, 
                ref numPoints, ref gx, ref gy, ref gz);

            if (ret != 0)
                throw new EtabsException(ret, "GetCurvedEdges", $"Failed to get curved edges for area '{areaName}'");

            var curvedEdges = new AreaCurvedEdges { AreaName = areaName };

            int pointIndex = 0;
            for (int i = 0; i < numEdges; i++)
            {
                var edge = new CurvedEdge
                {
                    CurveType = curveType[i],
                    Tension = tension[i]
                };

                int edgePointCount = numPoints[i];
                for (int j = 0; j < edgePointCount && pointIndex < gx.Length; j++, pointIndex++)
                {
                    edge.Points.Add(new AreaCoordinate(gx[pointIndex], gy[pointIndex], gz[pointIndex]));
                }

                curvedEdges.Edges.Add(edge);
            }

            return curvedEdges;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting curved edges for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion
}