using EtabSharp.Elements.FrameObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;

namespace EtabSharp.Elements.FrameObj;

/// <summary>
/// FrameObjectManager partial class - Properties and Section Methods
/// </summary>
public partial class FrameObjectManager
{
    #region Section Properties

    /// <summary>
    /// Assigns a frame section property to a frame object.
    /// Wraps cSapModel.FrameObj.SetSection.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="sectionName">Name of the frame section property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <param name="sVarRelStartLoc">Relative start location for non-prismatic sections</param>
    /// <param name="sVarTotalLength">Total length for non-prismatic sections</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSection(string frameName, string sectionName, eItemType itemType = eItemType.Objects, 
        double sVarRelStartLoc = 0, double sVarTotalLength = 0)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (string.IsNullOrEmpty(sectionName))
                throw new ArgumentException("Section name cannot be null or empty", nameof(sectionName));

            int ret = _sapModel.FrameObj.SetSection(frameName, sectionName, itemType, sVarRelStartLoc, sVarTotalLength);

            if (ret != 0)
                throw new EtabsException(ret, "SetSection", $"Failed to set section '{sectionName}' for frame '{frameName}'");

            _logger.LogDebug("Set section {SectionName} for frame {FrameName}", sectionName, frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting section for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the section property assigned to a frame object.
    /// Wraps cSapModel.FrameObj.GetSection.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (SectionName, AutoSelectList)</returns>
    public (string SectionName, string AutoSelectList) GetSection(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string sectionName = "", autoSelectList = "";
            int ret = _sapModel.FrameObj.GetSection(frameName, ref sectionName, ref autoSelectList);

            if (ret != 0)
                throw new EtabsException(ret, "GetSection", $"Failed to get section for frame '{frameName}'");

            return (sectionName, autoSelectList);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting section for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets non-prismatic section data for a frame.
    /// Wraps cSapModel.FrameObj.GetSectionNonPrismatic.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (SectionName, TotalLength, RelStartLoc)</returns>
    public (string SectionName, double TotalLength, double RelStartLoc) GetSectionNonPrismatic(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string sectionName = "";
            double totalLength = 0, relStartLoc = 0;
            int ret = _sapModel.FrameObj.GetSectionNonPrismatic(frameName, ref sectionName, ref totalLength, ref relStartLoc);

            if (ret != 0)
                throw new EtabsException(ret, "GetSectionNonPrismatic", $"Failed to get non-prismatic section for frame '{frameName}'");

            return (sectionName, totalLength, relStartLoc);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting non-prismatic section for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Material Properties

    /// <summary>
    /// Sets material property override for a frame.
    /// Wraps cSapModel.FrameObj.SetMaterialOverwrite.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="materialName">Name of the material property</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetMaterialOverwrite(string frameName, string materialName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (string.IsNullOrEmpty(materialName))
                throw new ArgumentException("Material name cannot be null or empty", nameof(materialName));

            int ret = _sapModel.FrameObj.SetMaterialOverwrite(frameName, materialName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetMaterialOverwrite", $"Failed to set material override '{materialName}' for frame '{frameName}'");

            _logger.LogDebug("Set material override {MaterialName} for frame {FrameName}", materialName, frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting material override for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets material property override for a frame.
    /// Wraps cSapModel.FrameObj.GetMaterialOverwrite.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Name of the material override (empty if none)</returns>
    public string GetMaterialOverwrite(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            string materialName = "";
            int ret = _sapModel.FrameObj.GetMaterialOverwrite(frameName, ref materialName);

            if (ret != 0)
                throw new EtabsException(ret, "GetMaterialOverwrite", $"Failed to get material override for frame '{frameName}'");

            return materialName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting material override for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Local Axes and Orientation

    /// <summary>
    /// Sets the local axis angle for a frame object.
    /// Wraps cSapModel.FrameObj.SetLocalAxes.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="angleInDegrees">Angle in degrees for local axis rotation</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetLocalAxes(string frameName, double angleInDegrees, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetLocalAxes(frameName, angleInDegrees, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetLocalAxes", $"Failed to set local axes for frame '{frameName}'");

            _logger.LogDebug("Set local axes angle {Angle}° for frame {FrameName}", angleInDegrees, frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting local axes for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the local axis angle for a frame object.
    /// Wraps cSapModel.FrameObj.GetLocalAxes.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (Angle in degrees, IsAdvanced)</returns>
    public (double Angle, bool IsAdvanced) GetLocalAxes(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            double angle = 0;
            bool advanced = false;
            int ret = _sapModel.FrameObj.GetLocalAxes(frameName, ref angle, ref advanced);

            if (ret != 0)
                throw new EtabsException(ret, "GetLocalAxes", $"Failed to get local axes for frame '{frameName}'");

            return (angle, advanced);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting local axes for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the design orientation for a frame object.
    /// Wraps cSapModel.FrameObj.GetDesignOrientation.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Design orientation enumeration</returns>
    public eFrameDesignOrientation GetDesignOrientation(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            eFrameDesignOrientation orientation = eFrameDesignOrientation.Other;
            int ret = _sapModel.FrameObj.GetDesignOrientation(frameName, ref orientation);

            if (ret != 0)
                throw new EtabsException(ret, "GetDesignOrientation", $"Failed to get design orientation for frame '{frameName}'");

            return orientation;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting design orientation for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Insertion Point and Offsets

    /// <summary>
    /// Sets the insertion point and end offsets for a frame object.
    /// Wraps cSapModel.FrameObj.SetInsertionPoint.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="cardinalPoint">Cardinal point (1-11)</param>
    /// <param name="mirror2">Mirror about local 2-axis</param>
    /// <param name="mirror3">Mirror about local 3-axis</param>
    /// <param name="stiffTransform">Transform stiffness</param>
    /// <param name="offset1">I-end offsets [x, y, z]</param>
    /// <param name="offset2">J-end offsets [x, y, z]</param>
    /// <param name="csys">Coordinate system for offsets</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetInsertionPoint(string frameName, int cardinalPoint, bool mirror2, bool mirror3, 
        bool stiffTransform, double[] offset1, double[] offset2, string csys = "Local", 
        eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (offset1 == null || offset1.Length != 3)
                throw new ArgumentException("Offset1 must be an array of 3 values", nameof(offset1));
            if (offset2 == null || offset2.Length != 3)
                throw new ArgumentException("Offset2 must be an array of 3 values", nameof(offset2));

            int ret = _sapModel.FrameObj.SetInsertionPoint(frameName, cardinalPoint, mirror2, mirror3, 
                stiffTransform, ref offset1, ref offset2, csys, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetInsertionPoint", $"Failed to set insertion point for frame '{frameName}'");

            _logger.LogDebug("Set insertion point {CardinalPoint} for frame {FrameName}", cardinalPoint, frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting insertion point for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the insertion point and end offsets for a frame object.
    /// Wraps cSapModel.FrameObj.GetInsertionPoint.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>FrameInsertionPoint model with insertion point data</returns>
    public FrameInsertionPoint GetInsertionPoint(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int cardinalPoint = 0;
            bool mirror2 = false, mirror3 = false, stiffTransform = false;
            double[] offset1 = new double[3];
            double[] offset2 = new double[3];
            string csys = "";

            int ret = _sapModel.FrameObj.GetInsertionPoint(frameName, ref cardinalPoint, ref mirror2, ref mirror3, 
                ref stiffTransform, ref offset1, ref offset2, ref csys);

            if (ret != 0)
                throw new EtabsException(ret, "GetInsertionPoint", $"Failed to get insertion point for frame '{frameName}'");

            return new FrameInsertionPoint
            {
                CardinalPoint = cardinalPoint,
                Mirror2 = mirror2,
                Mirror3 = mirror3,
                StiffnessTransform = stiffTransform,
                IEndOffset = offset1,
                JEndOffset = offset2,
                CoordinateSystem = csys
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting insertion point for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets end length offsets for a frame object.
    /// Wraps cSapModel.FrameObj.SetEndLengthOffset.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="autoOffset">Use automatic offset calculation</param>
    /// <param name="length1">I-end length offset</param>
    /// <param name="length2">J-end length offset</param>
    /// <param name="rz">Rigid zone factor</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetEndLengthOffset(string frameName, bool autoOffset, double length1, double length2, 
        double rz, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.SetEndLengthOffset(frameName, autoOffset, length1, length2, rz, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetEndLengthOffset", $"Failed to set end length offset for frame '{frameName}'");

            _logger.LogDebug("Set end length offset for frame {FrameName}: Auto={Auto}, L1={L1}, L2={L2}, RZ={RZ}", 
                frameName, autoOffset, length1, length2, rz);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting end length offset for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets end length offsets for a frame object.
    /// Wraps cSapModel.FrameObj.GetEndLengthOffset.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Tuple of (AutoOffset, Length1, Length2, RZ)</returns>
    public (bool AutoOffset, double Length1, double Length2, double RZ) GetEndLengthOffset(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            bool autoOffset = false;
            double length1 = 0, length2 = 0, rz = 0;

            int ret = _sapModel.FrameObj.GetEndLengthOffset(frameName, ref autoOffset, ref length1, ref length2, ref rz);

            if (ret != 0)
                throw new EtabsException(ret, "GetEndLengthOffset", $"Failed to get end length offset for frame '{frameName}'");

            return (autoOffset, length1, length2, rz);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting end length offset for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for a frame object.
    /// Wraps cSapModel.FrameObj.SetModifiers.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="modifiers">Array of 8 modifier values [Area, As2, As3, Torsion, I22, I33, Mass, Weight]</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetModifiers(string frameName, double[] modifiers, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));
            if (modifiers == null || modifiers.Length != 8)
                throw new ArgumentException("Modifiers must be an array of 8 values", nameof(modifiers));

            int ret = _sapModel.FrameObj.SetModifiers(frameName, ref modifiers, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetModifiers", $"Failed to set modifiers for frame '{frameName}'");

            _logger.LogDebug("Set modifiers for frame {FrameName}", frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting modifiers for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets property modifiers for a frame object.
    /// Wraps cSapModel.FrameObj.GetModifiers.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <returns>Array of 8 modifier values [Area, As2, As3, Torsion, I22, I33, Mass, Weight]</returns>
    public double[] GetModifiers(string frameName)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            double[] modifiers = new double[8];
            int ret = _sapModel.FrameObj.GetModifiers(frameName, ref modifiers);

            if (ret != 0)
                throw new EtabsException(ret, "GetModifiers", $"Failed to get modifiers for frame '{frameName}'");

            return modifiers;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting modifiers for frame '{frameName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes property modifiers for a frame object (resets to 1.0).
    /// Wraps cSapModel.FrameObj.DeleteModifiers.
    /// </summary>
    /// <param name="frameName">Name of the frame object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteModifiers(string frameName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(frameName))
                throw new ArgumentException("Frame name cannot be null or empty", nameof(frameName));

            int ret = _sapModel.FrameObj.DeleteModifiers(frameName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteModifiers", $"Failed to delete modifiers for frame '{frameName}'");

            _logger.LogDebug("Deleted modifiers for frame {FrameName}", frameName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting modifiers for frame '{frameName}': {ex.Message}", ex);
        }
    }

    #endregion
}