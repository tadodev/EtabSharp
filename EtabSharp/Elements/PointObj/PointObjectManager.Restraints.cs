using EtabSharp.Elements.PointObj.Models;
using EtabSharp.Exceptions;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Elements.PointObj;

/// <summary>
/// PointObjectManager partial class - Restraint and Support Methods
/// </summary>
public partial class PointObjectManager
{
    #region Restraint Methods

    /// <summary>
    /// Assigns restraint conditions (supports) to a point object.
    /// Wraps cSapModel.PointObj.SetRestraint.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="restraint">PointRestraint model with restraint conditions</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetRestraint(string pointName, PointRestraint restraint, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));
            if (restraint == null)
                throw new ArgumentNullException(nameof(restraint));

            bool[] restraintArray = restraint.ToArray();
            int ret = _sapModel.PointObj.SetRestraint(pointName, ref restraintArray, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "SetRestraint", $"Failed to set restraint for point '{pointName}'");

            _logger.LogDebug("Set restraint for point {PointName}: {Restraint}", pointName, restraint.ToString());

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting restraint for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the restraint conditions assigned to a point object.
    /// Wraps cSapModel.PointObj.GetRestraint.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <returns>PointRestraint model with restraint conditions, or null if no restraints</returns>
    public PointRestraint? GetRestraint(string pointName)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            bool[] restraintArray = new bool[6];
            int ret = _sapModel.PointObj.GetRestraint(pointName, ref restraintArray);

            if (ret != 0)
                throw new EtabsException(ret, "GetRestraint", $"Failed to get restraint for point '{pointName}'");

            // Check if any restraints are applied
            if (restraintArray.All(r => !r))
                return null; // No restraints applied

            var restraint = PointRestraint.FromArray(restraintArray);
            _logger.LogDebug("Retrieved restraint for point {PointName}: {Restraint}", pointName, restraint.ToString());

            return restraint;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting restraint for point '{pointName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Removes all restraints from a point object (makes it free).
    /// Wraps cSapModel.PointObj.DeleteRestraint.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for deletion</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int DeleteRestraint(string pointName, eItemType itemType = eItemType.Objects)
    {
        try
        {
            if (string.IsNullOrEmpty(pointName))
                throw new ArgumentException("Point name cannot be null or empty", nameof(pointName));

            int ret = _sapModel.PointObj.DeleteRestraint(pointName, itemType);

            if (ret != 0)
                throw new EtabsException(ret, "DeleteRestraint", $"Failed to delete restraint for point '{pointName}'");

            _logger.LogDebug("Deleted restraint for point {PointName}", pointName);

            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting restraint for point '{pointName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Convenience Methods for Common Support Types

    /// <summary>
    /// Sets a fixed support (all DOFs restrained) at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetFixedSupport(string pointName, eItemType itemType = eItemType.Objects)
    {
        return SetRestraint(pointName, PointRestraint.Fixed(), itemType);
    }

    /// <summary>
    /// Sets a pinned support (translations restrained, rotations free) at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetPinnedSupport(string pointName, eItemType itemType = eItemType.Objects)
    {
        return SetRestraint(pointName, PointRestraint.Pinned(), itemType);
    }

    /// <summary>
    /// Sets a roller support in Z direction (Uz restrained only) at a point.
    /// </summary>
    /// <param name="pointName">Name of the point object</param>
    /// <param name="itemType">Item type for assignment</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetRollerSupport(string pointName, eItemType itemType = eItemType.Objects)
    {
        return SetRestraint(pointName, PointRestraint.RollerZ(), itemType);
    }

    #endregion
}