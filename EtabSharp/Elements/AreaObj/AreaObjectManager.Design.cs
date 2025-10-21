using EtabSharp.Elements.AreaObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.AreaObj;

/// <summary>
/// AreaObjectManager partial class - Design and Analysis Methods
/// </summary>
public partial class AreaObjectManager
{
    #region Design Orientation Methods

    /// <summary>
    /// Gets the design orientation for an area object.
    /// Wraps cSapModel.AreaObj.GetDesignOrientation.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Design orientation enumeration</returns>
    public eAreaDesignOrientation GetDesignOrientation(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            eAreaDesignOrientation orientation = eAreaDesignOrientation.Null;
            int ret = _sapModel.AreaObj.GetDesignOrientation(areaName, ref orientation);

            if (ret != 0)
                throw new EtabsException(ret, "GetDesignOrientation", $"Failed to get design orientation for area '{areaName}'");

            return orientation;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting design orientation for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Edge Constraint Methods

    /// <summary>
    /// Sets edge constraint for an area object.
    /// Wraps cSapModel.AreaObj.SetEdgeConstraint.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="constraintExists">True if constraint exists</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetEdgeConstraint(string areaName, bool constraintExists, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.SetEdgeConstraint(areaName, constraintExists, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetEdgeConstraint", $"Failed to set edge constraint for area '{areaName}'");

            _logger.LogDebug("Set edge constraint for area {AreaName}: {ConstraintExists}", areaName, constraintExists);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting edge constraint for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets edge constraint for an area object.
    /// Wraps cSapModel.AreaObj.GetEdgeConstraint.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if constraint exists</returns>
    public bool GetEdgeConstraint(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            bool constraintExists = false;
            int ret = _sapModel.AreaObj.GetEdgeConstraint(areaName, ref constraintExists);

            if (ret != 0)
                throw new EtabsException(ret, "GetEdgeConstraint", $"Failed to get edge constraint for area '{areaName}'");

            return constraintExists;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting edge constraint for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Opening Methods

    /// <summary>
    /// Sets opening status for an area object.
    /// Wraps cSapModel.AreaObj.SetOpening.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="isOpening">True if area is an opening</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetOpening(string areaName, bool isOpening, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            int ret = _sapModel.AreaObj.SetOpening(areaName, isOpening, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetOpening", $"Failed to set opening status for area '{areaName}'");

            _logger.LogDebug("Set opening status for area {AreaName}: {IsOpening}", areaName, isOpening);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting opening status for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets opening status for an area object.
    /// Wraps cSapModel.AreaObj.GetOpening.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if area is an opening</returns>
    public bool GetOpening(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            bool isOpening = false;
            int ret = _sapModel.AreaObj.GetOpening(areaName, ref isOpening);

            if (ret != 0)
                throw new EtabsException(ret, "GetOpening", $"Failed to get opening status for area '{areaName}'");

            return isOpening;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting opening status for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Pier and Spandrel Methods

    /// <summary>
    /// Sets pier assignment for an area object.
    /// Wraps cSapModel.AreaObj.SetPier.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="pierName">Name of the pier</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetPier(string areaName, string pierName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(pierName))
                throw new ArgumentException("Pier name cannot be null or empty", nameof(pierName));

            int ret = _sapModel.AreaObj.SetPier(areaName, pierName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetPier", $"Failed to set pier assignment for area '{areaName}'");

            _logger.LogDebug("Set pier {PierName} for area {AreaName}", pierName, areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting pier assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets pier assignment for an area object.
    /// Wraps cSapModel.AreaObj.GetPier.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned pier</returns>
    public string GetPier(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            string pierName = "";
            int ret = _sapModel.AreaObj.GetPier(areaName, ref pierName);

            if (ret != 0)
                throw new EtabsException(ret, "GetPier", $"Failed to get pier assignment for area '{areaName}'");

            return pierName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting pier assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets spandrel assignment for an area object.
    /// Wraps cSapModel.AreaObj.SetSpandrel.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="spandrelName">Name of the spandrel</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetSpandrel(string areaName, string spandrelName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(spandrelName))
                throw new ArgumentException("Spandrel name cannot be null or empty", nameof(spandrelName));

            int ret = _sapModel.AreaObj.SetSpandrel(areaName, spandrelName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetSpandrel", $"Failed to set spandrel assignment for area '{areaName}'");

            _logger.LogDebug("Set spandrel {SpandrelName} for area {AreaName}", spandrelName, areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting spandrel assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets spandrel assignment for an area object.
    /// Wraps cSapModel.AreaObj.GetSpandrel.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned spandrel</returns>
    public string GetSpandrel(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            string spandrelName = "";
            int ret = _sapModel.AreaObj.GetSpandrel(areaName, ref spandrelName);

            if (ret != 0)
                throw new EtabsException(ret, "GetSpandrel", $"Failed to get spandrel assignment for area '{areaName}'");

            return spandrelName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting spandrel assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Diaphragm Methods

    /// <summary>
    /// Sets diaphragm assignment for an area object.
    /// Wraps cSapModel.AreaObj.SetDiaphragm.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="diaphragmName">Name of the diaphragm</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetDiaphragm(string areaName, string diaphragmName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));
            if (string.IsNullOrEmpty(diaphragmName))
                throw new ArgumentException("Diaphragm name cannot be null or empty", nameof(diaphragmName));

            int ret = _sapModel.AreaObj.SetDiaphragm(areaName, diaphragmName);

            if (ret != 0)
                throw new EtabsException(ret, "SetDiaphragm", $"Failed to set diaphragm assignment for area '{areaName}'");

            _logger.LogDebug("Set diaphragm {DiaphragmName} for area {AreaName}", diaphragmName, areaName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting diaphragm assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets diaphragm assignment for an area object.
    /// Wraps cSapModel.AreaObj.GetDiaphragm.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>Name of the assigned diaphragm</returns>
    public string GetDiaphragm(string areaName)
    {
        try
        {
            if (string.IsNullOrEmpty(areaName))
                throw new ArgumentException("Area name cannot be null or empty", nameof(areaName));

            string diaphragmName = "";
            int ret = _sapModel.AreaObj.GetDiaphragm(areaName, ref diaphragmName);

            if (ret != 0)
                throw new EtabsException(ret, "GetDiaphragm", $"Failed to get diaphragm assignment for area '{areaName}'");

            return diaphragmName;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting diaphragm assignment for area '{areaName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods for Common Design Settings

    /// <summary>
    /// Sets an area as a wall pier.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="pierName">Name of the pier</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetAsWallPier(string areaName, string pierName, eItemType itemType = eItemType.Objects)
    {
        int ret1 = SetPier(areaName, pierName, itemType);
        if (ret1 != 0) return ret1;

        // Set edge constraints for wall behavior
        int ret2 = SetEdgeConstraint(areaName, true, itemType);
        return ret2;
    }

    /// <summary>
    /// Sets an area as a wall spandrel.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="spandrelName">Name of the spandrel</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetAsWallSpandrel(string areaName, string spandrelName, eItemType itemType = eItemType.Objects)
    {
        int ret1 = SetSpandrel(areaName, spandrelName, itemType);
        if (ret1 != 0) return ret1;

        // Set edge constraints for wall behavior
        int ret2 = SetEdgeConstraint(areaName, true, itemType);
        return ret2;
    }

    /// <summary>
    /// Sets an area as a floor diaphragm.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="diaphragmName">Name of the diaphragm</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetAsFloorDiaphragm(string areaName, string diaphragmName)
    {
        int ret1 = SetDiaphragm(areaName, diaphragmName);
        if (ret1 != 0) return ret1;

        // Remove edge constraints for flexible diaphragm behavior
        int ret2 = SetEdgeConstraint(areaName, false);
        return ret2;
    }

    /// <summary>
    /// Sets an area as an opening (void).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetAsOpening(string areaName, eItemType itemType = eItemType.Objects)
    {
        return SetOpening(areaName, true, itemType);
    }

    /// <summary>
    /// Sets an area as a solid element (not an opening).
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetAsSolid(string areaName, eItemType itemType = eItemType.Objects)
    {
        return SetOpening(areaName, false, itemType);
    }

    /// <summary>
    /// Checks if an area has any design assignments.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>True if design assignments exist</returns>
    public bool HasDesignAssignments(string areaName)
    {
        try
        {
            string pier = GetPier(areaName);
            string spandrel = GetSpandrel(areaName);
            string diaphragm = GetDiaphragm(areaName);

            return !string.IsNullOrEmpty(pier) || 
                   !string.IsNullOrEmpty(spandrel) || 
                   !string.IsNullOrEmpty(diaphragm);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets a summary of all design assignments for an area.
    /// </summary>
    /// <param name="areaName">Name of the area object</param>
    /// <returns>String summary of design assignments</returns>
    public string GetDesignAssignmentSummary(string areaName)
    {
        try
        {
            var assignments = new List<string>();

            string pier = GetPier(areaName);
            if (!string.IsNullOrEmpty(pier))
                assignments.Add($"Pier: {pier}");

            string spandrel = GetSpandrel(areaName);
            if (!string.IsNullOrEmpty(spandrel))
                assignments.Add($"Spandrel: {spandrel}");

            string diaphragm = GetDiaphragm(areaName);
            if (!string.IsNullOrEmpty(diaphragm))
                assignments.Add($"Diaphragm: {diaphragm}");

            bool hasEdgeConstraints = GetEdgeConstraint(areaName);
            if (hasEdgeConstraints)
                assignments.Add("Edge Constraints: Yes");

            bool isOpening = GetOpening(areaName);
            if (isOpening)
                assignments.Add("Opening: Yes");

            return assignments.Count > 0 ? string.Join(", ", assignments) : "No design assignments";
        }
        catch (Exception ex)
        {
            return $"Error getting design assignments: {ex.Message}";
        }
    }

    #endregion
}