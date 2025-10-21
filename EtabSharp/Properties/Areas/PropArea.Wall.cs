using EtabSharp.Exceptions;
using EtabSharp.Properties.Areas.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Areas;

/// <summary>
/// PropArea partial class - Wall Property Methods
/// </summary>
public partial class PropArea
{
    #region Wall Properties

    /// <summary>
    /// Sets a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="wallProperty">WallProperty model with wall parameters</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetWall(string propertyName, WallProperty wallProperty)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (wallProperty == null)
                throw new ArgumentNullException(nameof(wallProperty));

            int ret = _sapModel.PropArea.SetWall(propertyName, wallProperty.WallPropType, wallProperty.ShellType,
                wallProperty.MaterialProperty, wallProperty.Thickness, wallProperty.Color, wallProperty.Notes, wallProperty.GUID);

            if (ret != 0)
                throw new EtabsException(ret, "SetWall", $"Failed to set wall property '{propertyName}'");

            _logger.LogDebug("Set wall property {PropertyName}: {Wall}", propertyName, wallProperty.ToString());
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting wall property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>WallProperty model with wall parameters</returns>
    public WallProperty GetWall(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            eWallPropType wallPropType = eWallPropType.Specified;
            eShellType shellType = eShellType.ShellThin;
            string materialProperty = "";
            double thickness = 0;
            int color = 0;
            string notes = "";
            string guid = "";

            int ret = _sapModel.PropArea.GetWall(propertyName, ref wallPropType, ref shellType, ref materialProperty,
                ref thickness, ref color, ref notes, ref guid);

            if (ret != 0)
                throw new EtabsException(ret, "GetWall", $"Failed to get wall property '{propertyName}'");

            return new WallProperty
            {
                Name = propertyName,
                WallPropType = wallPropType,
                ShellType = shellType,
                MaterialProperty = materialProperty,
                Thickness = thickness,
                Color = color,
                Notes = notes,
                GUID = guid
            };
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting wall property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Sets auto select list for a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="autoSelectList">Array of property names for auto selection</param>
    /// <param name="startingProperty">Starting property for auto selection</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetWallAutoSelectList(string propertyName, string[] autoSelectList, string startingProperty = "Median")
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (autoSelectList == null || autoSelectList.Length == 0)
                throw new ArgumentException("Auto select list cannot be null or empty", nameof(autoSelectList));

            int ret = _sapModel.PropArea.SetWallAutoSelectList(propertyName, autoSelectList, startingProperty);

            if (ret != 0)
                throw new EtabsException(ret, "SetWallAutoSelectList", $"Failed to set auto select list for wall property '{propertyName}'");

            _logger.LogDebug("Set auto select list for wall property {PropertyName}: {Count} items", propertyName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting auto select list for wall property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets auto select list for a wall property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>Tuple of (AutoSelectList, StartingProperty)</returns>
    public (string[] AutoSelectList, string StartingProperty) GetWallAutoSelectList(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            string[] autoSelectList = null;
            string startingProperty = "";

            int ret = _sapModel.PropArea.GetWallAutoSelectList(propertyName, ref autoSelectList, ref startingProperty);

            if (ret != 0)
                throw new EtabsException(ret, "GetWallAutoSelectList", $"Failed to get auto select list for wall property '{propertyName}'");

            return (autoSelectList ?? new string[0], startingProperty);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting auto select list for wall property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion
}