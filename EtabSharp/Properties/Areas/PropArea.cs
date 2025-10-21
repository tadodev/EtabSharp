using EtabSharp.Exceptions;
using EtabSharp.Interfaces.Properties;
using EtabSharp.Properties.Areas.Models;
using ETABSv1;
using Microsoft.Extensions.Logging;

namespace EtabSharp.Properties.Areas;

/// <summary>
/// Manages area properties in the ETABS model.
/// Implements the IPropArea interface by wrapping cSapModel.PropArea operations.
/// This is the main partial class containing core property management methods.
/// </summary>
public partial class PropArea : IPropArea
{
    private readonly cSapModel _sapModel;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the PropArea class.
    /// </summary>
    /// <param name="sapModel">The ETABS SapModel instance</param>
    /// <param name="logger">Logger for operation tracking</param>
    public PropArea(cSapModel sapModel, ILogger logger)
    {
        _sapModel = sapModel ?? throw new ArgumentNullException(nameof(sapModel));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region General Property Methods

    /// <summary>
    /// Changes the name of an existing area property.
    /// </summary>
    /// <param name="currentName">Current name of the property</param>
    /// <param name="newName">New name for the property</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int ChangeName(string currentName, string newName)
    {
        try
        {
            if (string.IsNullOrEmpty(currentName))
                throw new ArgumentException("Current name cannot be null or empty", nameof(currentName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New name cannot be null or empty", nameof(newName));

            int ret = _sapModel.PropArea.ChangeName(currentName, newName);

            if (ret != 0)
                throw new EtabsException(ret, "ChangeName", $"Failed to change area property name from '{currentName}' to '{newName}'");

            _logger.LogDebug("Changed area property name from {OldName} to {NewName}", currentName, newName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error changing area property name from '{currentName}' to '{newName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the count of area properties in the model.
    /// </summary>
    /// <param name="propertyType">Property type filter (0 for all)</param>
    /// <returns>Total number of area properties</returns>
    public int Count(int propertyType = 0)
    {
        try
        {
            int count = _sapModel.PropArea.Count(propertyType);
            _logger.LogDebug("Area property count: {Count} (type filter: {PropertyType})", count, propertyType);
            return count;
        }
        catch (Exception ex)
        {
            throw new EtabsException($"Unexpected error getting area property count: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes an area property from the model.
    /// </summary>
    /// <param name="propertyName">Name of the property to delete</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int Delete(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int ret = _sapModel.PropArea.Delete(propertyName);

            if (ret != 0)
                throw new EtabsException(ret, "Delete", $"Failed to delete area property '{propertyName}'");

            _logger.LogDebug("Deleted area property {PropertyName}", propertyName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error deleting area property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the names of all area properties.
    /// </summary>
    /// <param name="propertyType">Property type filter (0 for all)</param>
    /// <returns>Array of property names</returns>
    public string[] GetNameList(int propertyType = 0)
    {
        try
        {
            int numberNames = 0;
            string[] names = null;

            int ret = _sapModel.PropArea.GetNameList(ref numberNames, ref names, propertyType);

            if (ret != 0)
                throw new EtabsException(ret, "GetNameList", "Failed to get area property name list");

            _logger.LogDebug("Retrieved {Count} area property names (type filter: {PropertyType})", numberNames, propertyType);
            return names ?? new string[0];
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting area property name list: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets the property type for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>Property type enumeration</returns>
    public eAreaPropertyType GetPropertyType(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            int propertyType = 0;
            int ret = _sapModel.PropArea.GetTypeOAPI(propertyName, ref propertyType);

            if (ret != 0)
                throw new EtabsException(ret, "GetTypeOAPI", $"Failed to get property type for area property '{propertyName}'");

            return (eAreaPropertyType)propertyType;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting property type for area property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion

    #region Modifiers

    /// <summary>
    /// Sets property modifiers for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <param name="modifiers">AreaPropertyModifiers model with modifier values</param>
    /// <returns>0 if successful, non-zero otherwise</returns>
    public int SetModifiers(string propertyName, AreaPropertyModifiers modifiers)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));
            if (modifiers == null)
                throw new ArgumentNullException(nameof(modifiers));

            double[] modifierArray = modifiers.ToArray();
            int ret = _sapModel.PropArea.SetModifiers(propertyName, ref modifierArray);

            if (ret != 0)
                throw new EtabsException(ret, "SetModifiers", $"Failed to set modifiers for area property '{propertyName}'");

            _logger.LogDebug("Set modifiers for area property {PropertyName}", propertyName);
            return ret;
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error setting modifiers for area property '{propertyName}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Gets property modifiers for an area property.
    /// </summary>
    /// <param name="propertyName">Name of the property</param>
    /// <returns>AreaPropertyModifiers model with modifier values</returns>
    public AreaPropertyModifiers GetModifiers(string propertyName)
    {
        try
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name cannot be null or empty", nameof(propertyName));

            double[] modifierArray = new double[10];
            int ret = _sapModel.PropArea.GetModifiers(propertyName, ref modifierArray);

            if (ret != 0)
                throw new EtabsException(ret, "GetModifiers", $"Failed to get modifiers for area property '{propertyName}'");

            return AreaPropertyModifiers.FromArray(modifierArray);
        }
        catch (Exception ex) when (!(ex is EtabsException))
        {
            throw new EtabsException($"Unexpected error getting modifiers for area property '{propertyName}': {ex.Message}", ex);
        }
    }

    #endregion

    // Wall Properties are implemented in PropArea.Wall.cs partial class
    // Slab Properties are implemented in PropArea.Slab.cs partial class
    // Deck Properties are implemented in PropArea.Deck.cs partial class
    // Shell Properties are implemented in PropArea.Shell.cs partial class
}